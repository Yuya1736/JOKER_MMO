using UnityEngine;

/// <summary>
/// Local owner-side ground movement prediction.
/// </summary>
public class PlayerPredictionClient
{
    private readonly PlayerController owner;
    private readonly CharacterController characterController;
    private readonly PlayerInputCommand[] inputBuffer;
    private readonly PlayerStateSnapshot[] stateBuffer;
    private readonly int bufferMask;

    private PlayerMoveMotor.MotorState motorState;
    private uint currentTick;

    // Reused send buffer — avoids per-tick allocation.
    private const int InputRedundancy = 3;
    private readonly PlayerInputCommand[] sendBuffer = new PlayerInputCommand[InputRedundancy];

    public uint CurrentTick => currentTick;

    public PlayerPredictionClient(PlayerController owner, CharacterController characterController, int bufferSize = 128)
    {
        this.owner = owner;
        this.characterController = characterController;

        int size = Mathf.NextPowerOfTwo(Mathf.Max(32, bufferSize));
        inputBuffer = new PlayerInputCommand[size];
        stateBuffer = new PlayerStateSnapshot[size];
        bufferMask = size - 1;
    }

    public void Tick(PlayerInputCommand input)
    {
        currentTick = input.Tick;
        int index = (int)(currentTick & (uint)bufferMask);
        inputBuffer[index] = input;
        stateBuffer[index] = Simulate(input);
    }

    public void SendInput()
    {
        int count = (int)Mathf.Min(currentTick, InputRedundancy);
        for (int i = 0; i < count; i++)
        {
            uint tick = currentTick - (uint)(count - 1 - i);
            TryGetInput(tick, out sendBuffer[i]);
        }

        NetMessageManager.Instance.SendMessageToServer(
            NetMessageType.C2S_InputBatch,
            new C2S_InputBatch { commands = sendBuffer, count = count });
    }

    public bool TryGetInput(uint tick, out PlayerInputCommand input)
    {
        input = inputBuffer[(int)(tick & (uint)bufferMask)];
        return input.Tick == tick;
    }

    public bool TryGetState(uint tick, out PlayerStateSnapshot snapshot)
    {
        snapshot = stateBuffer[(int)(tick & (uint)bufferMask)];
        return snapshot.Tick == tick;
    }

    public void ApplyAuthoritativeSnapshot(PlayerStateSnapshot snapshot)
    {
        owner.transform.position = snapshot.Position;
        owner.transform.rotation = Quaternion.Euler(0f, snapshot.Yaw, 0f);
        motorState.Velocity = snapshot.Velocity;
        motorState.IsGrounded = snapshot.IsGrounded;
    }

    public void Replay(uint fromTick, uint toTick)
    {
        for (uint tick = fromTick; tick <= toTick; tick++)
        {
            if (!TryGetInput(tick, out PlayerInputCommand input))
            {
                continue;
            }

            stateBuffer[(int)(tick & (uint)bufferMask)] = Simulate(input);
        }
    }

    private PlayerStateSnapshot Simulate(PlayerInputCommand input)
    {
        return PlayerMoveMotor.SimulateGroundMove(
            characterController,
            owner.transform,
            owner.OwnerClientId,
            input,
            ref motorState,
            PlayerMoveMotor.MoveSpeed,
            PlayerMoveMotor.SprintSpeed,
            PlayerMoveMotor.JumpSpeed,
            PlayerMoveMotor.Gravity,
            PlayerMoveMotor.TickDeltaTime);
    }
}
