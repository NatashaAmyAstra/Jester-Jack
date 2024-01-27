using System.Collections;
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

        StartCoroutine(AnimateKing(successState));

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

    private IEnumerator AnimateKing(bool successState) {
        if(successState == false)
        {
            animator.SetBool("Angry", true);
            yield return new WaitForSeconds(1.7f);
        }

        animator.SetBool("Angry", false);
        animator.SetBool("Laughing", true);

        yield return new WaitForSeconds(1.5f);

        animator.SetBool("Laughing", false);
    }
}
