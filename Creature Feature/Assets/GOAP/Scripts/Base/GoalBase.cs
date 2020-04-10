using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBase : MonoBehaviour
{
    [Range(0, 100)]
    public int Priority = 0;
    public string UniqueID;

    public ActionBase ActiveAction;

    public virtual bool CanRun()
    {
        return true;
    }

    public virtual void RunGoal()
    {
        ActiveAction.RunAction();
    }

    public virtual void ChangeAction(ActionBase newAction)
    {
        ActiveAction = newAction;
        ActiveAction.Reset();
    }

    public virtual void WakeUp()
    {
        ActiveAction.WakeUp();
    }

    public virtual void GoToSleep()
    {
        ActiveAction.GoToSleep();
    }
}
