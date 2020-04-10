using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase : MonoBehaviour
{
    public int Cost = 0;

    public List<string> CanSatisfy;

    public virtual void WakeUp()
    {
        // if we want the actions to reset every time they wakeup uncomment below
        //Reset();
    }

    public virtual void GoToSleep()
    {

    }

    public virtual void RunAction()
    {

    }

    public virtual void Reset()
    {

    }

    public bool IsComplete()
    {
        return false;
    } 
}
