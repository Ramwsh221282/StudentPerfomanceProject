namespace SPerfomance.Domain.Tools;

public static class GenericArrayExtensions
{
    public static T[] InitializeTwoSide<TArraytype, TConvertableType>(
        this TArraytype array,
        TConvertableType convertableType
    ) { }
}
