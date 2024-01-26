using System.Collections;
using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField] private bool raised = false;

    [SerializeField] private float loweredHeight;
    [SerializeField] private float raisedHeight;

    [SerializeField] private float raiseSpeedSeconds;

    private Coroutine activeCoroutine = null;

    public void Interact() {
        if(activeCoroutine != null)
            return;

        if(raised == false)
            activeCoroutine = StartCoroutine(ChangeHeight(raisedHeight));
        else if(raised == true)
            activeCoroutine = StartCoroutine(ChangeHeight(loweredHeight));
    }

    private IEnumerator ChangeHeight(float height) {
        Debug.Log("HI");
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, height, transform.position.z);
        float t = 0;

        while(t < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            t += 1 / (50 * raiseSpeedSeconds);
            yield return new WaitForSeconds(0.02f);
        }

        raised = !raised;
        activeCoroutine = null;
    }
}
