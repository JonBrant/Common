using System;
using System.Collections.Generic;
using UnityEngine;


[Flags]
public enum Weekday : short {
    Monday = 1,
    Tuesday = 2,
    Wednesday = 4,
    Thursday = 8,
    Friday = 16,
    Saturday = 32,
    Sunday = 64
};

[Flags] 
public enum TimeOfDay {
    Hour0 = 1,
    Hour1 = 4,
    Hour2 = 8,
    Hour3 = 16,
    Hour4 = 32,
    Hour5 = 64,
    Hour6 = 128,
    Hour7 = 256,
    Hour8 = 512,
    Hour9 = 1024,
    Hour10 = 2048,
    Hour11 = 4096,
    Hour12 = 8192,
    Hour13 = 16384,
    Hour14 = 32768,
    Hour15 = 262144,
    Hour16 = 524288,
    Hour17 = 1048576,
    Hour18 = 2097152,
    Hour19 = 4194304,
    Hour20 = 8388608,
    Hour21 = 16777216,
    Hour22 = 33554432,
    Hour23 = 67108864
}

[Serializable]
public struct TimeAndDate {
    public Weekday Days;
    public TimeOfDay Times;
}

public class TimesAndDates : MonoBehaviour {
    public List<TimeAndDate> TimesAndDatesList = new List<TimeAndDate>();
}