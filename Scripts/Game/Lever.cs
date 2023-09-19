using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject Layout;
    GameObject Player;
    bool isColEntered;
    public AudioClip Default;
    Vector3 vec;
    public GameObject textIsFinded;
    public GameObject switchedPos;
    public GameObject LeverLift;
    public GameObject FakeLift;
    //public bool isKeyNeeded=false;
    public static bool keyFinded;
    public static bool leverPressed;
    void Start()
    {
        leverPressed=false;
        keyFinded=false;
        vec = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        isColEntered=false;
        switchedPos.SetActive(false);
        textIsFinded.SetActive(false);
        Layout.SetActive(false);
        FakeLift.SetActive(true);
        LeverLift.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !PlayerControl.isTriggerEntered)
        {
            PlayerControl.isTriggerEntered=true;
            Player=col.gameObject;
            isColEntered=true;
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
            PlayerControl.isTriggerEntered=false;
            PlayerControl.isCanEnter=true;
            isColEntered=false;
            Layout.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColEntered)
        {
            leverPressed=true;
            switchedPos.SetActive(true);
            AudioSource.PlayClipAtPoint(Default, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            LeverLift.SetActive(true);
            FakeLift.SetActive(false);
            GameObject[] arr = GameObject.FindGameObjectsWithTag("Storage");
            if (arr.Length==0)
            {
                leverPressed=false;
                keyFinded=false;
                PlayerControl.isKeyFinded=true;
                textIsFinded.SetActive(true);
            }
            else
            {
                if (keyFinded)
                {
                    leverPressed=false;
                    keyFinded=false;
                    PlayerControl.isKeyFinded=true;
                    textIsFinded.SetActive(true);
                }
            }
            gameObject.SetActive(false);
        }
    }
}
