using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Passenger
{
    //private BusStopScript.BusStopType origin; // a origem � via inspector
    public BusStopScript.BusStopType destiny;
    public int Cash;
}
