using UnityEngine;

/// <summary>
/// Owner-side reconciliation for authoritative ground movement snapshots.
/// </summary>
public class PlayerReconciliationClient
{
    private readonly PlayerPredictionClient predictionClient;
    private readonly float positionErrorThreshold;
    private readonly float yawErrorThreshold;

    public PlayerReconciliationClient(
        PlayerPredictionClient predictionClient,
        float positionErrorThreshold = 0.1f,
        float yawErrorThreshold = 3f)
    {
        this.predictionClient = predictionClient;
        this.positionErrorThreshold = positionErrorThreshold;
        this.yawErrorThreshold = yawErrorThreshold;
    }

    public void Reconcile(PlayerStateSnapshot authoritative)
    {
        if (!predictionClient.TryGetState(authoritative.Tick, out PlayerStateSnapshot predicted))
        {
            return;
        }

        float positionError = Vector3.Distance(predicted.Position, authoritative.Position);
        float yawError = Mathf.Abs(Mathf.DeltaAngle(predicted.Yaw, authoritative.Yaw));
        if (positionError <= positionErrorThreshold && yawError <= yawErrorThreshold)
        {
            return;
        }

        predictionClient.ApplyAuthoritativeSnapshot(authoritative);
        predictionClient.Replay(authoritative.LastProcessedInputTick + 1, predictionClient.CurrentTick);
    }


}
