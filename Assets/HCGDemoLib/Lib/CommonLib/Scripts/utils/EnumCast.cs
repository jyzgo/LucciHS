using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumCast 
{
    public static int ToInt<T>(this T soure) where T : IConvertible//enum
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentException("T must be an enumerated type");

        return (int)(IConvertible)soure;
    }


    public static T ToEnum<T>(this int value,T backup) where T : struct,IConvertible
    {
        if (!Enum.IsDefined(typeof(T), value))
        {
            Debug.LogError("value is out of range using backup value");
            return backup;
        }
        return (T)(object)value;


    }



}
