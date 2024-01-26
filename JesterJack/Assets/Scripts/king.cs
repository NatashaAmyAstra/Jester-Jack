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
    }
}
