using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class X104 : MonoBehaviour
{
    public GameObject[] toggleOff;
    public GameObject[] oldBG;
    public GameObject[] newBG;
    public GameObject[] newBGLight;
    public GameObject blackScreen;
    public GameObject subtitleTrigger;
    bool camReady = false;
    public Vector3 velocity;
    Vector3 desiredPosition;
    public Vector3 offset;
    float smoothTime=11f;
    public AudioClip errorSound;

    void Start()
    {
        blackScreen.SetActive(false);
    }

    void Update()
    {
        if (camReady)
        {
            Follow_Player.GetCamera.transform.position = Vector3.SmoothDamp(Follow_Player.GetCamera.transform.position, desiredPosition, ref velocity, smoothTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name=="Player")
        {
            PlayerControl.canMove=false;
            foreach(var el in toggleOff)
            {
                el.SetActive(false);
            }
            Invoke("Event",5f);
            Invoke("CameraStart",1f);
        }
    }

    void CameraStart()
    {
        Follow_Player.freeCam=true;
        InvokeRepeating("FreeCamera",0f,0.01f);
        Invoke("IncPlus",1f);
        Invoke("IncPlus",2f);
        Invoke("IncPlus",3f);
        Invoke("IncPlus",4f);
        Invoke("IncPlus",5f);
        Invoke("IncPlus",6f);
        desiredPosition = new Vector3(Follow_Player.GetCamera.transform.position.x, Follow_Player.GetCamera.transform.position.y, 0) +offset;
        camReady=true;
    }

    void IncPlus()
    {
        incValue+=0.0005f;
    }

    float camOrt=1.5f;
    float incValue=0.001f;

    void FreeCamera()
    {
        camOrt+=incValue;
        Follow_Player.GetCamera.GetComponent<Camera>().orthographicSize = camOrt;
        if (camOrt>=6f)
        {
            Follow_Player.GetCamera.GetComponent<Camera>().orthographicSize = 6f;
            CancelInvoke("FreeCamera");
            camReady=false;
        }
    }

    void Event()
    {
        Vector3 vec = new Vector3(PlayerControl.GetPlayer.transform.position.x,PlayerControl.GetPlayer.transform.position.y,0);
        AudioSource.PlayClipAtPoint(errorSound, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
        foreach(var el in newBGLight)
        {
            el.GetComponent<Light2D>().intensity = 0f;
        }
        foreach(var el in oldBG)
        {
            el.SetActive(false);
        }
        blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        blackScreen.SetActive(true);
        InvokeRepeating("BlackScreen",7f,0.1f);
        Invoke("SB",4f);
        Invoke("NewBG",1f);
    }

    void NewBG()
    {
        foreach(var el in newBG)
        {
            el.SetActive(true);
        }
        InvokeRepeating("LightAdd",0f,0.01f);
    }

    float newIntensity=0f;
    float addIntensity=0.001f;
    float endIntensity=0.36f;

    void LightAdd()
    {
        newIntensity+=addIntensity;
        foreach(var el in newBGLight)
        {
            el.GetComponent<Light2D>().intensity = newIntensity;
        }
        if (newIntensity>=endIntensity)
        {
            CancelInvoke("LightAdd");
            foreach(var el in newBGLight)
            {
                el.GetComponent<Light2D>().intensity = endIntensity;
            }
        }
    }

    void SB()
    {
        //ACHIEVEMENT
        subtitleTrigger.transform.position = PlayerControl.GetPlayer.transform.position;
    }

    float blackSec=5f;
    float blackAlp=0f;

    void BlackScreen()
    {
        blackSec-=0.1f;
        blackAlp+=0.02f;
        blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,blackAlp);
        if (blackSec<0.1f)
        {
            blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1f);
            CancelInvoke("BlackScreen");
            SubtitleLoad();
        }
    }

    void SubtitleLoad()
    {
        SceneManager.LoadScene("Subtitles");
    }
}
