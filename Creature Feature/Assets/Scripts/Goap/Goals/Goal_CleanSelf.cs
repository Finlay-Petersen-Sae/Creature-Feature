using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_CleanSelf : GoalBase
{
    public void Update()
    {
        Priority = Mathf.Clamp(Priority, 0, 100);
        Priority = FindObjectOfType<CatStats>().curCleansliness * 2; 
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
