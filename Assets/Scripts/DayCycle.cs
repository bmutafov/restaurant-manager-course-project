using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : GenericSingletonClass<DayCycle>
{
    public float daySpeed = 5;
    public int closingHour = 22;
    public int openingHour = 14;

    private DateTime gameTime;

    public DateTime GameTime
    {
        get
        {
            return gameTime;
        }
    }

    public delegate void OnDayChanged ();
    public OnDayChanged onDayChangedCallback;

    public delegate void OnHourChanged ();
    public OnHourChanged onHourChangedCallback;

    private bool isDay = true;
    private int lastHour;

    private void Start ()
    {
        lastHour = openingHour;
        gameTime = new DateTime(year: 2017, month: 1, day: 1, hour: openingHour, minute: 0, second: 0);
    }

    private void Update ()
    {
        if ( isDay )
        {
            gameTime = gameTime.AddMinutes(Time.deltaTime * daySpeed);

            if ( GameTime.Hour != lastHour )
            {
                lastHour = GameTime.Hour;
                if ( onHourChangedCallback != null )
                    onHourChangedCallback.Invoke();
            }

            if ( GameTime.Hour == closingHour )
            {
                ChangeDay();
            }
        }
    }

    /// <summary>
    /// When the hour equals the closing hour
    /// 
    /// Stops the timer
    /// </summary>
    private void ChangeDay ()
    {
        isDay = false;
        if ( onDayChangedCallback != null )
            onDayChangedCallback.Invoke();

        TimeSpan newDayTime = new TimeSpan(openingHour, 0, 0);
        gameTime = GameTime.Date.AddDays(1) + newDayTime;
    }
}
