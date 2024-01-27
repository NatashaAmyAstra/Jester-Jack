using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionpointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private Transform jackInABox;

    private int acquiredBox;
    private int acquiredSpring;
    private int acquiredHead;

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
                column.Interact(jackInABox);
            }
        }

        king king = objectAtPoint.GetComponent<king>();
        if(king != null)
        {

        }
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
