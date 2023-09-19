using UnityEngine;

public class BoxLink : MonoBehaviour
{
    Vector3 vec;
    public AudioClip breakClip;
    int chance = 0;
    void Start()
    {
        vec = new Vector3(transform.position.x, transform.position.y, 0);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Monster"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(breakClip, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            chance = PlayerControl.GameID switch
            {
                int i when i < 11 => 1,
                int i when i >= 11 && i < 15 => 2,
                int i when i >= 15 => 3,
                _ => 0,
            };
            if (chance > 0)
            {
                int rnd = Random.Range(0, 101);
                switch (rnd)
                {
                    case int i when i < chance:
                        gameObject.SetActive(false);
                        AudioSource.PlayClipAtPoint(breakClip, vec, (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
                        break;
                }
            }
        }
    }
}
