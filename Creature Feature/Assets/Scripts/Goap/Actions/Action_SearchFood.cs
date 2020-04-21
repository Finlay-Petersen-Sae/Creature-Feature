using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SearchFood : ActionBase
{
    protected Vector3 TargetLocation;
    private IEnumerator Eat;

    public override void WakeUp()
    {
        Reset();
        Eat = WaitforFood();
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
            if(Vector3.Distance(transform.position, TargetLocation) >= 0.5)
            {
                Character.SetDestination(TargetLocation);
            }
            else
            {
                StartCoroutine(Eat);
            }

        }
    }

    private IEnumerator WaitforFood()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2);
            FindObjectOfType<CatStats>().curHunger -= 40;
            break;
        }
    }

    public override void Reset()
    {
        var character = GetComponent<Character>();
        // pick a location and reset the progress
        TargetLocation = character.ClosestFood();
    }
}
