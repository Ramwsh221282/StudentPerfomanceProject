using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Validators;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

// Доменная сущность - студенческая группа
public sealed class StudentGroup : Entity
{
	// Коллкция студентов группы
	private List<Student> _students = [];

	// конструктор создания дефолтного объекта.
	// нужен для EF core.
	private StudentGroup() : base(Guid.Empty) => Name = GroupName.CreateDefault();

	// Конструктор создания объекта с параметрами
	private StudentGroup(Guid id, GroupName name) : base(id) => Name = name;

	// Коллекция студентов read-only.
	public IReadOnlyCollection<Student> Students => _students;

	// Value Object - имя группы.
	public GroupName Name { get; private set; } = null!;

	// Учебный план группы. Предоставляет группе иметь программу дисциплин и семестров.
	public EducationPlan? EducationPlan { get; private set; }

	// Метод изменения названия группы
	public CSharpFunctionalExtensions.Result ChangeGroupName(GroupName name)
	{
		// Если имя группы такое же, выдаем ошибку
		if (Name == name)
			return Failure(new GroupSameNameError().ToString());

		// валидация нового названия группы
		Validator<GroupName> validator = new GroupNameValidator(name);
		if (!validator.Validate())
			return Failure(validator.GetErrorText());

		// изменение имени группы
		Name = name;
		return Success();
	}

	// Метод добавления студента в группу
	public CSharpFunctionalExtensions.Result AddStudent(Student student)
	{
		// проверяем наличие такого же студента в группе.
		if (_students.Any(s => s.Id == student.Id &&
			s.Name == student.Name &&
			s.Recordbook == student.Recordbook))
			return Failure(new GroupHasStudentAlreadyError().ToString());

		// проверяем наличие студента с такой зачетной книжкой в группе.
		if (_students.Any(s => s.Recordbook == student.Recordbook))
			return Failure(new StudentDublicateRecordBookError(student.Recordbook.Recordbook).ToString());

		// добавляем студента в группу.
		student.ChangeGroup(this);
		_students.Add(student);
		return Success();
	}

	// Метод удаления студента из группы
	public CSharpFunctionalExtensions.Result RemoveStudent(Student student)
	{
		// если студент пустой - ошибка
		if (student == null)
			return Failure(new StudentNotFoundError().ToString());

		// ищем студента в этой группе по модели параметра метода
		Student? targetStudent = _students.FirstOrDefault(s => s.Id == student.Id);

		// если нет студента в группе, которого нужно удалить - ошибка
		if (targetStudent == null)
			return Failure(new StudentNotFoundError().ToString());

		// удаление студента
		_students.Remove(targetStudent);
		return Success();
	}

	// Метод смешивания студенческих групп.
	public CSharpFunctionalExtensions.Result MergeWithGroup(StudentGroup? mergeGroup)
	{
		// если группа, студентов который мы хотим добавить в текущую группу пустая - ошибка.
		if (mergeGroup == null)
			return Failure(new GroupNotFoundError().ToString());

		// если группа одна и та же - ошибка.
		if (Name == mergeGroup.Name)
			return Failure(new GroupMergeConflictError().ToString());

		// добавляем студентов другой группы в текущую группу
		mergeGroup._students.ForEach(s => s.ChangeGroup(this));
		_students.AddRange(mergeGroup._students);
		mergeGroup._students.Clear();
		return Success();
	}

	// Метод закрепления учебного плана
	public CSharpFunctionalExtensions.Result AttachEducationPlan(EducationPlan plan)
	{
		// если текущий учебный план имеется - ошибка
		if (EducationPlan != null)
			return Failure(new GroupPlanAlreadyAttachedError().ToString());

		// закрепление учебного плана
		EducationPlan = plan;
		return Success();
	}

	// Метод открепления учебного плана
	public CSharpFunctionalExtensions.Result DeattachEducationPlan()
	{
		// Если учебный план не закреплен - ошибка
		if (EducationPlan == null)
			return Failure(new GroupPlanAttachmentError().ToString());

		// Открепление учебного плана
		EducationPlan = null;
		return Success();
	}

	// Создание объекта студенческой группы.
	public static CSharpFunctionalExtensions.Result<StudentGroup> Create(GroupName name) =>
		new StudentGroup(Guid.NewGuid(), name);
}
