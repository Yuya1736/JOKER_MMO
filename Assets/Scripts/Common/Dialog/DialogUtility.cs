using DG.Tweening;
using UnityEngine;

public class DialogUtility
{
    public static float rotateDuration = 0.5f;

    private static Vector3 c1_OldRotationEuler;
    private static Vector3 c2_OldRotationEuler;
    private static Tween c1_Tween;
    private static Tween c2_Tween;

    public static void StartLookEach(Transform c1, Transform c2)
    {
        c1_OldRotationEuler = c1.eulerAngles;
        c2_OldRotationEuler = c2.eulerAngles;

        Quaternion c1Target = Quaternion.LookRotation(new Vector3(c2.position.x - c1.position.x, 0f, c2.position.z - c1.position.z));
        Quaternion c2Target = Quaternion.LookRotation(new Vector3(c1.position.x - c2.position.x, 0f, c1.position.z - c2.position.z));

        c1_Tween?.Kill();
        c2_Tween?.Kill();
        c1_Tween = c1.DORotateQuaternion(c1Target, rotateDuration);
        c2_Tween = c2.DORotateQuaternion(c2Target, rotateDuration);

    }

    public static void EndLookEach(Transform c1, Transform c2)
    {
        c1_Tween?.Kill();
        c2_Tween?.Kill();
        c1_Tween = c1.DORotate(c1_OldRotationEuler, rotateDuration);
        c2_Tween = c2.DORotate(c2_OldRotationEuler, rotateDuration);
    }
}
