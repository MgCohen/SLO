using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardEndSignal
{
    public OnBoardEndSignal(bool playerKilled)
    {
        WasPlayerKilled = playerKilled;
    }

    public bool WasPlayerKilled
    {
        get; private set;
    }
}
