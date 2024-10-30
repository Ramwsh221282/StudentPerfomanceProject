using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationDirections.Errors;

public static class EducationDirectionErrors
{
	public static Error NotFoundError() => new Error("Направление подготовки не найдено");

	public static Error NameEmptyError() => new Error("Название направления подготовки было пустое");

	public static Error TypeEmptyError() => new Error("Тип направления подготовки не был указан");

	public static Error CodeEmptyError() => new Error("Код направления подготовки не был указан");

	public static Error NameExceessLengthError(int length) => new Error($"Название направления подготовки более {length} символов");

	public static Error NameIsNotSatisfineError(int length) => new Error($"Название направления подготовки менее {length} символов");

	public static Error TypeInvalidError(string type) => new Error($"Тип направления подготовки {type} не допустим");

	public static Error CodeInvalidError(string code) => new Error($"Код направления подготовки {code} недопустим");

	public static Error CodeExceessLengthError(int length) => new Error($"Код направления подготовки более {length} символов");

	public static Error CodeDublicateError(string code) => new Error($"Направление подготовки с кодом {code} уже существует");

	public static Error DirectionDublicateError(string code, string name, string type) =>
		new Error($"Направление подготовки {code} {name} {type} уже существует");
}
