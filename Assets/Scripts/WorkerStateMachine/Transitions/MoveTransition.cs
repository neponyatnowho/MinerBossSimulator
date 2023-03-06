using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransition : WorkerTransition
{
    private WorkState _workState;
    private EatState _eatState;
    private SleepState _sleepState;
    private Worker _worker;
    private void Awake()
    {
        _sleepState = GetComponent<SleepState>();   
        _eatState = GetComponent<EatState>();
        _workState = GetComponent<WorkState>();
        _worker = GetComponent<Worker>();
    }
    private void Update()
    {
        if (_eatState.IsEating || _sleepState.IsSleeping)
            return;

        
        if(_worker.Satiety < 0.1f && !_eatState.IsEating)
            NeedTransit = true;
        if (_worker.Energy < 0.1f && !_sleepState.IsSleeping)
            NeedTransit = true;
        else if(_worker.Satiety > 0.1f && _worker.Energy > 0.1f && !_workState.IsWorking)
            NeedTransit = true;
    }


}
