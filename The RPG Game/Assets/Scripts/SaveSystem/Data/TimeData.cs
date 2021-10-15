using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimeData
{
    public float time;

    public TimeData(DayNightCycle cycle) {
        time = cycle.time;
    }

}
