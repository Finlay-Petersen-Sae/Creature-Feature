using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_SearchHuman : GoalBase
{
    public void Update()
    {
        Priority = Mathf.Clamp(Priority, 0, 100);
        if (GetComponent<CatStats>().LookingForHuman)
        {
            Priority = 100;
        }
    }

    public override bool CanRun()
    {
        return base.CanRun();
    }

    public override void WakeUp()
    {
        base.WakeUp();
    }

    public override void GoToSleep()
    {
        base.GoToSleep();
    }
}
