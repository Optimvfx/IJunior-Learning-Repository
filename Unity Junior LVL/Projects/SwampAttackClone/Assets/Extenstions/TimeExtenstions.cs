using UnityEngine;

public class TimeExtenstions 
{
public static bool IsTimeStoped()
    {
        return Mathf.Approximately(Time.deltaTime, 0);
    }
}
