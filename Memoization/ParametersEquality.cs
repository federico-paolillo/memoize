using System;

namespace Memoization;

public static class ParametersEquality
{
    public static bool AreNotEqual<TParameter>(TParameter first, TParameter second)
    {
        if (ReferenceEquals(first, second))
        {
            return false;
        }

        return Equals(first, second);
    }

    public static bool AreNotEqual<TParameter>(IEquatable<TParameter> first, TParameter second)
    {
        if (ReferenceEquals(first, second))
        {
            return false;
        }

        return first.Equals(second);
    }
}
