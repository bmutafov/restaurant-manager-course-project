using System;
using UnityEngine;

public class DayCycle : GenericSingletonClass<DayCycle>
{
    #region public_variables
    public static int daysPassedSinceStart = 0;

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
    #endregion

    #region private_variables
    private bool isDay = true;
    private int lastHour;
    #endregion

    #region delegates
    public delegate void OnDayChanged ();
    public OnDayChanged onDayChangedCallback;

    public delegate void OnHourChanged ();
    public OnHourChanged onHourChangedCallback;
    #endregion

    private void Start ()
    {
        // AutoSave function subscribe -> every time day changes
        onDayChangedCallback += Save.OnDayChangeAutoSave;

        lastHour = openingHour;
        gameTime = new DateTime(year: 2017, month: 1, day: 1, hour: openingHour, minute: 0, second: 0);

        if ( Load.Day() )
        {
            gameTime = gameTime.AddDays(daysPassedSinceStart);
        }
        else
        {
            daysPassedSinceStart = 0;
        }

        Load.PersonID();
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
    /// Resets the hour
    /// 
    /// Invokes onDayChanged
    /// 
    /// </summary>
    private void ChangeDay ()
    {
        daysPassedSinceStart++;
        isDay = false;
        TimeSpan newDayTime = new TimeSpan(openingHour, 0, 0);
        gameTime = GameTime.Date.AddDays(1) + newDayTime;

        if ( onDayChangedCallback != null )
            onDayChangedCallback.Invoke();
    }
}
