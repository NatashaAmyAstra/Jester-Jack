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
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteDisplay;

    [SerializeField] private float loweredHeight;
    [SerializeField] private float raisedHeight;

    [SerializeField] private Renderer columnRenderer;
    [SerializeField] private Material columnDefaultMaterial;
    [SerializeField] private Material columnHighlightMaterial;

    [SerializeField] private float raiseSpeedSeconds;

    private Coroutine activeCoroutine = null;
    [SerializeField] private float highlightTimeSeconds;
    private float highlightTimer;

    private void Awake() {
        columnRenderer.material = columnDefaultMaterial;
        spriteDisplay = GetComponentInChildren<SpriteRenderer>();
        spriteDisplay.sprite = sprites[givenColor];
    }

    private void Update() {
        if(highlightTimer > 0)
        {
            highlightTimer -= Time.deltaTime;
            if(highlightTimer < 0)
            {
                columnRenderer.material = columnDefaultMaterial;
            }
        }
    }

    public void Highlight() {
        if(columnState == columnStates.closed_empty)
            return;

        columnRenderer.material = columnHighlightMaterial;
        highlightTimer = highlightTimeSeconds;
    }

    public void Interact(Transform jackInABox) {
        if(activeCoroutine != null)
            return;

        switch(columnState)
        {
            case columnStates.closed_full:
                NextState();
                activeCoroutine = StartCoroutine(ChangeHeight(raisedHeight));
                break;
            case columnStates.opened_full:
                NextState();
                GrabContent(jackInABox);
                spriteDisplay.sprite = null;
                break;
            case columnStates.opened_empty:
                NextState();
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

        activeCoroutine = null;
    }

    private void GrabContent(Transform jackHolder) {
        foreach(Transform child in jackHolder)
        {
            JackInABox jackInABox = child.GetComponent<JackInABox>();
            if(jackInABox.GetBoxComponent() == givenComponent)
            {
                jackInABox.SetComponent(givenColor);
                break;
            }
        }
    }

    private void NextState() {
        columnState = (columnStates)((int)columnState + 1);
    }
}
