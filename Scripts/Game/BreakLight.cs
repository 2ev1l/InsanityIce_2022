using UnityEngine;

public class BreakLight : MonoBehaviour
{
    Vector3 vec;
    public GameObject light;
    public AudioClip clip;
    public GameObject next;

    void Start()
    {
        vec=new Vector3(light.transform.position.x, light.transform.position.y, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (next != null)
                next.SetActive(true);
            AudioSource.PlayClipAtPoint(clip, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            light.SetActive(false);
            gameObject.SetActive(false);
            
        }
    }
}
