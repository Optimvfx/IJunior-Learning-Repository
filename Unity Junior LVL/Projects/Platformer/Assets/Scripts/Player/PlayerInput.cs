using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private readonly string _horizontalAxis = "Horizontal";

    [SerializeField] private KeyCode _jumpButton;
    [SerializeField] private KeyCode _runButton;

    private PlayerMovement _movment;

    private void Start()
    {
        _movment = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_jumpButton))
            _movment.TryJump();

        _movment.SetMoveDirection(Input.GetAxis(_horizontalAxis), Input.GetKey(_runButton));
    }
}
