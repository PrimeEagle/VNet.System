using System.Numerics;

namespace VNet.System.Conversion;

public static class GenericNumber<T> where T : INumber<T>
{
    public static byte ToByte(T value)
    {
        return (byte)(Convert.ChangeType(value, typeof(byte)) ?? throw new InvalidOperationException());
    }

    public static sbyte ToSbyte(T value)
    {
        return (sbyte)(Convert.ChangeType(value, typeof(sbyte)) ?? throw new InvalidOperationException());
    }

    public static ushort ToUshort(T value)
    {
        return (ushort)(Convert.ChangeType(value, typeof(ushort)) ?? throw new InvalidOperationException());
    }

    public static short ToShort(T value)
    {
        return (short)(Convert.ChangeType(value, typeof(short)) ?? throw new InvalidOperationException());
    }

    public static uint ToUint(T value)
    {
        return (uint)(Convert.ChangeType(value, typeof(uint)) ?? throw new InvalidOperationException());
    }

    public static int ToInt(T value)
    {
        return (int)(Convert.ChangeType(value, typeof(int)) ?? throw new InvalidOperationException());
    }

    public static nuint ToNuint(T value)
    {
        return (nuint)(Convert.ChangeType(value, typeof(nuint)) ?? throw new InvalidOperationException());
    }

    public static nint ToNint(T value)
    {
        return (nint)(Convert.ChangeType(value, typeof(nint)) ?? throw new InvalidOperationException());
    }

    public static long ToLong(T value)
    {
        return (long)(Convert.ChangeType(value, typeof(long)) ?? throw new InvalidOperationException());
    }

    public static ulong ToUlong(T value)
    {
        return (ulong)(Convert.ChangeType(value, typeof(ulong)) ?? throw new InvalidOperationException());
    }

    public static float ToFloat(T value)
    {
        return (float)(Convert.ChangeType(value, typeof(float)) ?? throw new InvalidOperationException());
    }

    public static double ToDouble(T value)
    {
        return (double)(Convert.ChangeType(value, typeof(double)) ?? throw new InvalidOperationException());
    }

    public static decimal ToDecimal(T value)
    {
        return (decimal)(Convert.ChangeType(value, typeof(decimal)) ?? throw new InvalidOperationException());
    }

    public static T FromByte(byte value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromSbyte(sbyte value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromUshort(ushort value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromShort(short value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromUint(uint value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromInt(int value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromNuint(nuint value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromNint(nint value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromLong(long value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromUlong(ulong value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromFloat(float value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromDouble(double value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }

    public static T FromDecimal(decimal value)
    {
        return (T)(Convert.ChangeType(value, typeof(T)) ?? throw new InvalidOperationException());
    }
}