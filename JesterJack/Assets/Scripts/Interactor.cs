using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionpointRadius = 0.5f;
    [SerializeField] private float looseForce = 10000f;
    [SerializeField] private float looseTorque = 10000f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private Transform jackInABox;

    private int acquiredBox = -1;
    private int acquiredSpring = -1;
    private int acquiredHead = -1;

    private void Update()   
    {
        Collider[] objectsAtPoint = Physics.OverlapSphere(_interactionPoint.position, _interactionpointRadius);
        if(objectsAtPoint.Length == 0)
            return;

        Collider objectAtPoint = objectsAtPoint[0];
        Column column = objectAtPoint.GetComponent<Column>();
        if(column != null)
        {
            column.Highlight();
            if(Input.GetKeyDown(KeyCode.E)) {
                int color = column.Interact(jackInABox);
                if(color >= 0)
                {
                    switch(column.GetGivenComponent())
                    {
                        case JackInABoxComponents.box:
                            acquiredBox = color;
                            break;
                        
                        case JackInABoxComponents.spring:
                            acquiredSpring = color;
                            break;
                        
                        case JackInABoxComponents.head:
                            acquiredHead = color;
                            break;
                    }
                }
            }
        }

        king king = objectAtPoint.GetComponent<king>();
        if(king != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                foreach(Transform child in jackInABox)
                {
                    child.GetComponent<JackInABox>().SetComponent(-1);
                }

                bool success = king.TurnInToy(acquiredBox, acquiredSpring, acquiredHead);
                if(success == false)
                {
                    StartCoroutine(Loose());
                }
            }
        }
    }

    private IEnumerator Loose() {
        yield return new WaitForSeconds(1.5f);
        Collider playerCollider = transform.parent.GetComponent<Collider>();
        playerCollider.enabled = false;

        Rigidbody playerRB = transform.parent.GetComponent<Rigidbody>();
        playerRB.drag = 0;
        playerRB.constraints = RigidbodyConstraints.None;

        playerRB.AddForce((transform.up + transform.right) * looseForce);
        playerRB.AddTorque(transform.forward * looseTorque);

        yield return new WaitForSeconds(2f);
        playerRB.drag = 10;
        playerRB.constraints = RigidbodyConstraints.FreezeRotation;
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;

        transform.parent.rotation = Quaternion.identity;
        transform.parent.position = new Vector3(4.5f, 3.22f, 10.6f);

        playerCollider.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionpointRadius);
    }

    public void SetAcquiredComponent(JackInABoxComponents component, int color) {
        switch(component)
        {
            case JackInABoxComponents.box:
                acquiredBox = color;
                break;

            case JackInABoxComponents.spring:
                acquiredSpring = color;
                break;

            case JackInABoxComponents.head:
                acquiredHead = color;
                break;
        }
    }
}
