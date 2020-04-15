using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SearchHuman : ActionBase
{
    protected Vector3 TargetLocation;

    public override void WakeUp()
    {
        Reset();
    }

    public override void GoToSleep()
    {

    }

    public override void RunAction()
    {
        var Character = GetComponent<Character>();
        if (Character.ReachedDestination && Character.RefDestination == TargetLocation)
        {
            Reset();
        }
        else
        {
            Character.SetDestination(TargetLocation);
        }
    }

    public override void Reset()
    {
        var character = GetComponent<Character>();
        // pick a location and reset the progress
        TargetLocation = character.ClosestHuman();
    }
}
