using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerStateMachine : MonoBehaviour
{
    [SerializeField] private WorkerState _firstState;
    


    private WorkerState _currentState;
    protected PlayZone PlayZone { get; private set; }
    protected Animator Animator{ get; private set; }

    public bool IsWorking { get; protected set; }
    public bool IsEating { get; protected set; }
    public bool IsSleeping { get; protected set; }

    private void Awake()
    {
        PlayZone = GetComponentInParent<PlayZone>();
        Animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Transit(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;
        WorkerState nextState = _currentState.GetNextState();

        if(nextState != null)
        {
            Transit(nextState);
        }
    }

    private void Transit(WorkerState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();
        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }
}
