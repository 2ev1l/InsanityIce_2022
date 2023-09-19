using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class XSubtitles : MonoBehaviour
{
    public GameObject blood;
    public GameObject logo;
    public AudioClip bloodSound;
    public GameObject[] iceBreak;
    public GameObject[] bg;
    public GameObject[] texts;
    public GameObject blackScreen;
    float blackSec = 3f;
    float blackAlp = 1f;
    //static readonly string GameSave = "ss.json";

    void Start()
    {
        SaveData data = Saving.GetData();
        List<string> newList = data.list;
        newList[4]=" ";
        data.scene = "Subtitles";
        data.checkpoint = " ";
        data.list = newList;
        Saving.Save(data);
        
        DDOnLoad.MainMusicData.Stop();
        DDOnLoad.MenuMusicData.Pause();
        if (!DDOnLoad.MusicRandomData1.isPlaying)
            DDOnLoad.MusicRandomData1.Play();
        blackScreen.SetActive(true);
        blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1f);
        InvokeRepeating("BlackScreen",0f,0.1f);
        Invoke("Event",15f);
    }
    void BlackScreen()
    {
        blackSec-=0.1f;
        blackAlp-=0.033f;
        blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,blackAlp);
        if (blackSec<0.1f)
        {
            blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            blackScreen.SetActive(false);
            CancelInvoke("BlackScreen");
        }
    }
    void Event()
    {
        //Vector3 vec = new Vector3(logo.transform.position.x,logo.transform.position.y,0);
        //AudioSource.PlayClipAtPoint(bloodSound, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
        DDOnLoad.ScreamerData2.Play();
        Invoke("Blood",0.2f);
        //logo.GetComponent<SpriteRenderer>().sprite=newLogo;
        InvokeRepeating("AlphaM",0.2f,0.1f);
    }
    float seconds=2f;
    float alphaLevel=1f;
    int colLevel=255;
    void AlphaM()
    {
        seconds-=0.1f;
        alphaLevel-=0.05f;
        logo.GetComponent<SpriteRenderer>().color=new Color(255,255,255,alphaLevel);
        blood.GetComponent<SpriteRenderer>().color=new Color(255,255,255,alphaLevel);
        colLevel-=12;
        foreach(var el in iceBreak)
        {
            el.GetComponent<SpriteRenderer>().color=new Color(1f,alphaLevel,alphaLevel,1f);
        }
        foreach(var el in bg)
        {
            el.GetComponent<SpriteRenderer>().color=new Color(0.4f,alphaLevel,alphaLevel,1f);
        }
        if (seconds<0.1f)
        {
            foreach(var el in bg)
            {
                el.GetComponent<SpriteRenderer>().color=new Color(0.4f,0,0,1f);
            }
            foreach(var el in iceBreak)
            {
                el.GetComponent<SpriteRenderer>().color=new Color(1f,0,0,1f);
            }
            logo.GetComponent<SpriteRenderer>().color=new Color(255,255,255,0);
            CancelInvoke("AlphaM");
        }
    }
    void Blood()
    {
        foreach(var el in texts)
        {
            el.SetActive(false);
        }
        blood.SetActive(true);
    }
}
