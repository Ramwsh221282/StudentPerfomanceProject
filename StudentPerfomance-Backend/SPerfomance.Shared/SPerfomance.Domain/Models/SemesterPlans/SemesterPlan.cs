using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Models.SemesterPlans.ValueObjects;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.SemesterPlans;

/// <summary>
/// Сущность - Преподаваемая дисциплина
/// </summary>
public class SemesterPlan : DomainEntity
{
    /// <summary>
    /// Название дисциплины
    /// </summary>
    public DisciplineName Discipline { get; private set; }

    /// <summary>
    /// Номер семестра, в котором находится дисциплина
    /// </summary>
    public Semester Semester { get; private set; }

    /// <summary>
    /// Закрепленный преподаватель дисциплины
    /// </summary>
    public Teacher? Teacher { get; private set; }

    /// <summary>
    /// Конструктор для ORM - EF Core.
    /// </summary>
    internal SemesterPlan()
        : base(Guid.Empty)
    {
        Discipline = DisciplineName.Empty;
        Semester = Semester.Empty;
    }

    /// <summary>
    /// Конструктор создания объекта
    /// </summary>
    /// <param name="name">Название дисциплины</param>
    /// <param name="semester">Семестр дисциплины</param>
    internal SemesterPlan(DisciplineName name, Semester semester)
        : base(Guid.NewGuid())
    {
        Discipline = name;
        Semester = semester;
    }

    /// <summary>
    /// Создание объекта без инициализации полей
    /// </summary>
    internal static SemesterPlan Empty => new SemesterPlan();

    /// <summary>
    /// Метод закрепления преподавателя в дисциплине
    /// </summary>
    /// <param name="teacher">Объект - преподаватель</param>
    /// <returns>Результат операции закрепления преподавателя.</returns>
    internal Result<SemesterPlan> AttachTeacher(Teacher teacher)
    {
        if (Teacher != null)
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.TeacherAlreadyAttacher());

        Teacher = teacher;
        return Result<SemesterPlan>.Success(this);
    }

    /// <summary>
    /// Метод открепления преподавателя в дисциплине
    /// </summary>
    /// <returns>Результат операции открепления преподавателя.</returns>
    internal Result<SemesterPlan> DeattachTeacher()
    {
        if (Teacher == null)
            return Result<SemesterPlan>.Success(this);

        Teacher = null;
        return Result<SemesterPlan>.Success(this);
    }

    /// <summary>
    /// Метод изменения названия у дисциплины
    /// </summary>
    /// <param name="name">Новое название дисциплины</param>
    /// <returns>Возвращает результат выполнения операции изменения названия дисциплины</returns>
    internal Result<SemesterPlan> ChangeName(string name)
    {
        Result<DisciplineName> newName = DisciplineName.Create(name);
        if (newName.IsFailure)
            return Result<SemesterPlan>.Failure(newName.Error);

        if (Discipline == newName.Value)
            return Result<SemesterPlan>.Success(this);

        Discipline = newName.Value;
        return Result<SemesterPlan>.Success(this);
    }

    /// <summary>
    /// Статичный фабричный метод создания экземпляра класса дисциплины
    /// </summary>
    /// <param name="disciplineName">Название дисциплины</param>
    /// <param name="semester">Семестр, в котором дисциплина создается</param>
    /// <returns>Результат создания экземпляра класса дисциплины</returns>
    internal static Result<SemesterPlan> Create(string disciplineName, Semester semester)
    {
        Result<DisciplineName> name = DisciplineName.Create(disciplineName);
        if (name.IsFailure)
            return Result<SemesterPlan>.Failure(name.Error);

        return Result<SemesterPlan>.Success(new SemesterPlan(name.Value, semester));
    }
}
