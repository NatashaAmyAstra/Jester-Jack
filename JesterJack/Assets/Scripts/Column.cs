using System.Collections;
using UnityEngine;

public class Column : MonoBehaviour
{
    private enum columnStates {
        closed_full = 0,
        opened_full,
        opened_empty,
        closed_empty,
    }

    [SerializeField] private columnStates columnState = columnStates.closed_full;
    [SerializeField] private JackInABoxComponents givenComponent;
    [SerializeField][Range(0, 5)] private int givenColor;

    [SerializeField] private float loweredHeight;
    [SerializeField] private float raisedHeight;

    [SerializeField] private float raiseSpeedSeconds;

    private Coroutine activeCoroutine = null;

    public void Interact(Transform jackInABox) {
        if(activeCoroutine != null)
            return;

        switch(columnState)
        {
            case columnStates.closed_full:
                activeCoroutine = StartCoroutine(ChangeHeight(raisedHeight));
                break;
            case columnStates.opened_full:
                GrabContent(jackInABox);
                break;
            case columnStates.opened_empty:
                activeCoroutine = StartCoroutine(ChangeHeight(loweredHeight));
                break;
        }
    }

    private IEnumerator ChangeHeight(float height) {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, height, transform.position.z);
        float t = 0;

        while(t < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            t += 1 / (50 * raiseSpeedSeconds);
            yield return new WaitForSeconds(0.02f);
        }

        columnState = (columnStates)((int)columnState + 1);
        activeCoroutine = null;
    }

    private void GrabContent(Transform jackHolder) {
        foreach(Transform child in jackHolder)
        {
            JackInABox jackInABox = child.GetComponent<JackInABox>();
            if(jackInABox.GetBoxComponent() == givenComponent)
            {
                Debug.Log("same component");
                jackInABox.SetComponent(givenColor);
                break;
            }
        }

        columnState = (columnStates)((int)columnState + 1);
    }
}
