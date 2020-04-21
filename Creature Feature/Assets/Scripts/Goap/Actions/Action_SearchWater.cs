using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SearchWater : ActionBase
{
    protected Vector3 TargetLocation;
    private IEnumerator Drink;

    public override void WakeUp()
    {
        Reset();
        Drink = WaitforWater();
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
            if (Vector3.Distance(transform.position, TargetLocation) >= 0.5)
            {
                Character.SetDestination(TargetLocation);
            }
            else
            {
                StartCoroutine(Drink);
            }
        }
    }
    private IEnumerator WaitforWater()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2);
            FindObjectOfType<CatStats>().curThirst -= 40;
            break;
        }
    }

    public override void Reset()
    {
        var character = GetComponent<Character>();
        // pick a location and reset the progress
        TargetLocation = character.ClosestWater();
    }
}
