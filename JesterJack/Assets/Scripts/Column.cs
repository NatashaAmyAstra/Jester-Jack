using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Column : MonoBehaviour
{
    public enum ColumnStates {
        closed_full = 0,
        opened_full,
        opened_empty,
        closed_empty,
    }

    [SerializeField] private ColumnStates columnState = ColumnStates.closed_full;
    [SerializeField] private JackInABoxComponents givenComponent;
    private List<Column> columnsWithSameComponent = new List<Column>();

    [SerializeField][Range(0, 5)] private int givenColor;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteDisplay;

    [SerializeField] private float spriteFadeInSeconds;
    private float opacityPerStep;

    [SerializeField] private Renderer columnRenderer;
    [SerializeField] private Material columnDefaultMaterial;
    [SerializeField] private Material columnHighlightMaterial;
    private bool highlightable = true;

    [SerializeField] private float raiseSpeedSeconds;

    [SerializeField] private float loweredHeight;
    [SerializeField] private float raisedHeight;

    private Coroutine activeCoroutine = null;
    [SerializeField] private float highlightTimeSeconds;
    private float highlightTimer;

    private king king;

    private void Awake() {
        columnRenderer.material = columnDefaultMaterial;
        spriteDisplay = GetComponentInChildren<SpriteRenderer>();
        spriteDisplay.sprite = sprites[givenColor];

        opacityPerStep = 1 / (spriteFadeInSeconds * 50);
    }

    private void Start() {
        king = GameObject.Find("King").GetComponent<king>();
        king.GetEvent().AddListener(ResetColumns);

        Column[] Columns = FindObjectsOfType<Column>();
        foreach(Column column in Columns)
        {
            if(givenComponent == column.GetGivenComponent())
                columnsWithSameComponent.Add(column);
        }
    }

    public JackInABoxComponents GetGivenComponent() {
        return givenComponent;
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
        if(columnState == ColumnStates.closed_empty || highlightable == false)
            return;

        columnRenderer.material = columnHighlightMaterial;
        highlightTimer = highlightTimeSeconds;
    }

    public int Interact(Transform jackInABox) {
        if(activeCoroutine != null)
            return -1;

        int returnvalue = -1;
        switch(columnState)
        {
            case ColumnStates.closed_full:

                NextState();
                DisableRow();
                activeCoroutine = StartCoroutine(ChangeHeight(raisedHeight));
                highlightable = false;
                break;
            case ColumnStates.opened_full:
                returnvalue = givenColor;
                NextState();
                GrabContent(jackInABox);
                spriteDisplay.color = new Color(1, 1, 1, 0);
                NextState();
                activeCoroutine = StartCoroutine(ChangeHeight(loweredHeight));
                break;
        }

        return returnvalue;
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

        float opacity = 0;
        while(opacity < 1)
        {
            opacity += opacityPerStep;
            spriteDisplay.color = new Color(1, 1, 1, opacity);
            yield return new WaitForSeconds(0.02f);
        }

        highlightable = true;
        activeCoroutine = null;
    }

    private void DisableRow() {
        foreach(Column column in columnsWithSameComponent)
        {
            if(column.gameObject == gameObject)
                continue;

            column.SetColumnState(ColumnStates.closed_empty);
        }
    }

    public void SetColumnState(ColumnStates state) {
        columnState = state;
    }

    private void GrabContent(Transform jackHolder) {
        foreach(Transform child in jackHolder)
        {
            if(child.GetComponent<SpriteRenderer>().sprite != null)
                continue;

            JackInABox jackInABox = child.GetComponent<JackInABox>();
            if(jackInABox.GetBoxComponent() == givenComponent)
            {
                jackInABox.SetComponent(givenColor);
                break;
            }
        }
    }

    private void NextState() {
        columnState = (ColumnStates)((int)columnState + 1);
    }

    private void ResetColumns() {
        columnState = ColumnStates.closed_full;
        spriteDisplay.color = new Color(1, 1, 1, 0);
    }
}
