using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionpointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private Collider _numFound;


    private void Update()
    {
        _numFound = Physics.OverlapSphere(_interactionPoint.position, _interactionpointRadius)[0];
        Column interactable = _numFound.GetComponent<Column>();
        if(interactable != null)
        {
            if(Input.GetKeyDown(KeyCode.E)) {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionpointRadius);
    }
}
