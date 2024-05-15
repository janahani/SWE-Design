using Gym_Web_Application.Models;

public class ClassService
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    private ClassAuthoritiesFactory _classAuthoritiesFactory;

    public ClassService(IWebHostEnvironment hostingEnvironment, ClassAuthoritiesFactory classAuthoritiesFactory)
    {
        _hostingEnvironment = hostingEnvironment;
        _classAuthoritiesFactory = classAuthoritiesFactory;
    }

    
    public async Task AddClass(ClassModel classModel, IFormFile imageFile, List<string> selectedDays)
    {
        await _classAuthoritiesFactory.addClass(classModel,imageFile,selectedDays);
    }

    public async Task<List<ClassModel>> GetAllClasses()
    {
        return await _classAuthoritiesFactory.getClasses();
    }

    public async Task<string> GetClassName(int classId)
    {
        return await _classAuthoritiesFactory.getClassName(classId);
    }

    public async Task<List<ClassDaysModel>> GetAllClassDays()
    {
        return await _classAuthoritiesFactory.getClassDays();
    }

    public async Task<List<AssignedClassModel>> GetAllAssignedClasses()
    {
        return await _classAuthoritiesFactory.getAssignedClasses();
    }

    public async Task<List<DateTime>> GetClassDays(int classId)
    {
        return await _classAuthoritiesFactory.getClassDays(classId);
    }

    public async Task AddAssignedClass(AssignedClassModel assignedClassRequest)
    {
        await _classAuthoritiesFactory.addAssignedClass(assignedClassRequest);
    }

    public async Task AddReservedClass(int AssignedClassID, int ClientID, int CoachID)
    {
        await _classAuthoritiesFactory.addReservedClass(AssignedClassID, ClientID,CoachID);
    }

    public async Task<ClassModel> GetClassById(int classId)
    {
        return await _classAuthoritiesFactory.getClassById(classId);
    }

    public async Task<List<ClassDaysModel>> GetClassDaysByClassId(int classId)
    {
        return await _classAuthoritiesFactory.getClassDaysByClassId(classId);
    }

    public async Task UpdateClass(ClassModel updatedClass)
    {
        await _classAuthoritiesFactory.editClass(updatedClass);
    }


    public async Task AddClassDay(ClassDaysModel classDay)
    {
        await _classAuthoritiesFactory.addClassDay(classDay);
    }

    public async Task RemoveClassDay(int classDayId)
    {
        await _classAuthoritiesFactory.removeClassDay(classDayId);
    }


}
