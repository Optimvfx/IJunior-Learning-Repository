using UnityEngine;

[RequireComponent(typeof(ReadOnlyEnemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private ReadOnlyEnemy _self;

    private State _currentState;

    public State Current => _currentState;

    private void Awake()
    {
        _self = GetComponent<ReadOnlyEnemy>();

        StopStateMachine(_self);
    }

    private void OnEnable()
    {
        _self.Dying += StopStateMachine;
    }

    private void OnDisable()
    {
        _self.Dying -= StopStateMachine;
    }

    private void Start()
    {
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(State startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter();
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }

    private void StopStateMachine(ReadOnlyEnemy enemy)
    {
        _currentState = null;

        foreach (var state in GetComponents<State>())
            state.enabled = false;

        foreach (var transition in GetComponents<Transition>())
            transition.enabled = false;
    }    
}