using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Money
{
    private static string[] units = new string[] { "", "K", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc", "UDc", "DDc", "TDc", "QaDc", "QiDc", "SxDc", "SpDc", "ODc", "NDc", "Vg", "UVg", "DVg", "TVg", "QaVg", "QiVg", "SxVg", "SpVg", "OVg", "NVg", "Tg", "UTg", "DTg", "TTg", "QaTg", "QiTg", "SxTg", "SpTg", "OTg", "NTg", "Qd", "UQd", "DQd", "TQd", "QaQd", "QiQd", "SxQd", "SpQd", "OQd", "NQd", "Qq", "UQq", "DQq", "TQq", "QaQq", "QiQq", "SxQq", "SpQq", "OQq", "NQq", "Sx", "USx", "DSx", "TSx", "QaSx", "QiSx", "SxSx", "SpSx", "OSx", "NSx", "Sp", "USp", "DSp", "TSp", "QaSp", "QiSp", "SxSp", "SpSp", "OSp", "NSp", "Og", "UOg", "DOg", "TOg", "QaOg", "QiOg", "SxOg", "SpOg", "OOg", "NOg", "Nn", "UNn", "DNn", "TNn", "QaNn", "QiNn", "SxNn", "SpNn", "ONn", "NNn", "Ce" };
    private static double unitDiff = 10 ^ 3;
    private static string currency = "€";

    private double value;
    private int unit;

    public Money() : this(0, 0)
    {
    }

    public Money(double value) : this(value, 0)
    {
    }

    public Money(double value, int unit)
    {
        this.value = value;
        this.unit = unit;

        this.Normalize();
    }

    private void Normalize()
    {
        while (this.value >= Money.unitDiff)
        {
            this.value /= Money.unitDiff;
            this.unit++;
        }
        while (this.value < 1 && this.unit > 0)
        {
            this.value *= Money.unitDiff;
            this.unit--;
        }
    }

    public double GetValue()
    {
        return value;
    }

    public int GetUnit()
    {
        return unit;
    }

    public override string ToString()
    {
        return this.value.ToString("0.##") + Money.units[this.unit] + Money.currency;
    }

    public static Money operator +(Money a, Money b)
    {
        if (a.unit == b.unit)
        {
            return new Money(a.value + b.value, a.unit);
        }
        else if (a.unit < b.unit)
        {
            return new Money(a.value + b.value * Money.unitDiff * (b.unit - a.unit), a.unit);
        }
        else
        {
            return new Money(a.value * Money.unitDiff * (a.unit - b.unit) + b.value, b.unit);
        }
    }

    public static Money operator -(Money a, Money b)
    {
        if (a.unit == b.unit)
        {
            return new Money(a.value - b.value, a.unit);
        }
        else if (a.unit < b.unit)
        {
            return new Money(a.value - b.value * Money.unitDiff * (b.unit - a.unit), a.unit);
        }
        else
        {
            return new Money(a.value * Money.unitDiff * (a.unit - b.unit) - b.value, b.unit);
        }
    }

    public static Money operator *(Money a, double b)
    {
        return new Money(a.value * b, a.unit);
    }

    public static Money operator *(double a, Money b)
    {
        return new Money(a * b.value, b.unit);
    }

    public static Money operator /(Money a, double b)
    {
        return new Money(a.value / b, a.unit);
    }

    public static bool operator ==(Money a, Money b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Money a, Money b)
    {
        return !a.Equals(b);
    }

    public static bool operator >(Money a, Money b)
    {
        Money c = a - b;
        return c.value > 0 && c.unit >= 0;
    }

    public static bool operator <(Money a, Money b)
    {
        Money c = a - b;
        return c.value < 0 && c.unit <= 0;
    }

    public static bool operator >=(Money a, Money b)
    {
        Money c = a - b;
        return c.value >= 0 && c.unit >= 0;
    }

    public static bool operator <=(Money a, Money b)
    {
        Money c = a - b;
        return c.value <= 0 && c.unit <= 0;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        Money other = obj as Money;
        if (other == null) return false;
        return this.value == other.value && this.unit == other.unit;
    }

    public override int GetHashCode()
    {
        return this.value.GetHashCode() ^ this.unit.GetHashCode();
    }

    public static Money Parse(string s)
    {
        try
        {
            double value = int.Parse(s);
            s = s.Replace(value.ToString(), "");
            return new Money(value, Array.IndexOf(Money.units, s));
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return new Money();
        }
    }
}