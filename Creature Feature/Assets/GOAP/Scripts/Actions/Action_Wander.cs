using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Wander : ActionBase
{
    protected Vector3 StartLocation;
    protected Vector3 TargetLocation;

    public float LocationReachedThreshold = 0.1f;
    public float MovementTime = 5f;
    protected float MovementProgress = -1f;

    public override void WakeUp()
    {
        Reset();
    }

    public override void GoToSleep()
    {

    }

    public override void RunAction()
    {
        MovementProgress += Time.deltaTime / MovementTime;
        transform.position = Vector3.Lerp(StartLocation, TargetLocation, MovementProgress);

        if (Vector3.Distance(transform.position, TargetLocation) <= LocationReachedThreshold)
            Reset();
    }

    public override void Reset()
    {
        // pick a location and reset the progress
        StartLocation = transform.position;
        TargetLocation = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        MovementProgress = 0f;
    }
}
