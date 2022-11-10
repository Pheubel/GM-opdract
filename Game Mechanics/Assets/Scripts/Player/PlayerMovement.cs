using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    bool _controlsEnabled;

    CharacterController2D _characterController;
    PlayerShoot _playerShoot;
    Animator _animator;

    GameControls _controls;
    InputAction _moveAction;
    InputAction _jumpAction;

    bool _previousJumpPressed;
    int _doubleJumpsUsed;
    [SerializeField] int _maxDoubleJumps;

    [SerializeField] UnityEvent _onDoubleJump;

    private void Start()
    {
        _characterController = GetComponent<CharacterController2D>();
        _playerShoot = GetComponent<PlayerShoot>();
        _animator = GetComponent<Animator>();
        _controls = new GameControls();

        _controls.Enable();

        _moveAction = _controls.Platforming.Move;
        _jumpAction = _controls.Platforming.Jump;
    }

    private void Update()
    {
        //if (!_controlsEnabled)
        //    return;

        var moveInput = _moveAction.ReadValue<float>();
        var jumpPressed = _jumpAction.IsPressed();

        _characterController.Move(moveInput);
        _animator.SetFloat("speed", moveInput);

        HandleJumps(jumpPressed);
    }

    private void HandleJumps(bool jumpPressed)
    {
        if (jumpPressed != _previousJumpPressed)
        {
            // state change

            if (jumpPressed)
            {
                // pressed jump button

                // is grounded?
                if (_characterController.IsOnGround)
                {
                    _doubleJumpsUsed = 0;
                    _characterController.Jump();
                }
                else if (_doubleJumpsUsed < _maxDoubleJumps)
                {
                    _onDoubleJump?.Invoke();

                    _doubleJumpsUsed++;
                    _characterController.ForceJump();
                    _playerShoot.JumpShoot();
                }
            }
            else
            {
                // released jump button
            }
        }

        _previousJumpPressed = jumpPressed;
    }

    public void SetMaxDoubleJumps(int value) =>
        _maxDoubleJumps = value;

    public void EnableControls() =>
        _controls.Enable();

    public void DisableControls() =>
        _controls.Disable();
}
