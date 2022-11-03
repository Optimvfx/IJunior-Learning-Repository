using UnityEngine;

[RequireComponent(typeof(Folower))]
public class PlayerFolower : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerSaw _firstSpectable;
    [SerializeField] private PlayerSaw _secondSpectable;

    private Folower _folower;

    private void Awake()
    {
        _folower = GetComponent<Folower>();
    }

    private void OnEnable()
    {
        _playerMover.OnChangeState += OnChangeState;
    }

    private void OnDisable()
    {
        _playerMover.OnChangeState -= OnChangeState;
    }

    private void OnChangeState(PlayerMover.CurrentState state)
    {
        if (state == PlayerMover.CurrentState.RotateAroundFirst)
        {
            _folower.Init(_firstSpectable.transform);
        }
        else
        {
            _folower.Init(_secondSpectable.transform);
        }
    }
}
