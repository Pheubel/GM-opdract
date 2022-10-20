using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    public CharacterStateFlag CurrentState => _currentState;
    public float JumpForce => _jumpForce;
    public float MovementSpeed => _movementSpeed;
    public bool IsInAir => (_currentState & CharacterStateFlag.IsGrounded) == 0;
    public bool IsOnGround => (_currentState & CharacterStateFlag.IsGrounded) != 0;


    [SerializeField] private float _jumpForce = 400f;
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _slopeCheckDistance = .3f;
    [Range(0, 180), SerializeField] private float _maxSlopeAngle = 30;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField, Min(0)] float groundCheckRadius = 0.2f;

    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;

    private Rigidbody2D _rigidbody;

    private float _oldSlopeAngle;
    private float _horizontalForce;
    private Vector2 _slopeNormalPerpendicular;

    private CharacterStateFlag _currentState;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var pos = _groundCheck.position;

        HandleGround(pos);
        HandleSlope(pos);
        HandleMovement();
    }

    private void HandleGround(Vector2 position)
    {
        bool isFalling = _rigidbody.velocity.y <= 0.0f;

        if (Physics2D.OverlapCircle(position, groundCheckRadius, _groundMask) && (isFalling || IsOnGround))
        {
            _currentState |= CharacterStateFlag.IsGrounded;
        }
        else
        {
            _currentState &= ~CharacterStateFlag.IsGrounded;
        }

        if (isFalling)
        {
            _currentState &= ~CharacterStateFlag.IsJumping;
        }
    }

    private void HandleSlope(Vector2 position)
    {
        // slope check
        float horizontalSlopeAngle = HandleSlopeHorizontal(position);
        float verticalSlopeAngle = HandleSlopeVertical(position);

        if (verticalSlopeAngle <= _maxSlopeAngle)
        {
            _currentState |= CharacterStateFlag.CanWalkOnSlope | CharacterStateFlag.CanJumpOnSlope;
        }
        else if (horizontalSlopeAngle <= _maxSlopeAngle)
        {
            _currentState |= CharacterStateFlag.CanWalkOnSlope;
            _currentState &= ~CharacterStateFlag.CanJumpOnSlope;
        }
        else
        {
            _currentState &= ~(CharacterStateFlag.CanJumpOnSlope | CharacterStateFlag.CanWalkOnSlope);
        }

        if ((_currentState & CharacterStateFlag.ShouldWalkOnSlope) == CharacterStateFlag.ShouldWalkOnSlope && _horizontalForce == 0.0f)
        {
            _rigidbody.sharedMaterial = fullFriction;
        }
        else
        {
            _rigidbody.sharedMaterial = noFriction;
        }
    }

    private float HandleSlopeHorizontal(Vector2 position)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(position, transform.right, _slopeCheckDistance, _groundMask);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(position, -transform.right, _slopeCheckDistance, _groundMask);

        if (slopeHitFront)
        {
            _currentState |= CharacterStateFlag.IsOnSlope;

            return Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            _currentState |= CharacterStateFlag.IsOnSlope;

            return Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _currentState &= ~CharacterStateFlag.IsOnSlope;

            return 0;
        }
    }

    private float HandleSlopeVertical(Vector2 position)
    {
        var hitInfo = Physics2D.Raycast(position, Vector2.down, _slopeCheckDistance, _groundMask);

        if (hitInfo)
        {
            _slopeNormalPerpendicular = Vector2.Perpendicular(hitInfo.normal).normalized;

            float slopeAngle = Vector2.Angle(hitInfo.normal, Vector2.up);
            if (slopeAngle != _oldSlopeAngle)
            {
                _currentState |= CharacterStateFlag.IsOnSlope;
            }

            _oldSlopeAngle = slopeAngle;

            return slopeAngle;
        }

        return 0;
    }

    private void HandleMovement()
    {
        const CharacterStateFlag notOnSlopeMask = CharacterStateFlag.IsGrounded | CharacterStateFlag.IsOnSlope | CharacterStateFlag.IsJumping;
        const CharacterStateFlag onSlopeMask = notOnSlopeMask | CharacterStateFlag.CanWalkOnSlope;
        const CharacterStateFlag isOnSlope = CharacterStateFlag.IsGrounded | CharacterStateFlag.IsOnSlope | CharacterStateFlag.CanWalkOnSlope;

        if ((_currentState & notOnSlopeMask) == CharacterStateFlag.IsGrounded) // on flat ground
        {
            _rigidbody.velocity = new Vector2(_horizontalForce * _movementSpeed, 0);
        }
        else if ((_currentState & onSlopeMask) == isOnSlope) // on slope
        {
            _rigidbody.velocity = new Vector2(_movementSpeed * _slopeNormalPerpendicular.x * -_horizontalForce, _movementSpeed * _slopeNormalPerpendicular.y * -_horizontalForce);
        }
        else if ((_currentState & CharacterStateFlag.IsGrounded) != CharacterStateFlag.IsGrounded) // in air
        {
            _rigidbody.velocity = new Vector2(_horizontalForce * _movementSpeed, _rigidbody.velocity.y);
        }
    }

    public void Jump()
    {
        if ((_currentState & CharacterStateFlag.CanJump) == CharacterStateFlag.CanJump)
        {
            _currentState |= CharacterStateFlag.IsJumping;
            _currentState &= ~CharacterStateFlag.IsGrounded;

            // Add a vertical force to the character.
            _rigidbody.AddForce(new Vector2(0f, _jumpForce));
        }
    }

    public void ForceJump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);

        _currentState |= CharacterStateFlag.IsJumping;
        _currentState &= ~CharacterStateFlag.IsGrounded;

        // Add a vertical force to the character.
        _rigidbody.AddForce(new Vector2(0f, _jumpForce));
    }

    public void Move(float horizontalForce)
    {
        _horizontalForce = horizontalForce;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, groundCheckRadius);
    }

    [Flags]
    public enum CharacterStateFlag
    {
        IsGrounded = 1,
        IsOnSlope = 2,
        IsJumping = 4,
        CanWalkOnSlope = 8,
        CanJumpOnSlope = 16,

        CanJump = IsGrounded | CanJumpOnSlope,
        ShouldWalkOnSlope = IsOnSlope | CanWalkOnSlope,
    }
}
