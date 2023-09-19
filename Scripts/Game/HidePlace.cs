using UnityEngine;

public class HidePlace : MonoBehaviour
{
    public GameObject Layout;
    GameObject Player;
    bool isColEntered;
    bool isObjEntered;
    public AudioClip open;
    public AudioClip close;
    public GameObject light;
    Vector3 vec;
    public bool isHidden = false;
    void Start()
    {
        vec = new Vector3(Layout.transform.position.x, Layout.transform.position.y, 0);
        isObjEntered=false;
        isColEntered=false;
        Layout.SetActive(false);
        light.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !PlayerControl.isTriggerEntered)
        {
            PlayerControl.isTriggerEntered=true;
            isColEntered=true;
            Player = col.gameObject;
            Layout.SetActive(true);
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
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerControl.canMove)
        {
            if (isColEntered && !isObjEntered)
            {
                AudioSource.PlayClipAtPoint(open, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                PlayerControl.FlashLightForMonster.SetActive(false);
                Player.SetActive(false);
                isObjEntered=true;
                isColEntered=true;
                DDOnLoad.StepSoundData.Stop();
                light.SetActive(true);
                PlayerControl.isTriggerEntered=true;
                PlayerControl.isCanEnter=false;
                isHidden = true;
            }
            else 
            if (isObjEntered)
            {
                AudioSource.PlayClipAtPoint(close, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                Player.SetActive(true);
                isObjEntered=false;
                light.SetActive(false);
                PlayerControl.isTriggerEntered=false;
                PlayerControl.isCanEnter=true;
                isHidden = false;
            }
        }
    }
}
