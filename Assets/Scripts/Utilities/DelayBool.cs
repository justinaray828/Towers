using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBool
{

    public bool delayBoolState;
    public float delayTime;

    private bool defaultState = true;
    private float defaultTime = 0.5f;

    /// <summary>
    /// Default DelayBool in true state with a delay time of 0.5 seconds
    /// </summary>
    public DelayBool()
    {
        delayBoolState = defaultState;
        delayTime = defaultTime;
    }

    /// <summary>
    /// DelayBool in true state with one float parameter for the delay time in seconds
    /// </summary>
    /// <param name="delayBoolTime"></param>
    public DelayBool(float delayBoolTime)
    {
        delayBoolState = defaultState;
        delayTime = delayBoolTime;
    }

    /// <summary>
    /// Delay bool with one bool of the current state and one float parameter for the delay time in seconds
    /// </summary>
    /// <param name="boolState"></param>
    /// <param name="delayBoolTime"></param>
    public DelayBool(bool boolState, float delayBoolTime)
    {
        delayBoolState = boolState;
        delayTime = delayBoolTime;
    }

}

