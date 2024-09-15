using StudentPerfomance.Api.Responses.Teachers;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Api.Responses.Disciplines;

public class DisciplineResponse
{
    private DisciplineResponse(string name, TeacherResponse teacher) => 
        (Name, Teacher) = (name, teacher);

    public string Name { get; }        
    public TeacherResponse Teacher { get; }

    public static DisciplineResponse FromDiscipline(Discipline discipline)
    {
        return new DisciplineResponse
        (
            discipline.Name,
            TeacherResponse.FromTeacher(discipline.Teacher)            
        );
    }
}
