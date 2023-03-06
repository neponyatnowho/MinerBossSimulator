using System;
using UnityEngine;

public static class FormatNumsHelper
{
    private static string[] names = new[]
    {
        "",
        "K",
        "M",
        "B",
        "t",
        "q",
    };

    public static string FormatNum(ulong num)
    {
        if (num == 0) return "0";
        int drop = 0;
        int i = 0;



        while (i+1 < names.Length && num >= 1000)
        {
            drop = (int)num % 1000;
            drop /= 100;
            num /= 1000;
            i++;
        }

        if(i > 0)
        {
            return num.ToString() + "." + drop.ToString() + names[i];
        }
        
        return num.ToString() + names[i];
    }

    public static string FormatNum(int num)
    {
        if (num == 0) return "0";
        int drop = 0;
        int i = 0;



        while (i + 1 < names.Length && num >= 1000)
        {
            drop = (int)num % 1000;
            drop /= 100;
            num /= 1000;
            i++;
        }

        if (i > 0)
        {
            return num.ToString() + "." + drop.ToString() + names[i];
        }

        return num.ToString() + names[i];
    }

    public static string FormatNum(float num)
    {
        if (num == 0) return "0";
        int i = 0;



        while (i + 1 < names.Length && num >= 1000)
        {
            num /= 1000;
            i++;
        }

        if (i > 1)
        {
            return num.ToString(format: "##.#") + names[i];
        }

        return num.ToString(format: "0.#") + names[i];
    }

}
