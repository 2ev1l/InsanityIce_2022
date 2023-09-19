using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class ControlPanel : MonoBehaviour
{
    public GameObject Layout;
    GameObject Player;
    bool isColEntered;
    public AudioClip danger;
    Vector3 vec;
    public Text[] Timer;
    public static Text[] OpenTimer = new Text[3];
    public GameObject instruction;
    bool isChecked=false;
    public GameObject redScreen;
    public GameObject redCanvas;
    public GameObject blueScreen;
    public GameObject blackScreen;
    public AudioClip explosion;
    //static readonly string GameSave = "ss.json";
    public GameObject explosionObj;
    public GameObject[] disabledObj;
    public Vector3 velocity;
    
    void Start()
    {
        PlayerControl.isTriggerEntered=false;
        PlayerControl.isCanEnter=true;
        isColEntered=false;
        isChecked=false;
        for(int i=0; i<=2; i++)
            OpenTimer[i]=Timer[i];
        vec = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        isColEntered=false;
        Layout.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isChecked && !PlayerControl.isTriggerEntered && PlayerControl.isKeyFinded)
        {
            PlayerControl.isTriggerEntered=true;
            Player=col.gameObject;
            isColEntered=true;
            Layout.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isChecked && PlayerControl.isCanEnter && PlayerControl.isKeyFinded)
        {
            Player=col.gameObject;
            PlayerControl.isCanEnter=false;
            PlayerControl.isTriggerEntered=true;
            isColEntered=true;
            Layout.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isChecked && PlayerControl.isKeyFinded)
        {
            instruction.SetActive(false);
            PlayerControl.isTriggerEntered=false;
            PlayerControl.isCanEnter=true;
            isColEntered=false;
            Layout.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColEntered && !isChecked && PlayerControl.isKeyFinded)
        {
            if (!instruction.activeSelf)
            {
                Layout.SetActive(false);
                instruction.SetActive(true);
            }
            else
            {
                Layout.SetActive(true);
                instruction.SetActive(false);
            }
        }
        if (camReady)
        {
            Follow_Player.GetCamera.transform.position = Vector3.SmoothDamp(Follow_Player.GetCamera.transform.position, desiredPosition, ref velocity, smoothTime);
        }
    }
    public void RedButton()
    {
        if (!isChecked && !PlayerControl.PauseMenuForButtons.activeSelf)
        {
            instruction.SetActive(false);
            isChecked=true;
            AudioSource.PlayClipAtPoint(danger, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            PlayerControl.canMove=false;
            redCanvas.transform.position=new Vector3(PlayerControl.GetPlayer.transform.position.x,PlayerControl.GetPlayer.transform.position.y+0.52f,1);
            redScreen.SetActive(true);
            redScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            InvokeRepeating("EndRed",0f,0.1f);
            PlayerControl.PauseMenuForButtons.SetActive(false);
            Time.timeScale=1f;
            SaveData data = Saving.GetData();
            data.scene = "Subtitles";
            data.checkpoint = " ";
            Saving.Save(data);
            SteamAchievements.Ach("ACH_INSANITY_HOT");
        }
    }
    float seconds = 10f;
    float alpha = 0f;
    void EndRed()
    {
        foreach(var i in Timer)
            i.text=(System.Math.Truncate(seconds*10)/10f).ToString();
        seconds-=0.1f;
        alpha+=0.01f;
        redScreen.GetComponent<SpriteRenderer>().color = new Color(255,0,0,alpha);
        if (seconds<0.1f)
        {
            alpha = 0f;
            foreach(var i in Timer)
                i.text="0.0";
            CancelInvoke("EndRed");
            AudioSource.PlayClipAtPoint(explosion, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            explosionObj.GetComponent<CanvasGroup>().alpha=0f;
            explosionObj.transform.position=new Vector3(PlayerControl.GetPlayer.transform.position.x,PlayerControl.GetPlayer.transform.position.y+0.52f,1);
            explosionObj.SetActive(true);
            foreach(var el in disabledObj)
            {
                el.SetActive(false);
            }
            MonsterControl.isMuted=true;
            InvokeRepeating("ExplosionEnc",0f,0.1f);
            Invoke("CameraStart",5f);
        }
    }
    void ExplosionEnc()
    {
        if (alpha<1f)
        {
            alpha+=0.01f;
            try
            {
            redScreen.GetComponent<SpriteRenderer>().color = new Color(255,0,0,1f-alpha);
            }
            catch
            {
                redScreen.GetComponent<SpriteRenderer>().color = new Color(255,0,0,0f);
            }
            explosionObj.GetComponent<CanvasGroup>().alpha=alpha;
        }
        else
        {
            redScreen.GetComponent<SpriteRenderer>().color = new Color(255,0,0,0f);
            explosionObj.GetComponent<CanvasGroup>().alpha=1f;
            CancelInvoke("ExplosionEnc");
            
        }
    }
    bool camReady=false;
    Vector3 desiredPosition;
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
        desiredPosition = new Vector3(Follow_Player.GetCamera.transform.position.x+1.59361f, Follow_Player.GetCamera.transform.position.y+1.4703f, 0);
        camReady=true;
        //Follow_Player.GetCamera.transform.position = desiredPosition;
    }
    float smoothTime=6f;
    float camOrt=1.5f;
    public Sprite tvNewSprite;
    public GameObject tv;
    public AudioClip glitchTvSound;
    float incValue=0.001f;
    void IncPlus()
    {
        incValue+=0.0005f;
    }
    void FreeCamera()
    {
        camOrt+=incValue;
        Follow_Player.GetCamera.GetComponent<Camera>().orthographicSize = camOrt;
        if (camOrt>=6f)
        {
            Follow_Player.GetCamera.GetComponent<Camera>().orthographicSize = 6f;
            CancelInvoke("FreeCamera");
            blackScreen.SetActive(true);
            blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            camReady=false;
            Invoke("TvEffects",1f);
        }
    }
    void TvEffects()
    {
        tv.GetComponent<SpriteRenderer>().sprite = tvNewSprite;
        Vector3 tvVec=new Vector3(tv.transform.position.x,tv.transform.position.y,0);
        AudioSource.PlayClipAtPoint(glitchTvSound, tvVec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
        InvokeRepeating("BlackScreen",1f,0.1f);
    }
    float blackSec = 5f;
    float blackAlp = 0f;
    void BlackScreen()
    {
        blackSec-=0.1f;
        blackAlp+=0.02f;
        blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,blackAlp);
        if (blackSec<0.1f)
        {
            blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1f);
            CancelInvoke("BlackScreen");
            Invoke("SubtitleLoad",0.5f);
        }
    }
    void SubtitleLoad()
    {
        SceneManager.LoadScene("Subtitles");
    }
    void EndBlue()
    {
        foreach(var i in Timer)
            i.text=(System.Math.Truncate(seconds*10)/10f).ToString();
        seconds-=0.1f;
        alpha+=0.01f;
        blueScreen.GetComponent<SpriteRenderer>().color = new Color(0,250,255,alpha);
        if (seconds<0.1f)
        {
            foreach(var i in Timer)
                i.text="0.0";
            CancelInvoke("EndBlue");
            SceneManager.LoadScene("Game101");
        }
        
    }
    public void BlueButton()
    {
        if (!isChecked && !PlayerControl.PauseMenuForButtons.activeSelf)
        {
            instruction.SetActive(false);
            isChecked=true;
            PlayerControl.PauseMenuForButtons.SetActive(false);
            Time.timeScale=1f;
            AudioSource.PlayClipAtPoint(danger, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            PlayerControl.canMove=false;
            blueScreen.SetActive(true);
            blueScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            InvokeRepeating("EndBlue",0f,0.1f);
            SteamAchievements.Ach("ACH_INSANITY_ICE");
        }
    }
}
