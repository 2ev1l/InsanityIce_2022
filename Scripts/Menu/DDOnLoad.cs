using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOnLoad : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";
    public static List<AudioSource> SoundList = new List<AudioSource>{};
    public static int CheckAudio;
    public static GameObject Scripts;
    public static GameObject SwitchSound;
    public static GameObject ClickSound;
    public static GameObject MenuMusic;
    public static GameObject StepSound;
    public static GameObject StepSound1;
    public static GameObject MainMusic;
    public static AudioSource MainMusicData;
    public static AudioSource SwitchSoundData;
    public static AudioSource ClickSoundData;
    public static AudioSource MenuMusicData;
    public static AudioSource StepSoundData;
    public static AudioSource StepSoundData1;
    public static GameObject Screamer1;
    public static AudioSource ScreamerData1;
    public static GameObject Screamer2;
    public static AudioSource ScreamerData2;
    public static GameObject Screamer3;
    public static AudioSource ScreamerData3;
    public static GameObject ScreamerDanger1;
    public static AudioSource ScreamerDangerData1;
    public static GameObject ScreamerDanger2;
    public static AudioSource ScreamerDangerData2;
    public static GameObject ScreamerRandom1;
    public static AudioSource ScreamerRandomData1;
    public static GameObject ScreamerRandom2;
    public static AudioSource ScreamerRandomData2;
    public static GameObject ScreamerRandom3;
    public static AudioSource ScreamerRandomData3;
    public static GameObject ScreamerRandom4;
    public static AudioSource ScreamerRandomData4;
    public static GameObject SoundRandom1;
    public static AudioSource SoundRandomData1;
    public static GameObject SoundRandom2;
    public static AudioSource SoundRandomData2;
    public static GameObject JumpSound;
    public static AudioSource JumpSoundData;
    public static GameObject FlashLightOn;
    public static AudioSource FlashLightOnData;
    public static GameObject FlashLightOff;
    public static AudioSource FlashLightOffData;
    public static GameObject TestSound;
    public static AudioSource TestSoundData;
    public static GameObject MusicRandom1;
    public static AudioSource MusicRandomData1;
    public static GameObject MusicRandom2;
    public static AudioSource MusicRandomData2;
    public static GameObject MusicRandom3;
    public static AudioSource MusicRandomData3;
    public static GameObject LiftSound;
    public static AudioSource LiftSoundData;
    public static GameObject HeartBeat;
    public static AudioSource HeartBeatData;
    void Start()
    {
        if (CheckAudio == 0)
        {
        MusicRandom1 = GameObject.Find("MusicRandom1");
        MusicRandom2 = GameObject.Find("MusicRandom2");
        MusicRandom3 = GameObject.Find("MusicRandom3");
        Scripts = GameObject.Find("ScriptsXXX");
        SwitchSound = GameObject.Find("SwitchSound");
        ClickSound = GameObject.Find("ClickSound");
        MenuMusic = GameObject.Find("MenuMusic");
        MainMusic = GameObject.Find("MainMusic");
        StepSound = GameObject.Find("StepSound");
        StepSound1 = GameObject.Find("StepSound1");
        Screamer1 = GameObject.Find("Screamer1");
        Screamer2 = GameObject.Find("Screamer2");
        Screamer3 = GameObject.Find("Screamer3");
        ScreamerDanger1 = GameObject.Find("ScreamerDanger1");
        ScreamerDanger2 = GameObject.Find("ScreamerDanger2");
        ScreamerRandom1 = GameObject.Find("ScreamerRandom1");
        ScreamerRandom2 = GameObject.Find("ScreamerRandom2");
        ScreamerRandom3 = GameObject.Find("ScreamerRandom3");
        ScreamerRandom4 = GameObject.Find("ScreamerRandom4");
        SoundRandom1 = GameObject.Find("SoundRandom1");
        SoundRandom2 = GameObject.Find("SoundRandom2");
        JumpSound = GameObject.Find("JumpSound");
        FlashLightOff = GameObject.Find("FlashLightOff");
        FlashLightOn = GameObject.Find("FlashLightOn");
        TestSound =  GameObject.Find("TestSound");
        LiftSound = GameObject.Find("LiftSound");
        HeartBeat = GameObject.Find("HeartBeat");
        HeartBeatData = HeartBeat.GetComponent<AudioSource>();
        LiftSoundData = LiftSound.GetComponent<AudioSource>();
        MusicRandomData1 = MusicRandom1.GetComponent<AudioSource>();
        MusicRandomData2 = MusicRandom2.GetComponent<AudioSource>();
        MusicRandomData3 = MusicRandom3.GetComponent<AudioSource>();
        TestSoundData = TestSound.GetComponent<AudioSource>();
        FlashLightOnData = FlashLightOn.GetComponent<AudioSource>();
        FlashLightOffData = FlashLightOff.GetComponent<AudioSource>();
        ScreamerData1 = Screamer1.GetComponent<AudioSource>();
        ScreamerData2 = Screamer2.GetComponent<AudioSource>();
        ScreamerData3 = Screamer3.GetComponent<AudioSource>();
        ScreamerDangerData1 = ScreamerDanger1.GetComponent<AudioSource>();
        ScreamerDangerData2 = ScreamerDanger2.GetComponent<AudioSource>();
        ScreamerRandomData1 = ScreamerRandom1.GetComponent<AudioSource>();
        ScreamerRandomData2 = ScreamerRandom2.GetComponent<AudioSource>();
        ScreamerRandomData3 = ScreamerRandom3.GetComponent<AudioSource>();
        ScreamerRandomData4 = ScreamerRandom4.GetComponent<AudioSource>();
        SoundRandomData1 = SoundRandom1.GetComponent<AudioSource>();
        SoundRandomData2 = SoundRandom2.GetComponent<AudioSource>();
        StepSoundData1 = StepSound1.GetComponent<AudioSource>();
        MainMusicData = MainMusic.GetComponent<AudioSource>();
        StepSoundData = StepSound.GetComponent<AudioSource>();
        MenuMusicData = MenuMusic.GetComponent<AudioSource>();
        SwitchSoundData = SwitchSound.GetComponent<AudioSource>();
        ClickSoundData = ClickSound.GetComponent<AudioSource>();
        JumpSoundData = JumpSound.GetComponent<AudioSource>();

        DontDestroyOnLoad(Scripts);
        DontDestroyOnLoad(SwitchSound);
        DontDestroyOnLoad(ClickSound);
        DontDestroyOnLoad(MenuMusic);
        DontDestroyOnLoad(StepSound);
        DontDestroyOnLoad(MainMusic);
        DontDestroyOnLoad(StepSound1);
        DontDestroyOnLoad(MainMusicData);
        DontDestroyOnLoad(MenuMusicData);
        DontDestroyOnLoad(SwitchSoundData);
        DontDestroyOnLoad(ClickSoundData);
        DontDestroyOnLoad(StepSoundData);
        DontDestroyOnLoad(StepSoundData1);
        DontDestroyOnLoad(Screamer1);
        DontDestroyOnLoad(Screamer2);
        DontDestroyOnLoad(Screamer3);
        DontDestroyOnLoad(ScreamerDanger1);
        DontDestroyOnLoad(ScreamerDanger2);
        DontDestroyOnLoad(ScreamerRandom1);
        DontDestroyOnLoad(ScreamerRandom2);
        DontDestroyOnLoad(ScreamerRandom3);
        DontDestroyOnLoad(ScreamerRandom4);
        DontDestroyOnLoad(SoundRandom1);
        DontDestroyOnLoad(SoundRandom2);
        DontDestroyOnLoad(ScreamerData1);
        DontDestroyOnLoad(ScreamerData2);
        DontDestroyOnLoad(ScreamerData3);
        DontDestroyOnLoad(ScreamerDangerData1);
        DontDestroyOnLoad(ScreamerDangerData2);
        DontDestroyOnLoad(ScreamerRandomData1);
        DontDestroyOnLoad(ScreamerRandomData2);
        DontDestroyOnLoad(ScreamerRandomData3);
        DontDestroyOnLoad(ScreamerRandomData4);
        DontDestroyOnLoad(SoundRandomData1);
        DontDestroyOnLoad(SoundRandomData2);
        DontDestroyOnLoad(JumpSound);
        DontDestroyOnLoad(JumpSoundData);
        DontDestroyOnLoad(FlashLightOff);
        DontDestroyOnLoad(FlashLightOffData);
        DontDestroyOnLoad(FlashLightOn);
        DontDestroyOnLoad(FlashLightOnData);
        DontDestroyOnLoad(TestSound);
        DontDestroyOnLoad(TestSoundData);
        DontDestroyOnLoad(MusicRandom1);
        DontDestroyOnLoad(MusicRandomData1);
        DontDestroyOnLoad(MusicRandom2);
        DontDestroyOnLoad(MusicRandomData2);
        DontDestroyOnLoad(MusicRandom3);
        DontDestroyOnLoad(MusicRandomData3);
        DontDestroyOnLoad(LiftSound);
        DontDestroyOnLoad(LiftSoundData);
        DontDestroyOnLoad(HeartBeat);
        DontDestroyOnLoad(HeartBeatData);

        SoundList.Add(ScreamerData1); //0
        SoundList.Add(ScreamerData2); //1
        SoundList.Add(ScreamerData3); //2
        SoundList.Add(ScreamerDangerData1); //3
        SoundList.Add(ScreamerDangerData2); //4
        SoundList.Add(ScreamerRandomData1); //5
        SoundList.Add(ScreamerRandomData2); //6
        SoundList.Add(ScreamerRandomData3); //7
        SoundList.Add(ScreamerRandomData4); //8
        SoundList.Add(SoundRandomData1); //9
        SoundList.Add(SoundRandomData2); //10
        SoundList.Add(MusicRandomData1); //11
        SoundList.Add(MusicRandomData2); //12
        SoundList.Add(MusicRandomData3); //13
        //[0..13]
        
        CheckAudio++;
        }
        
        if (SceneManager.GetActiveScene().name=="Menu")
        {
            MenuMusicData.Play();
        }
    }   
}
