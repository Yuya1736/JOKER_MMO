using UnityEngine;

/// <summary>
/// Shared CharacterController-based movement solver for prediction and authority.
/// </summary>
public static class PlayerMoveMotor
{
    public const byte JumpButtonMask = 1 << 0;
    public const byte SprintButtonMask = 1 << 1;
    public const byte AttackButtonMask = 1 << 2;

    public const float MoveSpeed = 2f;
    public const float SprintSpeed = 3f;
    public const float JumpSpeed = 6f;
    public const float Gravity = 10f;
    public const float TickDeltaTime = 1f / 30f;

    public struct MotorState
    {
        public Vector3 Velocity;
        public bool IsGrounded;
    }

    public static PlayerStateSnapshot SimulateGroundMove(
        CharacterController characterController,
        Transform targetTransform,
        ulong clientId,
        PlayerInputCommand input,
        ref MotorState motorState,
        float moveSpeed,
        float sprintSpeed,
        float jumpSpeed,
        float gravity,
        float deltaTime)
    {
        Vector3 moveDir = new Vector3(input.MoveDir.x, 0f, input.MoveDir.y);
        if (moveDir.sqrMagnitude > 1f)
        {
            moveDir.Normalize();
        }

        float speed = ((input.Buttons & SprintButtonMask) != 0) ? sprintSpeed : moveSpeed;
        Vector3 planarVelocity = moveDir * speed;

        motorState.Velocity.x = planarVelocity.x;
        motorState.Velocity.z = planarVelocity.z;

        if (characterController.isGrounded)
        {
            motorState.IsGrounded = true;
            if (motorState.Velocity.y < 0f)
            {
                motorState.Velocity.y = -2f;
            }

            if ((input.Buttons & JumpButtonMask) != 0)
            {
                motorState.Velocity.y = jumpSpeed;
                motorState.IsGrounded = false;
            }
        }
        else
        {
            motorState.IsGrounded = false;
            motorState.Velocity.y -= gravity * deltaTime;
        }

        CollisionFlags flags = characterController.Move(motorState.Velocity * deltaTime);
        if ((flags & CollisionFlags.Below) != 0)
        {
            motorState.IsGrounded = true;
            if (motorState.Velocity.y < 0f)
            {
                motorState.Velocity.y = -2f;
            }
        }

        targetTransform.rotation = Quaternion.Euler(0f, input.Yaw, 0f);

        PlayerState state;
        if (!motorState.IsGrounded)
        {
            state = motorState.Velocity.y > 0f ? PlayerState.Jump : PlayerState.AirDown;
        }
        else
        {
            state = moveDir.sqrMagnitude > 0.0001f ? PlayerState.Move : PlayerState.Idle;
        }

        return new PlayerStateSnapshot
        {
            ClientId = clientId,
            Tick = input.Tick,
            Position = targetTransform.position,
            Velocity = motorState.Velocity,
            Yaw = input.Yaw,
            State = state,
            LastProcessedInputTick = input.Tick,
            IsGrounded = motorState.IsGrounded
        };
    }
}
