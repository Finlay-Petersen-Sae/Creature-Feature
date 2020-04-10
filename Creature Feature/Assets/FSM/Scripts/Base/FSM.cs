using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public List<BaseState> AllStates;
    public BaseState InitialState;

    protected BaseState CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = InitialState;

        // initialise all of the states
        foreach(BaseState state in AllStates)
        {
            state.State_Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check all of the transitions for the state
        BaseState nextState = CurrentState.CheckTransitions();

        // have a new state?
        if (nextState != null)
        {
            CurrentState.State_Exit();

            CurrentState = nextState;

            CurrentState.State_Enter();
        }

        CurrentState.State_Update();
    }
}
