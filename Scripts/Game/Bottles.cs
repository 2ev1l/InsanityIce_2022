using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Bottles : MonoBehaviour
{
    public GameObject Layout;
    GameObject Player;
    bool isColEntered;
    public AudioClip success;
    public AudioClip reject;
    Vector3 vec;
    public Text[] formulaText;
    public static string formula;
    public string value;
    public int maxCount=9;
    int curCount=0;
    bool isSuccess=false;
    public static Text[] OpenText = new Text[3];
    //static readonly string GameSave = "ss.json";
    
    void Start()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Game99":
                formula="HSO3Cl";
                formulaText[0].text="";
                formulaText[1].text="";
                formulaText[2].text="";
                break;
            case "Game103":
                formula="HSO3Cl";
                break;
        }
        OpenText[0]=formulaText[0];
        OpenText[1]=formulaText[1];
        OpenText[2]=formulaText[2];
        vec = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        isColEntered=false;
        Layout.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !PlayerControl.isTriggerEntered && curCount<maxCount && !BottleLift.isWaiting)
        {
            PlayerControl.isTriggerEntered=true;
            isColEntered=true;
            Player = col.gameObject;
            Layout.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && PlayerControl.isCanEnter && curCount<maxCount && !BottleLift.isWaiting)
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
        if (col.gameObject.CompareTag("Player") && curCount<maxCount)
        {
            isColEntered=false;
            Layout.SetActive(false);
            PlayerControl.isTriggerEntered=false;
            PlayerControl.isCanEnter=true;
        }
    }
    void Update()
    {
        if (BottleLift.isWaiting && Layout.activeSelf)
        {
            Layout.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E) && isColEntered && curCount<maxCount && !BottleLift.isWaiting && PlayerControl.canMove)
        {
            curCount++;
            if (curCount==maxCount)
                Layout.SetActive(false);
            
            int i=0;
            foreach(var el in formulaText)
            {
                if(SceneManager.GetActiveScene().name == "Game99" || SceneManager.GetActiveScene().name == "Game102")
                {
                    if (el.text=="")
                    {
                        el.text=value;
                        isSuccess=true;
                        continue;
                    }
                    if (el.text=="H2")
                    {
                        if (value=="Cl2")
                        {
                            el.text="HCl";
                            isSuccess=true;
                            continue;
                        }
                        else
                        {
                            el.text=value;
                            isSuccess=false;
                            continue;
                        }
                    }
                    if (el.text=="Cl2")
                    {
                        if (value=="H2")
                        {
                            el.text="HCl";
                            isSuccess=true;
                            continue;
                        }
                        else
                        {
                            el.text=value;
                            isSuccess=false;
                            continue;
                        }
                    }
                    if (el.text=="HCl")
                    {
                        if (value=="SO3")
                        {
                            el.text="HSO3Cl";
                            isSuccess=true;
                            continue;
                        }
                        else
                        {
                            el.text=value;
                            isSuccess=false;
                            continue;
                        }
                    }
                    isSuccess=false;
                    el.text=value;
                }
            }
            if (isSuccess)
            {
                AudioSource.PlayClipAtPoint(success, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            }
            else
            {
                AudioSource.PlayClipAtPoint(reject, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            }
            SaveData data = Saving.GetData();
            data.checkpoint = formulaText[0].text.ToString();
            Saving.Save(data);
        }
    }
}
