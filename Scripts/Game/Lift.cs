using UnityEngine;
using UnityEngine.SceneManagement;
public class Lift : MonoBehaviour
{
    public GameObject Layout;
    GameObject Player;
    bool isColEntered;
    public AudioClip open;
    Vector3 vec;
    public GameObject Text;
    void Start()
    {
        vec = new Vector3(Layout.transform.position.x, Layout.transform.position.y, 0);
        isColEntered=false;
        Layout.SetActive(false);
        DDOnLoad.SoundList[11].Stop();
        DDOnLoad.SoundList[12].Stop();
        DDOnLoad.SoundList[13].Stop();
        DDOnLoad.MainMusicData.UnPause();
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !PlayerControl.isTriggerEntered)
        {
            PlayerControl.isTriggerEntered=true;
            isColEntered=true;
            Player = col.gameObject;
            Layout.SetActive(true);
            if (SceneManager.GetActiveScene().name=="Tutorial2" || SceneManager.GetActiveScene().name=="Game102")
            {
                PlayerControl.isKeyFinded=true;
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && PlayerControl.isCanEnter)
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
        if (col.gameObject.CompareTag("Player"))
        {
            isColEntered=false;
            Layout.SetActive(false);
            PlayerControl.isTriggerEntered=false;
            PlayerControl.isCanEnter=true;
            Text.SetActive(false);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColEntered && PlayerControl.canMove)
        {
            if (PlayerControl.isKeyFinded)
            {
                AudioSource.PlayClipAtPoint(open, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                Player.SetActive(false);
                isColEntered=false;
                PlayerControl.isTriggerEntered=true;
                PlayerControl.isCanEnter=false;
                DDOnLoad.LiftSoundData.Play();
                switch(SceneManager.GetActiveScene().name)
                {
                    case "Tutorial1.2":
                        bscr=GameObject.Find("BLACKSCR");
                        InvokeRepeating("ll",0f,0.02f);
                        break;
                    case "Game102":
                        SceneManager.LoadScene("Game103");
                        break;
                    default:
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        break;
                }
                DDOnLoad.StepSoundData.Stop();
            }
            else
            {
                if (SceneManager.GetActiveScene().name != "Tutorial1.1")
                {
                    Text.SetActive(true);
                }
            }
        }
    }
    float f=0f;
    GameObject bscr;
    void ll()
    {
        f+=0.01f;
        bscr.GetComponent<SpriteRenderer>().color=new Color(0,0,0,f);
        if (f>0.99f)
        {
            CancelInvoke("ll");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
