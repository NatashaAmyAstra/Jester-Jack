using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] scrolls;
    [SerializeField] private GameObject[] boxNode;
    [SerializeField] private GameObject[] springNode;
    [SerializeField] private GameObject[] headNode;

    public IEnumerator ShowInstructions(float difficulty, int box, int spring, int head) {
        int selectedSprite = (int)(difficulty * scrolls.Length);
        selectedSprite = Mathf.Clamp(selectedSprite, 0, scrolls.Length);
        image.sprite = scrolls[selectedSprite];

        foreach(GameObject obj in boxNode)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in springNode)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in headNode)
        {
            obj.SetActive(false);
        }

        boxNode[box].SetActive(true);
        springNode[spring].SetActive(true);
        headNode[head].SetActive(true);

        gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
