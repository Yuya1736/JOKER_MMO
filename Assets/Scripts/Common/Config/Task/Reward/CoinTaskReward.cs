public class CoinTaskReward : TaskRewardBase
{
    public int coinCount;

    public override void ConverFromString(string stringValue)
    {
        coinCount = int.Parse(stringValue);
    }
}
