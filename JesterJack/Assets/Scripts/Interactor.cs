using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionpointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private Transform jackInABox;

    private readonly Collider[] _colliders = new Collider[3];


    private void Update()   
    {
        Collider[] objectsAtPoint = Physics.OverlapSphere(_interactionPoint.position, _interactionpointRadius);
        if(objectsAtPoint.Length == 0)
            return;

        Collider objectAtPoint = objectsAtPoint[0];
        Column interactable = objectAtPoint.GetComponent<Column>();
        if(interactable != null)
        {
            if(Input.GetKeyDown(KeyCode.E)) {
                interactable.Interact(jackInABox);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionpointRadius);
    }
}
