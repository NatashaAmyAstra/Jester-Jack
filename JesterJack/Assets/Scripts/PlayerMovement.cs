using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float XMoveRotation;
    [SerializeField] private float YMoveRotation;
    

    void Update()
    {
        {
            Vector3 positionOffset = new Vector3(0, 0, 0);
            Quaternion rotationOffset = Quaternion.Euler(0, 0, Time.deltaTime);

            positionOffset.x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
            positionOffset.z = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

            transform.position += positionOffset;
        }
    }
}

