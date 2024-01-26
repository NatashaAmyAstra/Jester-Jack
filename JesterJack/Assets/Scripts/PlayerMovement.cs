using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float XMoveRotation;
    [SerializeField] private float YMoveRotation;
    private Animator animator;
    bool facingRight = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 positionOffset = new Vector3(0, 0, 0);
        Quaternion rotationOffset = Quaternion.Euler(0, 0, Time.deltaTime);

        positionOffset.x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
           
        positionOffset.z = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        transform.position += positionOffset;



        if (positionOffset  == Vector3.zero)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }


        if (positionOffset.x < 0 && facingRight)
        {
            Flip();
        }
        else if (positionOffset.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

}

