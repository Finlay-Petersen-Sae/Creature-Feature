using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_RandWander : GoalBase
{
    public void Update()
    {
        var catstats = FindObjectOfType<CatStats>();
        if (catstats.curHunger > 75 || catstats.curThirst > 75 || catstats.curCleansliness > 45)
        {
               Priority = Mathf.Clamp(Priority, 0, 100);
        Priority = (catstats.curHunger + catstats.curThirst + catstats.curCleansliness) / 6;
        }
        else
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
