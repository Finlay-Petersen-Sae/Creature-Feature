using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAP : MonoBehaviour
{
    private List<GoalBase> Goals;
    private List<ActionBase> Actions;

    private GoalBase ActiveGoal;

    // Start is called before the first frame update
    void Start()
    {
        Goals = new List<GoalBase>(GetComponents<GoalBase>());
        Actions = new List<ActionBase>(GetComponents<ActionBase>());
    }

    // Update is called once per frame
    void Update()
    {
        DetermineGoal();

        if (ActiveGoal)
            ActiveGoal.RunGoal();
    }

    void DetermineGoal()
    {
        GoalBase bestGoal = null;
        ActionBase bestAction = null;

        // find the highest priority goal that can run
        foreach(var goal in Goals)
        {
            // can't run - do nothing
            if (!goal.CanRun())
                continue;

            // pick the best goal
            if ((bestGoal == null) || (goal.Priority > bestGoal.Priority))
            {
                bestAction = null;

                // find a matching action
                foreach(var action in Actions)
                {
                    if (action.CanSatisfy.Contains(goal.UniqueID))
                    {
                        bestAction = action;
                        break;
                    }
                }

                if (bestAction != null)
                    bestGoal = goal;
            }
        }

        if (bestGoal == null)
        {
            Debug.LogError("[GOAP] Failed to find new best goal for " + gameObject.name);
            return;
        }

        // is this a new best goal
        if (ActiveGoal != bestGoal)
        {
            if (ActiveGoal != null)
                ActiveGoal.GoToSleep();

            ActiveGoal = bestGoal;
            ActiveGoal.ChangeAction(bestAction);

            bestGoal.WakeUp();
        }
        else if (bestAction != bestGoal.ActiveAction)
        {
            // could switch if better goal
        }
    }
}
