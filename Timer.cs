using System;

public static class TestTimer
{
    private static DateTime startTime;

    public static void Start()
    {
        startTime = DateTime.Now;
    }

    public static TimeSpan Stop()
    {
        return DateTime.Now - startTime;
    }
}
