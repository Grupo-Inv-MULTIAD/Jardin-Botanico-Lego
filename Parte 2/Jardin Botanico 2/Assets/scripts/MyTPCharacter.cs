using UnityEngine;

public class MyTPCharacter : MonoBehaviour
{
    [SerializeField] // 👈 Esto hace visible el campo en el Inspector
    private Animator fullBodyAnimator;

    private void Awake()
    {
        if (fullBodyAnimator == null)
        {
            fullBodyAnimator = GetComponent<Animator>();
        }
    }

    public Animator GetFullBodyAnimator() => fullBodyAnimator;
}
