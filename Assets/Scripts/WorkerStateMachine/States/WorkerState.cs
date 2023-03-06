using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkerState : WorkerStateMachine
{
    [SerializeField] private WorkerTransition[] _transitions;
    
    protected bool IsMoving;

    public void Enter()
    {
        if (enabled == false)
        {
            enabled = true;
            foreach(var transition in _transitions)
            {
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {
        IsMoving = false;
        IsWorking = false;

        if (enabled == true)
        {
            foreach (WorkerTransition transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
        StopAllCoroutines();
    }

    public WorkerState GetNextState()
    {
        foreach(WorkerTransition transition in _transitions)
        {
            if(transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }
        return null;
    }

    private void OnEnable()
    {
        Enable();
    }

    protected abstract void Enable();
}
