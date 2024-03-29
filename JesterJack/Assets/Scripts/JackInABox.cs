using UnityEngine;

public enum JackInABoxComponents {
    head,
    spring,
    box
}

public class JackInABox : MonoBehaviour
{
    [SerializeField][Range(-1, 5)] private int startSprite;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private JackInABoxComponents component;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(startSprite > 0)
            spriteRenderer.sprite = sprites[startSprite];
        else
            spriteRenderer.sprite = null;
    }

    public JackInABoxComponents GetBoxComponent() {
        return component;
    }

    public void SetComponent(int index) {
        if(index < 0)
        {
            spriteRenderer.sprite = null;
            return;
        }

        spriteRenderer.sprite = sprites[index];
    }

}
