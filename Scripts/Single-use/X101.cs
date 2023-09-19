using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class X101 : MonoBehaviour
{
    public GameObject blueScreen;
    public Text[] TimerText; 
    //static readonly string GameSave = "ss.json";
    int ss;
    float alpha = 1f;
    float secondsA = 5f;
    public AudioClip iceSound;
    public GameObject altScreen;
    public Text[] formulaText;
    public GameObject[] altScreenAnim;
    public GameObject blackScreen;
    Vector3 vec;

    void Start()
    {
        SaveData data = Saving.GetData();
        try
        {
            blueScreen.SetActive(true);
            InvokeRepeating("SetAlphaLevel",0f,0.1f);
        }
        catch
        {
            CancelInvoke("SetAlphaLevel");
        }
        InvokeRepeating("Timer",0f,1f);
        foreach(var el in formulaText)
        {
            el.text=data.checkpoint;
        }
    }
    void CatchVec()
    {
        try
        {
            vec = new Vector3(PlayerControl.GetPlayer.transform.position.x, PlayerControl.GetPlayer.transform.position.y, 0);
        }
        catch
        {
            vec = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }
    }
    void SetAlphaLevel()
    {
        try
        {
            secondsA-=0.1f;
            alpha-=0.02f;
            blueScreen.GetComponent<SpriteRenderer>().color = new Color(0,250,255,alpha);
            if (secondsA<0.1f)
            {
                blueScreen.SetActive(false);
                CancelInvoke("SetAlphaLevel");
                blueScreen.GetComponent<SpriteRenderer>().color = new Color(0,250,255,0);
            }
        }
        catch
        {
            blueScreen.SetActive(false);
            CancelInvoke("SetAlphaLevel");
        }
    }
    void SetTime(int seconds)
    {
        SaveData data = Saving.GetData();
        List<string> List = data.list;
        List[4]=seconds.ToString();
        data.list = List;
        Saving.Save(data);
    }
    void Timer()
    {
        SaveData data = Saving.GetData();
        if (data.list[4] == " " || data.list[4] == "")
        {
            ss=540;
            SetTime(540);
        }
        else
        {
            ss = System.Convert.ToInt32(data.list[4]);
            ss--;
        }
        SetTime(ss);
        int min = (int)ss/60;
        int sec = (int)ss%60;
        foreach(var i in TimerText)
        {
            i.text=min.ToString("0")+":"+sec.ToString("00");
        }
        //Debug.Log(ss);
        if (ss<=0)
        {
            CancelInvoke("Timer");
            PlayerControl.PauseMenuForButtons.SetActive(false);
            Time.timeScale=1f;
            data = Saving.GetData();
            data.scene = "Subtitles";
            data.checkpoint = " ";
            Saving.Save(data);
            IceScreen();
            SteamAchievements.Ach("ACH_0K");
        }
    }
    void IceScreen()
    {
        PlayerControl.canMove=false;
        MonsterControl.isMuted=true;
        try
        {
            GameObject.Find("Monster").GetComponent<MonsterControl>().CancelInvoke();
            MonsterControl.GetMonster.SetActive(false);
        }
        catch
        { 
            Debug.Log("Monster isn't finded");
        }
        CatchVec();
        AudioSource.PlayClipAtPoint(iceSound, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
        altScreen.GetComponent<SpriteRenderer>().color = new Color(0,226,255,0);
        altScreen.SetActive(true);
        InvokeRepeating("SetAlphaLevel2",0f,0.1f);
    }
    float secondsA2=4f;
    float alpha2=0f;
    void SetAlphaLevel2()
    {
        secondsA2-=0.1f;
        alpha2+=0.01f;
        altScreen.GetComponent<SpriteRenderer>().color = new Color(0,226,255,alpha2);
        foreach (var el in altScreenAnim)
        {
            el.GetComponent<SpriteRenderer>().color = new Color(255,255,255,alpha2*2);
        }
        switch (secondsA2)
        {
            case float i when(i<3.5f && i>=3.1f):
                altScreenAnim[0].SetActive(true);
                break;
            case float i when(i<3.1f && i>=2.7f):
                altScreenAnim[1].SetActive(true);
                break;
            case float i when(i<2.7f && i>=2.3f):
                altScreenAnim[2].SetActive(true);
                break;
            case float i when(i<2.3f && i>=1.5f):
                altScreenAnim[3].SetActive(true);
                break;
            case float i when(i<1.5f && i>=1f):
                altScreenAnim[4].SetActive(true);
                break;
            case float i when(i<1f && i>=0.1f):
                altScreenAnim[5].SetActive(true);
                break;
        }
        if (secondsA2<0.1f)
        {
            CancelInvoke("SetAlphaLevel2");
            InvokeRepeating("BlackScreen",0f,0.1f);
            blackScreen.SetActive(true);
            blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        }
    }
    float secondsA3=5f;
    float alpha3=0f;
    void BlackScreen()
    {
        secondsA3-=0.1f;
        alpha3+=0.02f;
        blackScreen.GetComponent<SpriteRenderer>().color = new Color(0,0,0,alpha3);
        if (secondsA3<0.1f)
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
