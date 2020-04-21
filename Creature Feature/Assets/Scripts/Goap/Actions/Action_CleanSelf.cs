using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_CleanSelf : ActionBase
{

    private IEnumerator Clean;

    public override void WakeUp()
    {
        Reset();
        Clean = WaitforClean();
    }

    public override void GoToSleep()
    {

    }

    public override void RunAction()
    {
        StartCoroutine(Clean);
    }

    private IEnumerator WaitforClean()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2);
            FindObjectOfType<CatStats>().curCleansliness -= 20;
            break;
        }
    }

    public override void Reset()
    {
  
    }
}
