using UnityEngine;

public class BreakLight2 : MonoBehaviour
{
    Vector3 vec;
    public GameObject light;
    public AudioClip clip;

    void Start()
    {
        vec=new Vector3(light.transform.position.x, light.transform.position.y, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && light.activeSelf)
        {
            int rnd = Random.Range(0,3);
            if (rnd==2)
            {
                AudioSource.PlayClipAtPoint(clip, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                light.SetActive(false);
                gameObject.SetActive(false);
            }
            //Debug.Log(rnd);
        }
    }
}
