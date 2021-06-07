using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringFormatUtil 
{
    public static string GetFormatDecimalString(int value)
    {
        return String.Format("{0:n0}", value);
    }

    public static string GetFormatTimeString(int totalSeconds)
    {
        if (totalSeconds < 0)
        {
            return "-";
        }
        int hours = totalSeconds / 3600;
        int min;
        int sec = totalSeconds % 60;
        if (hours > 0)
        {
            min = (totalSeconds - hours * 3600) / 60;
            return hours.ToString() + ':' + min.ToString("D2") + ":" + sec.ToString("D2");
        }
        else
        {
            min = totalSeconds / 60;
            return min.ToString() + ":" + sec.ToString("D2");
        }
    }


}
