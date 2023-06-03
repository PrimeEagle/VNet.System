using System.Globalization;
using System.Numerics;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CompareOfFloatsByEqualityOperator

#pragma warning disable IDE0251

namespace VNet.System;

public readonly struct NormalizedDouble : IComparable<NormalizedDouble>, IFormattable, IEquatable<NormalizedDouble>, INumber<NormalizedDouble>
{
    private readonly double _value;

    public override bool Equals(object? obj)
    {
        return obj is NormalizedDouble other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }


    public NormalizedDouble(decimal value)
    {
        if (value < 0 || value > 1)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 1.");

        _value = (double) value;
    }

    public NormalizedDouble(float value)
    {
        if (value < 0 || value > 1)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 1.");

        _value = (double) value;
    }

    public NormalizedDouble(double value)
    {
        if (value < 0 || value > 1)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 1.");

        _value = value;
    }

    public int CompareTo(double other)
    {
        return _value.CompareTo(other);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return _value.ToString(format, formatProvider);
    }

    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return _value.TryFormat(destination, out charsWritten, format, provider);
    }

    public bool Equals(double other)
    {
        return _value.Equals(other);
    }

    public int CompareTo(object? obj)
    {
        return _value.CompareTo(obj);
    }

    public int CompareTo(NormalizedDouble other)
    {
        return _value.CompareTo(other._value);
    }

    public bool Equals(NormalizedDouble other)
    {
        return _value.Equals(other._value);
    }

    public static NormalizedDouble Parse(string s, IFormatProvider? provider)
    {
        var value = double.Parse(s, provider);
        return new NormalizedDouble(value);
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out NormalizedDouble result)
    {
        if (double.TryParse(s, provider, out var temp))
        {
            result = new NormalizedDouble(temp);
            return true;
        }
        else
        {
            result = default;
            return false;
        }
    }

    public static NormalizedDouble Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        var value = double.Parse(s.ToString(), provider);
        return new NormalizedDouble(value);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out NormalizedDouble result)
    {
        if (double.TryParse(s.ToString(), provider, out var temp))
        {
            result = new NormalizedDouble(temp);
            return true;
        }
        else
        {
            result = default;
            return false;
        }
    }

    public static bool operator ==(NormalizedDouble left, NormalizedDouble right)
    {
        return left._value == right._value;
    }

    public static bool operator !=(NormalizedDouble left, NormalizedDouble right)
    {
        return left._value != right._value;
    }

    public static bool operator >(NormalizedDouble left, NormalizedDouble right)
    {
        return left._value > right._value;
    }

    public static bool operator >=(NormalizedDouble left, NormalizedDouble right)
    {
        return left._value >= right._value;
    }

    public static bool operator <(NormalizedDouble left, NormalizedDouble right)
    {
        return left._value < right._value;
    }

    public static bool operator <=(NormalizedDouble left, NormalizedDouble right)
    {
        return left._value <= right._value;
    }

    public static bool operator ==(double left, NormalizedDouble right)
    {
        return left == right._value;
    }

    public static bool operator !=(double left, NormalizedDouble right)
    {
        return left != right._value;
    }

    public static bool operator >(double left, NormalizedDouble right)
    {
        return left > right._value;
    }

    public static bool operator >=(double left, NormalizedDouble right)
    {
        return left >= right._value;
    }

    public static bool operator <(double left, NormalizedDouble right)
    {
        return left < right._value;
    }

    public static bool operator <=(double left, NormalizedDouble right)
    {
        return left <= right._value;
    }

    public static bool operator ==(NormalizedDouble left, double right)
    {
        return left._value == right;
    }

    public static bool operator !=(NormalizedDouble left, double right)
    {
        return left._value != right;
    }

    public static bool operator >(NormalizedDouble left, double right)
    {
        return left._value > right;
    }

    public static bool operator >=(NormalizedDouble left, double right)
    {
        return left._value >= right;
    }

    public static bool operator <(NormalizedDouble left, double right)
    {
        return left._value < right;
    }

    public static bool operator <=(NormalizedDouble left, double right)
    {
        return left._value <= right;
    }

    public static NormalizedDouble operator --(NormalizedDouble value)
    {
        var result = value._value - 1;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator /(NormalizedDouble left, NormalizedDouble right)
    {
        var result = left._value / right._value;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator ++(NormalizedDouble value)
    {
        var result = value._value + 1;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator %(NormalizedDouble left, NormalizedDouble right)
    {
        var result = left._value % right._value;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator *(NormalizedDouble left, NormalizedDouble right)
    {
        var result = left._value * right._value;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator -(NormalizedDouble left, NormalizedDouble right)
    {
        var result = left._value - right._value;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator -(double left, NormalizedDouble right)
    {
        var result = left - right._value;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator -(NormalizedDouble left, double right)
    {
        var result = left._value - right;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator +(NormalizedDouble left, NormalizedDouble right)
    {
        var result = left._value + right._value;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator +(double left, NormalizedDouble right)
    {
        var result = left + right._value;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator +(NormalizedDouble left, double right)
    {
        var result = left._value + right;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator -(NormalizedDouble value)
    {
        var result = -value._value;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble operator +(NormalizedDouble value)
    {
        var result = value._value;
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble Abs(NormalizedDouble value)
    {
        var result = Math.Abs(value._value);
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static implicit operator double(NormalizedDouble d)
    {
        return d._value;
    }

    public static implicit operator decimal(NormalizedDouble d)
    {
        return (decimal) d._value;
    }

    public static implicit operator float(NormalizedDouble d)
    {
        return (float) d._value;
    }

    public static implicit operator int(NormalizedDouble d)
    {
        return (int) d._value;
    }

    public static implicit operator uint(NormalizedDouble d)
    {
        return (uint) d._value;
    }

    public static implicit operator long(NormalizedDouble d)
    {
        return (long) d._value;
    }

    public static implicit operator ulong(NormalizedDouble d)
    {
        return (ulong) d._value;
    }

    public static implicit operator short(NormalizedDouble d)
    {
        return (short) d._value;
    }

    public static implicit operator ushort(NormalizedDouble d)
    {
        return (ushort) d._value;
    }

    public static implicit operator byte(NormalizedDouble d)
    {
        return (byte) d._value;
    }

    public static implicit operator NormalizedDouble(double d)
    {
        return new NormalizedDouble(d);
    }

    public static implicit operator NormalizedDouble(decimal d)
    {
        return new NormalizedDouble((double) d);
    }

    public static implicit operator NormalizedDouble(float d)
    {
        return new NormalizedDouble((double) d);
    }

    public static implicit operator NormalizedDouble(int d)
    {
        return new NormalizedDouble((double) d);
    }

    public static implicit operator NormalizedDouble(uint d)
    {
        return new NormalizedDouble((double) d);
    }

    public static implicit operator NormalizedDouble(long d)
    {
        return new NormalizedDouble((double) d);
    }

    public static implicit operator NormalizedDouble(ulong d)
    {
        return new NormalizedDouble((double) d);
    }

    public static implicit operator NormalizedDouble(short d)
    {
        return new NormalizedDouble((double) d);
    }

    public static implicit operator NormalizedDouble(ushort d)
    {
        return new NormalizedDouble((double) d);
    }

    public static implicit operator NormalizedDouble(byte d)
    {
        return new NormalizedDouble((double) d);
    }

    public static bool IsCanonical(NormalizedDouble value)
    {
        return false;
    }

    public static bool IsComplexNumber(NormalizedDouble value)
    {
        return false;
    }

    public static bool IsEvenInteger(NormalizedDouble value)
    {
        return value._value % 2 == 0;
    }

    public static bool IsFinite(NormalizedDouble value)
    {
        return double.IsFinite(value._value);
    }

    public static bool IsImaginaryNumber(NormalizedDouble value)
    {
        return false;
    }

    public static bool IsInfinity(NormalizedDouble value)
    {
        return double.IsInfinity(value._value);
    }

    public static bool IsInteger(NormalizedDouble value)
    {
        return value._value % 1 == 0;
    }

    public static bool IsNaN(NormalizedDouble value)
    {
        return double.IsNaN(value._value);
    }

    public static bool IsNegative(NormalizedDouble value)
    {
        return value._value < 0;
    }

    public static bool IsNegativeInfinity(NormalizedDouble value)
    {
        return double.IsNegativeInfinity(value._value);
    }

    public static bool IsNormal(NormalizedDouble value)
    {
        return double.IsNormal(value._value);
    }

    public static bool IsOddInteger(NormalizedDouble value)
    {
        return value._value % 2 != 0;
    }

    public static bool IsPositive(NormalizedDouble value)
    {
        return value._value > 0;
    }

    public static bool IsPositiveInfinity(NormalizedDouble value)
    {
        return double.IsPositiveInfinity(value._value);
    }

    public static bool IsRealNumber(NormalizedDouble value)
    {
        return false;
    }

    public static bool IsSubnormal(NormalizedDouble value)
    {
        return false;
    }

    public static bool IsZero(NormalizedDouble value)
    {
        return value._value == 0;
    }

    public static NormalizedDouble MaxMagnitude(NormalizedDouble x, NormalizedDouble y)
    {
        return Math.Abs(x._value) > Math.Abs(y._value) ? x : y;
    }

    public static NormalizedDouble MaxMagnitudeNumber(NormalizedDouble x, NormalizedDouble y)
    {
        var result = double.MaxMagnitudeNumber(x._value, y._value);
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble MinMagnitude(NormalizedDouble x, NormalizedDouble y)
    {
        var result = Math.Abs(x._value) < Math.Abs(y._value) ? x : y;
        return result;
    }

    public static NormalizedDouble MinMagnitudeNumber(NormalizedDouble x, NormalizedDouble y)
    {
        var result = double.MinMagnitudeNumber(x._value, y._value);
        result = Math.Max(0, Math.Min(1, result));
        return new NormalizedDouble(result);
    }

    public static NormalizedDouble Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
    {
        var value = double.Parse(s.ToString(), style, provider);
        return new NormalizedDouble(value);
    }

    public static NormalizedDouble Parse(string s, NumberStyles style, IFormatProvider? provider)
    {
        var value = double.Parse(s, style, provider);
        return new NormalizedDouble(value);
    }

    public static bool TryConvertFromChecked<TOther>(TOther value, out NormalizedDouble result) where TOther : INumberBase<TOther>
    {
        try
        {
            result = new NormalizedDouble((double) (object) value);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public static bool TryConvertFromSaturating<TOther>(TOther value, out NormalizedDouble result) where TOther : INumberBase<TOther>
    {
        try
        {
            result = new NormalizedDouble((double) (object) value);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public static bool TryConvertFromTruncating<TOther>(TOther value, out NormalizedDouble result) where TOther : INumberBase<TOther>
    {
        try
        {
            result = new NormalizedDouble((double) (object) value);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public static bool TryConvertToChecked<TOther>(NormalizedDouble value, out TOther result) where TOther : INumberBase<TOther>
    {
        try
        {
            result = (TOther) (object) value._value;
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public static bool TryConvertToSaturating<TOther>(NormalizedDouble value, out TOther result) where TOther : INumberBase<TOther>
    {
        try
        {
            result = (TOther) (object) value._value;
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public static bool TryConvertToTruncating<TOther>(NormalizedDouble value, out TOther result) where TOther : INumberBase<TOther>
    {
        try
        {
            result = (TOther) (object) value._value;
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out NormalizedDouble result)
    {
        if (double.TryParse(s.ToString(), style, provider, out var value))
        {
            value = Math.Max(0, Math.Min(1, value));
            result = new NormalizedDouble(value);
            return true;
        }
        else
        {
            result = default;
            return false;
        }
    }

    public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out NormalizedDouble result)
    {
        if (double.TryParse(s, style, provider, out var value))
        {
            result = new NormalizedDouble(value);
            return true;
        }
        else
        {
            result = default;
            return false;
        }
    }


    public static NormalizedDouble AdditiveIdentity => new();
    public static NormalizedDouble MultiplicativeIdentity => new();
    public static NormalizedDouble One => new(1d);
    public static int Radix => 2;
    public static NormalizedDouble Zero => new(0d);
}