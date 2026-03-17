using UnityEngine;

public class CharacterViewBase : MonoBehaviour
{
    public Transform atkEffTransform;
    [HideInInspector] public Animator animator;
    public AudioClip[] footStepAudioClips;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null) animator = GetComponentInChildren<Animator>();
        }
    }
}