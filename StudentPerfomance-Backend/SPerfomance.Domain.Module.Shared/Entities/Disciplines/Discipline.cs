using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Disciplines.Validators;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Domain.Module.Shared.Entities.Disciplines;

public sealed class Discipline : Entity
{
	private Discipline() : base(Guid.Empty)
	{
		Name = string.Empty;
	}

	private Discipline(Guid id, string name) : base(id) => Name = name;

	public Teacher? Teacher { get; private set; }

	public string Name { get; private set; }

	public void ChangeName(string name) => Name = name;

	public void SetTeacher(Teacher teacher) => Teacher = teacher;

	public void RemoveTeacher(Teacher teacher)
	{
		if (Teacher != null && Teacher.Id == teacher.Id)
			Teacher = null;
	}

	public static CSharpFunctionalExtensions.Result<Discipline> Create(string name)
	{
		Validator<Discipline> validator = new DisciplineValidator(name);
		return validator.Validate() ?
			new Discipline(Guid.NewGuid(), name) :
			CSharpFunctionalExtensions.Result.Failure<Discipline>(validator.GetErrorText());
	}

	public override bool Equals(object? obj)
	{
		if (obj == null) return false;
		if (obj is Discipline other)
		{
			return Name == other.Name;
		}
		return false;
	}

	public override int GetHashCode() => GetHashCode();
	public override string ToString() => Name;
}
