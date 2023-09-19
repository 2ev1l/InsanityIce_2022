using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Steamworks;
using System;
using System.Runtime.Serialization.Formatters.Binary;
public class Buttons : MonoBehaviour
{
    //static readonly string GameSave = "ss.data";
    public GameObject btnContinue;
    public GameObject Settings;
    public GameObject Confirmation;
    public GameObject Credits;
    public GameObject Menu;
    public GameObject Resolution;
    public GameObject Audio;
    public GameObject Controls;
    public GameObject LanguagePanel;
    public static bool is_FS;
    Animator BS_Anim;
    int countPress=0;
    void Start()
    {
        countPress=0;
        SaveData data = Saving.GetData();
        if (data.scene != " " && data.scene != "")
        {
            btnContinue.SetActive(true);
        }
        else
        {
            btnContinue.SetActive(false);
        }
        try
        {
            BS_Anim = GameObject.Find("BlackScreenX").GetComponent<Animator>();
            Settings.SetActive(false);
            Confirmation.SetActive(false);
            Credits.SetActive(false);
            Menu.SetActive(true);
            Resolution.SetActive(false);
            Audio.SetActive(false);
            Controls.SetActive(false);
            LanguagePanel.SetActive(false);
        }
        catch
        {
            //Debug.Log("didn't exist");
        }
        is_FS = Screen.fullScreen;
        if (SceneManager.GetActiveScene().name=="Menu")
        {
            Time.timeScale=1f;
        }
    }

    public void PressedContinue()
    {
        if (countPress==0)
        {
            if (BS_Anim != null)
            {
                BS_Anim.SetBool("BSStart",true);
                Invoke("SC2",1f);
                countPress=1;
            }
        }
        try {GameObject.Find("EventSystem").SetActive(false);}
        catch {};
    }
    void SC2()
    {
        StartCoroutine("ContinueLoadAsync");
    }
    IEnumerator ContinueLoadAsync()
    {
        SaveData data = Saving.GetData();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(data.scene);

        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game10");
        if (asyncLoad.progress == 1f) 
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(data.scene));

            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game10"));
            yield return null;
        }
        DDOnLoad.MenuMusicData.Pause();
        if (data.scene == "Subtitles")
            DDOnLoad.MusicRandomData1.Play();
        else
            DDOnLoad.MainMusicData.Play();
    }
    public void PressedNewGameConfirm()
    {
        if (countPress==0)
        {
            if (BS_Anim != null)
            {
                BS_Anim.SetBool("BSStart",true);
                Invoke("SC1",1f);
            }
        }
        try {GameObject.Find("EventSystem").SetActive(false);}
        catch {};
    }
    void SC1()
    {
        StartCoroutine("NewGameLoadAsync");
    }
    IEnumerator NewGameLoadAsync()
    {
        SaveData data = Saving.GetData();
        data.scene = " ";
        data.checkpoint = " ";
        List<string> List = data.list;
        List[4] = " ";
        List[5] = " ";
        Saving.Save(data);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Tutorial");
        if (asyncLoad.progress == 1f) 
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Tutorial"));
            yield return null;
        }
        DDOnLoad.MenuMusicData.Pause();
        DDOnLoad.MainMusicData.Play();
    }

    IEnumerator MenuLoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
        if (asyncLoad.progress == 1f) 
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
            yield return null;
        }
        
    }
    public void LangEng()
    {
        SaveData data = Saving.GetData();
        List<string> List = data.list;
        List[3]="ENG";
        data.list = List;
        Saving.Save(data);
        try
        {
            foreach(var el in Language.RUSshare)
                el.SetActive(false);
            foreach(var el in Language.ENGshare)
                el.SetActive(true);
        }
        catch
        {
            Debug.Log("englang error ");
        }
    }
    public void LangRus()
    {
        SaveData data = Saving.GetData();
        List<string> List = data.list;
        List[3] = "RUS";
        data.list = List;
        Saving.Save(data);
        try
        {
            foreach (var el in Language.ENGshare)
            {
                el.SetActive(false);
            }
            foreach (var el in Language.RUSshare)
                el.SetActive(true);
        }
        catch
        {
            Debug.Log("ruslang error ");
        }
    }
    public void PressedNewGame()
    {
        Menu.SetActive(false);
        Confirmation.SetActive(true);
    }
    public void PressedLang()
    {
        Settings.SetActive(false);
        LanguagePanel.SetActive(true);
    }
    public void BackLangPressed()
    {
        LanguagePanel.SetActive(false);
        Settings.SetActive(true);
    }

    public void PressedSettings()
    {
        Menu.SetActive(false);
        Settings.SetActive(true);
    }

    public void BackSettingsPressed()
    {
        Settings.SetActive(false);
        Menu.SetActive(true);
    }

    public void BackCreditsPressed()
    {
        Credits.SetActive(false);
        Settings.SetActive(true);
    }

    public void BackNewGamePressed()
    {
        Confirmation.SetActive(false);
        Menu.SetActive(true);
    }

    public void PressedExit()
    {
        SteamAPI.Shutdown();
        Application.Quit();
        SteamAPI.Shutdown();
    }

    public void PressedCredits()
    {
        Settings.SetActive(false);
        Credits.SetActive(true);
    }

    public void PressedResolution()
    {
        Settings.SetActive(false);
        Resolution.SetActive(true);
    }

    public void BackResolutionPressed()
    {
        Resolution.SetActive(false);
        Settings.SetActive(true);
    }

    public void PressedAudio()
    {
        Settings.SetActive(false);
        Audio.SetActive(true);
    }

    public void BackAudioPressed()
    {
        Audio.SetActive(false);
        Settings.SetActive(true);
    }

    public void PressedControls()
    {
        Settings.SetActive(false);
        Controls.SetActive(true);
    }

    public void BackControlsPressed()
    {
        Controls.SetActive(false);
        Settings.SetActive(true);
    }

    public void PressedFullScreen()
    {
        is_FS=!is_FS;
        Screen.fullScreen = is_FS;
    }

    public void PressedListen()
    {
        DDOnLoad.TestSoundData.Play();
    }
    
    public void BackPausePressed()
    {
        PlayerControl.PauseMenuForButtons.SetActive(false);
        Time.timeScale=1f;
    }

    public void BackPauseMenuPressed()
    {
        Invoke("SC3",0f);
    }
    void SC3()
    {
        //BS_Anim.SetBool("BSStart",true);
        StartCoroutine("MenuLoadAsync");
        foreach (AudioSource el in DDOnLoad.SoundList)
        {
            el.Stop();
            el.pitch=1f;
        }
        DDOnLoad.MainMusicData.pitch=1f;
        DDOnLoad.StepSoundData.pitch=1f;
        DDOnLoad.StepSoundData1.pitch=1f;
        DDOnLoad.FlashLightOnData.pitch=1f;
        DDOnLoad.FlashLightOffData.pitch=1f;
        DDOnLoad.HeartBeatData.pitch = 1f;
        DDOnLoad.MainMusicData.Pause();
        DDOnLoad.StepSoundData.Stop();
        DDOnLoad.StepSoundData1.Stop();
        DDOnLoad.FlashLightOffData.Stop();
        DDOnLoad.FlashLightOnData.Stop();
        DDOnLoad.HeartBeatData.Stop();
    }
}
