using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DateTimeConvertor", menuName = "NkObjects/DateTimeConvertor", order = 1)]
public class DateTimeConvertor : ScriptableObject
{
    //==== Seconds (int) to string (HH:MM:SS)
    public string ConvertSecIntoString(int totalSeconds)
    {
        string currTime = "";

        int hh = 0, mm = 0;

        if (totalSeconds >= 3600)
        {
            hh = (totalSeconds / 3600);
            totalSeconds -= (3600 * hh);
        }

        if (totalSeconds >= 60)
        {
            mm = (totalSeconds / 60);
            totalSeconds -= (60 * mm);
        }

        currTime = hh.ToString("00") + ":" + mm.ToString("00") + ":" + totalSeconds.ToString("00");
        return currTime;
    }

    //==== String to DateTime
    public DateTime ConverStringIntoDateTime(string str)
    {
        DateTime date = Convert.ToDateTime(str, System.Globalization.CultureInfo.GetCultureInfo("en-IN").DateTimeFormat);
        return date;
    }

    //==== String to Seconds
    public int ConvertStringIntoSec(string time1)
    {
        List<string> splitTime = time1.Split(':').ToList();
        int divisor = 1;
        int totalSeconds = 0;

        for (int i = (splitTime.Count - 1); i >= 0; i--)
        {
            totalSeconds += (int.Parse(splitTime[i]) * divisor);
            divisor *= 60;
        }
        return totalSeconds;
    }

    //==== Compare Two Dates (sting) and return int
    public int CompareDates(string date1,string date2)
    {
        return DateTime.Compare(ConverStringIntoDateTime(date1), ConverStringIntoDateTime(date2));
    }
}
