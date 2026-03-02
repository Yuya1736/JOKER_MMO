public static class AnimationEvent
{
    private const string idle = "Idle";
    private const string move = "Move";
    private const string jump = "JumpStart";
    private const string airDown = "JumpAirDown";

    public static string Idle => idle;
    public static string Move => move;
    public static string Jump => jump;
    public static string AirDown => airDown;
}