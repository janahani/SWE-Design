using Gym_Web_Application.Data;
using Gym_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;


public class ClassService
{
    private readonly AppDbContext _dbContext;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ClassService(DbContextOptions<AppDbContext> options,IWebHostEnvironment hostingEnvironment)
    {
        _dbContext = AppDbContext.GetInstance(options);
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task AddClass(ClassModel classModel, IFormFile imageFile, List<string> selectedDays)
    {
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
                _dbContext.ClassDays.Add(classDay);
            }
            await _dbContext.SaveChangesAsync();
        }
    }

}