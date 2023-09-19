using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject Layout;
    GameObject Player;
    bool isColEntered;
    public AudioClip clip;
    public GameObject Link;
    Vector3 vec;
    void Start()
    {
        vec = new Vector3(Link.transform.position.x, Link.transform.position.y, 0);
        isColEntered=false;
        Layout.SetActive(false);
        Link.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player=col.gameObject;
            isColEntered=true;
            Layout.SetActive(true);
            if (PlayerControl.GameID > 3)
            {
                int rnd = Random.Range(0, 26);
                if (rnd == 4)
                {
                    Layout.SetActive(false);
                    Link.SetActive(true);
                    AudioSource.PlayClipAtPoint(clip, vec, (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
                    gameObject.SetActive(false);
                }
                //Debug.Log(rnd);
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isColEntered=false;
            Layout.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColEntered && PlayerControl.canMove)
        {
            Layout.SetActive(false);
            Link.SetActive(true);
            AudioSource.PlayClipAtPoint(clip, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            gameObject.SetActive(false);
        }
    }
}
