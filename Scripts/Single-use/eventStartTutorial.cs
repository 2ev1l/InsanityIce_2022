using UnityEngine;

public class eventStartTutorial : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip blood;
    public GameObject eventOff;
    public GameObject eventOn;
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject bloodobj;
    public GameObject wall;
    Vector3 vec1;
    Vector3 vec2;
    Vector3 vec3;
    Vector3 vec4;
    public GameObject gr;
    public GameObject set;
    void Start()
    {
        vec4=new Vector3(bloodobj.transform.position.x, bloodobj.transform.position.y, 0);
        vec1=new Vector3(light1.transform.position.x, light1.transform.position.y, 0);
        vec2=new Vector3(light2.transform.position.x, light2.transform.position.y, 0);
        vec3=new Vector3(light3.transform.position.x, light3.transform.position.y, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DDOnLoad.MainMusicData.Pause();
            wall.SetActive(true);
            eventOff.SetActive(false);
            gr.SetActive(true);
            Invoke("se",2f);
            Invoke("bloodx",1f);
            AudioSource.PlayClipAtPoint(clip, vec1, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            AudioSource.PlayClipAtPoint(clip, vec2, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            AudioSource.PlayClipAtPoint(clip, vec3, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
        }
    }
    void se()
    {
        DDOnLoad.MusicRandomData1.Play();
        wall.SetActive(false);
        DDOnLoad.ScreamerDangerData1.Play();
        eventOn.SetActive(true);
        set.SetActive(false);
    }
    void bloodx()
    {
        AudioSource.PlayClipAtPoint(blood, vec4, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            MonsterTutorial.isReady=true;
        }
    }
}
