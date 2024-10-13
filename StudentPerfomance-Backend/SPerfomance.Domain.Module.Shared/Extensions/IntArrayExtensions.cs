namespace SPerfomance.Domain.Module.Shared.Extensions;

public static class IntArrayExtensions
{
	public static int GetOrderedValue(this int[] array)
	{
		// Если порядковых номеров - 0, возврат 1.
		if (array.Length == 0)
			return 1;

		// Если порядковый номер - 1, возврат 2
		if (array.Length == 1)
			return array[0] += 1;

		// Сортируем порядковые номера по возрастанию
		Array.Sort(array);
		for (int i = 0; i < array.Length - 1; i++)
		{
			// если разность между парами порядковых номеров больше 1, возврат левой границы + 1.
			if (array[i + 1] - array[i] > 1)
				return array[i] + 1;
		}

		// возврат последней границы + 1.
		return array[array.Length - 1] + 1;
	}
}
