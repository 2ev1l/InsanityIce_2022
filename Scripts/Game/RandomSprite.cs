using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] newSprite;
    private void Start()
    {
        int rnd = Random.Range(0, newSprite.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite[rnd];
    }
}
