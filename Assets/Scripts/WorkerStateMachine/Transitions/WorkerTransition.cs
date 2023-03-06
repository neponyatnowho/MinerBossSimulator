using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkerTransition : WorkerStateMachine
{
    [SerializeField] private WorkerState _targetState;
    
    public WorkerState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

}
