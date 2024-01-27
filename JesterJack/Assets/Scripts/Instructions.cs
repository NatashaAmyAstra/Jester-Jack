using UnityEngine;

public class Instructions : MonoBehaviour
{
    private int box;
    private int spring;
    private int head;
    private float difficulty;
    private Scroll scroll;

    private void Awake() {
        GenerateInstruction();
    }

    private void GenerateInstruction() {
        box = Random.Range(0, 6);
        spring = Random.Range(0, 6);
        head = Random.Range(0, 6);
    }

    public void SetPaper(king king, float difficulty, Scroll scroll) {
        king.RequestComponents(box, spring, head);
        this.scroll = scroll;
        this.difficulty = difficulty;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag != "Player")
            return;

        StartCoroutine(scroll.ShowInstructions(difficulty, box, spring, head));
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 6f);
    }
}
