using UnityEngine;
using System.IO;

public class BottleLift : MonoBehaviour
{
    public GameObject Layout;
    public GameObject BottleLiftX;
    GameObject Player;
    bool isColEntered;
    public AudioClip open;
    Vector3 vec;
    public AudioClip success;
    public AudioClip reject;
    public static bool isWaiting=false;
    //static readonly string GameSave = "ss.json";

    void Start()
    {
        isWaiting=false;
        vec = new Vector3(Layout.transform.position.x, Layout.transform.position.y, 0);
        isColEntered=false;
        Layout.SetActive(false);       
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !PlayerControl.isTriggerEntered && !isWaiting && Bottles.OpenText[0].text.ToString() != "")
        {
            PlayerControl.isTriggerEntered=true;
            isColEntered=true;
            Player = col.gameObject;
            Layout.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && PlayerControl.isCanEnter && !isWaiting && Bottles.OpenText[0].text.ToString() != "")
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
        if (Input.GetKeyDown(KeyCode.E) && isColEntered && !isWaiting && PlayerControl.canMove)
        {
            isColEntered=false;
            PlayerControl.isTriggerEntered=true;
            PlayerControl.isCanEnter=false;
            Layout.SetActive(false);
            if (Bottles.formula == Bottles.OpenText[0].text.ToString())
            {
                isWaiting=true;
                foreach(var el in Bottles.OpenText)
                    el.text="...";
                AudioSource.PlayClipAtPoint(success, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                Invoke("WaitEnd",10f);
                Invoke("Reaction",5f);
            }
            else
            {
                if (Bottles.OpenText[0].text.ToString() != "")
                    AudioSource.PlayClipAtPoint(reject, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                foreach(var el in Bottles.OpenText)
                    el.text="";
                SaveData data = Saving.GetData();
                data.checkpoint = " ";
                Saving.Save(data);

            }
        }
    }
    void Reaction()
    {
        AudioSource.PlayClipAtPoint(open, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
    }
    void WaitEnd()
    {
        BottleLiftX.SetActive(true);
        PlayerControl.isKeyFinded=true;
        gameObject.SetActive(false);
    }
}
