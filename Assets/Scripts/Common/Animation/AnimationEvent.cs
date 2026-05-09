public static class AnimationEvent
{
    private const string idle = "Idle";
    private const string move = "Move";
    private const string jump = "JumpStart";
    private const string airDown = "JumpAirDown";
    private const string damage = "Damage";
    private const string attack = "Attack";
    private const string die = "Die";
    private const string equip = "Equip";

    public static string Idle => idle;
    public static string Move => move;
    public static string Jump => jump;
    public static string AirDown => airDown;
    public static string Damage => damage;
    public static string Attack => attack;
    public static string Die => die;
    public static string Equip => equip;

}