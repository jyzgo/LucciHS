using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DateUtil 
{

    public static int currentDay = (DateTime.Now - new DateTime(1970, 1, 1)).Days;
    public static int currentWeek = (currentDay + 4) / 7; //1970 1 1 is a thursday
    public static int currentMonth = (DateTime.Now.Year - 1970) * 12 + DateTime.Now.Month;
    public static DateTime the1stOfThisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    public static DateTime thelast7DayOfLastMonth = the1stOfThisMonth - new TimeSpan(7, 0, 0, 0);

}
