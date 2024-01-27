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
        Time.timeScale = 1.0f;
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

    public bool TurnInToy(int box, int spring, int head) {
        bool successState;
        if(box == requestedBox && spring == requestedSpring && head == requestedHead)
        {
            successState = true;
            animator.SetBool("Laughing", true);
        }
        else
        {
            successState = false;
            animator.SetBool("Crying", true);
        }
        Debug.Log(successState);
        resetRoundEvent.Invoke();
        return successState;
    }

    public UnityEvent GetEvent() {
        return resetRoundEvent;
    }

    public void RequestComponents(int box, int spring, int head) {
        requestedBox = box;
        requestedSpring = spring;
        requestedHead = head;
    }
}
