using UnityEngine;
using UnityEngine.Events;

public class king : MonoBehaviour
{
    private UnityEvent resetRoundEvent;

    private Animator animator;

    private int requestedBox;
    private int requestedSpring;
    private int requestedHead;

    private void Awake()
    {
        resetRoundEvent = new UnityEvent();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("Laughing", false);
        }
        else animator.SetBool("Laughing", true);


        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetBool("Crying", false);
        }
        else animator.SetBool("Crying", true);
    }

    public void TurnInToy(int box, int spring, int head) {
        if(box == requestedBox && spring == requestedSpring && head == requestedHead)
        {
            
        }

        resetRoundEvent.Invoke();
    }
}
