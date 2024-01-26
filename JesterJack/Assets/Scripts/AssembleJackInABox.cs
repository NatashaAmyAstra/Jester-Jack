using UnityEngine;

public class AssembleJackInABox : MonoBehaviour
{
    [SerializeField][Range(0, 7)] private int startSprite;
    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[startSprite];
    }

    public void SetComponent(int index) {
        spriteRenderer.sprite = sprites[index];
    }
}
