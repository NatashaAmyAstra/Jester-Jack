using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class king : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
     
    }

    void Update()
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
}
