using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay
{
    private float delayTime = 0.5f; //Time in seconds to delay
    private float currentTime = 0f;
    private bool firstCall = true;

    /// <summary>
    /// Initializes Delay with default values.
    /// delayTime = 0.5f
    /// </summary>
    public Delay()
    {
        
    }

    /// <summary>
    /// Input float for amount of time to delay in seconds.
    /// </summary>
    /// <param name="timeToWait"></param>
    public Delay(float timeToWait)
    {
        delayTime = timeToWait;
    }

    /// <summary>
    /// Sets currentTime to 0 and returns true on first call
    /// </summary>
    public void ResetDelay()
    {
        firstCall = true;
        currentTime = 0f;
    }

    /// <summary>
    /// Returns true when delay is over or on the first call. Only works if called in Update.
    /// </summary>
    /// <returns></returns>
    public bool CallDelay()
    {
        IncreaseTime();

        if(currentTime >= delayTime || firstCall)
        {
            ResetTime();
            firstCall = false;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns true when delay is over or on the first call. Only works if called in FixedUpdate.
    /// </summary>
    /// <returns></returns>
    public bool FixedCallDelay()
    {
        IncreaseTime();

        if (currentTime >= delayTime || firstCall)
        {
            ResetTime();
            firstCall = false;
            return true;
        }

        return false;
    }

    private void IncreaseTime()
    {
        currentTime += Time.deltaTime;
    }

    private void IncreaseFixedTime()
    {
        currentTime += Time.fixedDeltaTime;
    }

    private void ResetTime()
    {
        currentTime = 0;
    }
}
