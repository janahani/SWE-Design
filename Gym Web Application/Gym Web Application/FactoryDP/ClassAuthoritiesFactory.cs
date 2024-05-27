using Gym_Web_Application.Models;
using Gym_Web_Application.Data;
using Microsoft.EntityFrameworkCore;

public class ClassAuthoritiesFactory : AuthorityModel,IGetAuthority<ClassModel>
{
    private readonly DbContextOptions<AppDbContext> _options;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ClassAuthoritiesFactory(DbContextOptions<AppDbContext> options, IWebHostEnvironment hostingEnvironment)
    {
        _options = options;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task addClass(ClassModel classModel, IFormFile imageFile, List<string> selectedDays)
    {
        using var _dbContext = new AppDbContext(_options);
        if (imageFile != null && imageFile.Length > 0)
        {

            string originalFileName = Path.GetFileName(imageFile.FileName);
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", originalFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            ClassModel classEntity = new ClassModel
            {
                Name = classModel.Name,
                Description = classModel.Description,
                ImgPath = "/images/" + originalFileName
            };

            await _dbContext.Classes.AddAsync(classEntity);
            await _dbContext.SaveChangesAsync();

            foreach (var day in selectedDays)
            {
                ClassDaysModel classDay = new ClassDaysModel
                {
                    ClassID = classEntity.ID,
                    Days = day
                };

                await _dbContext.ClassDays.AddAsync(classDay);
            }

            await _dbContext.SaveChangesAsync();

        }
    }

    public async Task<List<ClassModel>> getAll()
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.Classes.ToListAsync();
    }

    public async Task<string> getClassName(int classId)
    {
        using var _dbContext = new AppDbContext(_options);
        var className = await _dbContext.Classes
            .Where(c => c.ID == classId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync();

        return className;
    }

    public async Task<List<ClassDaysModel>> getClassDays()
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.ClassDays.ToListAsync();
    }

    public async Task<List<AssignedClassModel>> getAssignedClasses()
    {
        using var _dbContext = new AppDbContext(_options);
        var now = DateTime.Now;

        return await _dbContext.AssignedClasses
            .Where(ac => (ac.Date > now.Date ||
                          (ac.Date == now.Date && ac.StartTime.TimeOfDay > now.TimeOfDay)) &&
                          ac.AvailablePlaces > 0)
            .ToListAsync();
    }

    public async Task<List<DateTime>> getClassDays(int classId)
    {
        using var _dbContext = new AppDbContext(_options);
        var classDays = await _dbContext.ClassDays
            .Where(cd => cd.ClassID == classId)
            .Select(cd => cd.Days)
            .ToListAsync();

        List<DateTime> upcomingDates = new List<DateTime>();

        foreach (var day in classDays)
        {
            upcomingDates.Add(CalculateNextOccurrenceDate(day));
        }

        return upcomingDates;
    }

    public async Task addAssignedClass(AssignedClassModel assignedClassRequest)
    {
        using var _dbContext = new AppDbContext(_options);
        var assignedClass = new AssignedClassModel()
        {
            ClassID = assignedClassRequest.ClassID,
            CoachID = assignedClassRequest.CoachID,
            Date = assignedClassRequest.Date,
            StartTime = assignedClassRequest.StartTime,
            EndTime = assignedClassRequest.EndTime,
            IsFree = assignedClassRequest.IsFree,
            Price = assignedClassRequest.Price,
            NumOfAttendants = assignedClassRequest.NumOfAttendants,
            AvailablePlaces = assignedClassRequest.NumOfAttendants
        };
        await _dbContext.AssignedClasses.AddAsync(assignedClass);
        await _dbContext.SaveChangesAsync();
    }
    public async Task addReservedClass(int AssignedClassID, int ClientID, int CoachID)
    {
        using var _dbContext = new AppDbContext(_options);
        var assignedClass = await _dbContext.AssignedClasses.FindAsync(AssignedClassID);

        if (assignedClass != null)
        {
            assignedClass.AvailablePlaces -= 1;

            var reservedClass = new ReservedClassModel()
            {
                AssignedClassID = AssignedClassID,
                CoachID = CoachID,
                ClientID = ClientID
            };

            await _dbContext.ReservedClasses.AddAsync(reservedClass);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException("Class not found or no available places.");
        }
    }

    public async Task<ClassModel> getClassById(int classId)
    {
        using var _dbContext = new AppDbContext(_options);
        var classObject = await _dbContext.Classes.FirstOrDefaultAsync(c => c.ID == classId);

        if (classObject == null)
        {
            return null;
        }

        var classModel = new ClassModel
        {
            Name = classObject.Name,
            Description = classObject.Description
        };

        return classModel;
    }

    public async Task<List<ClassDaysModel>> getClassDaysByClassId(int classId)
    {
        using var _dbContext = new AppDbContext(_options);
        return await _dbContext.ClassDays.Where(cd => cd.ClassID == classId).ToListAsync();
    }

    public async Task editClass(ClassModel updatedClass)
    {
        using var _dbContext = new AppDbContext(_options);
        var classObject = await _dbContext.Classes.FirstOrDefaultAsync(c => c.ID == updatedClass.ID);
        if (classObject != null)
        {
            classObject.Name = updatedClass.Name;
            classObject.Description = updatedClass.Description;

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task addClassDay(ClassDaysModel classDay)
    {
        using var _dbContext = new AppDbContext(_options);
        await _dbContext.ClassDays.AddAsync(classDay);
        await _dbContext.SaveChangesAsync();
    }

    public async Task removeClassDay(int classDayId)
    {
        using var _dbContext = new AppDbContext(_options);
        var classDayToRemove = await _dbContext.ClassDays.FindAsync(classDayId);
        if (classDayToRemove != null)
        {
            _dbContext.ClassDays.Remove(classDayToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }

    private DateTime CalculateNextOccurrenceDate(string day)
    {
        DateTime currentDate = DateTime.Today;

        if (!Enum.TryParse(day, out DayOfWeek selectedDay))
        {
            throw new ArgumentException("Invalid day");
        }
        int daysUntilNextOccurrence = ((int)selectedDay - (int)currentDate.DayOfWeek + 7) % 7;

        DateTime nearestDate = currentDate.AddDays(daysUntilNextOccurrence);

        return nearestDate;
    }
}