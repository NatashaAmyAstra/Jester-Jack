using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody parentRB;
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    bool facingRight = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        parentRB = transform.parent.GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 positionOffset = new Vector3(0, 0, 0);
        Quaternion rotationOffset = Quaternion.Euler(0, 0, Time.deltaTime);

        positionOffset.x = Input.GetAxisRaw("Horizontal") * -1;   
        positionOffset.z = Input.GetAxisRaw("Vertical") * -1;
        positionOffset.Normalize();

        parentRB.AddForce(positionOffset  * speed);

        if (positionOffset == Vector3.zero)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }


        if (positionOffset.x + positionOffset.z < 0)
        {
            facingRight = true;
            animator.SetBool("Facing right", facingRight);
        }
        else if (positionOffset.x + positionOffset.z > 0)
        {
            facingRight = false;
            animator.SetBool("Facing right", facingRight);
        }
        Debug.Log(positionOffset.x + positionOffset.z);
    }

}

