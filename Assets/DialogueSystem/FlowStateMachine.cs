using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FlowStateMachine : MonoBehaviour
{
    [SerializeField]
    private FlowChannel channel;
    [SerializeField]
    private FlowState startupState;

    private FlowState _currentState;
    public FlowState CurrentState => _currentState;

    private static FlowStateMachine _instance;
    public static FlowStateMachine Instance => _instance;

    private void Awake()
    {
        _instance = this;

        channel.OnFlowStateRequested += SetFlowState;
    }

    private void Start()
    {
        SetFlowState(startupState);
    }

    private void OnDestroy()
    {
        channel.OnFlowStateRequested -= SetFlowState;

        _instance = null;
    }

    private void SetFlowState(FlowState state)
    {
        if (_currentState != state)
        {
            _currentState = state;
            channel.RaiseFlowStateChanged(_currentState);
        }
    }
}
