using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Subtitles : MonoBehaviour
{
    public float[] time = {0f};
    public GameObject[] subtitles;
    public static List<string> playedList;
    int count=0;
    public static string sceneNameOld="null";
    bool isPlayed=false;
    public GameObject Canvas;
    CanvasGroup alphaLevel;
    float alphaCount=1f;
    const float ms=50f;
    public static GameObject[] arrST;
    public static bool CurrentActive = false;
    private void Start()
    {
        CurrentActive=false;
        arrST = GameObject.FindGameObjectsWithTag("Subtitle");
        alphaLevel=Canvas.GetComponent<CanvasGroup>();
        alphaLevel.alpha=1f;
        if (SceneManager.GetActiveScene().name != sceneNameOld)
        {
            count=0;
            playedList = new List<string>{};
        }
        //Debug.Log("old = "+sceneNameOld+" new = "+SceneManager.GetActiveScene().name);
        sceneNameOld=SceneManager.GetActiveScene().name;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!CurrentActive)
            {
                //Debug.Log("Check1 " +gameObject.name);
                foreach(var i in playedList)
                {
                    if (i == gameObject.name)
                    {
                        isPlayed=true;
                        break;
                    }
                }
                if (!isPlayed)
                {
                    //Debug.Log("isn't played");
                    foreach(var i in arrST)
                    {
                        if (i.name != gameObject.name)
                        {
                            i.SetActive(false);
                        }
                    }
                    Invoke("TextStart",time[count]);
                }
                CurrentActive=true;
            }
        }
    }
    private void TextStart()
    {
        if (count-1>=0)
        {
            //Debug.Log("count-1>=0");
            alphaLevel.alpha=1f;
            InvokeRepeating("SmoothAlphaClose",0f,Check());
        }
        else
        {
            //Debug.Log("count-1<0");
            alphaLevel.alpha=0f;
            subtitles[count].SetActive(true);
            InvokeRepeating("SmoothAlphaSet",0f,Check());
        }
    }
    private void Close()
    {
        alphaLevel.alpha-=0.1f;
        if (alphaLevel.alpha==0f)
        {
            //Debug.Log("Closeend2");
            CurrentActive=false;
            CancelInvoke("Close");
            if (count - 1 > -1 && count - 1 < subtitles.Length)
            {
                subtitles[count - 1].SetActive(false);
            }
            foreach(var i in arrST)
            {
                i.SetActive(true);
            }
            if (gameObject.name != "SpecialSubtitleTrigger")
            {
                playedList.Add(gameObject.name);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    private void SmoothAlphaSet()
    {
        //Debug.Log("SAS");
        alphaLevel.alpha+=0.1f;
        if (alphaLevel.alpha==1f)
        {
            CancelInvoke("SmoothAlphaSet");
            count++;
            if (count<=subtitles.Length-1)
            {
                //Debug.Log("SACend1");
                Invoke("TextStart",time[count]);
            }
            else
            {
                //Debug.Log("SACend2 " + subtitles.Length + " count = " + count);
                if (count<time.Length)
                    InvokeRepeating("Close",time[count], Check());
            }
        }
        //Debug.Log("count== "+count);
    }
    private void SmoothAlphaClose()
    {
        //Debug.Log("SAC");
        alphaLevel.alpha-=0.1f;
        if (alphaLevel.alpha==0f)
        {
            //Debug.Log("SACend");
            CancelInvoke("SmoothAlphaClose");
            if (count-1>-1)
                subtitles[count-1].SetActive(false);
            if (count<subtitles.Length)
                subtitles[count].SetActive(true);
            InvokeRepeating("SmoothAlphaSet",0f,Check());
        }
    }
    private float Check()
    { 
        if(count+1<time.Length)
        {
            //Debug.Log("CHECK1 = "+time[count+1]/ms);
            return (time[count+1])/ms;
        }
        else
        {
            //Debug.Log("CHECK2 = "+1f/ms);
            return 2f/ms;
        }
    }
}
