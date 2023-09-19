using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;
using System.Linq;
public class MonsterControl : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";
    public static float speed=-1.7f;
    public static float speedConst=1.7f;
    public Animator anim;
    public static Rigidbody2D rigidBody;
    public static bool isReady=false;
    public GameObject[] KillScreen;
    public Animator[] KillScreenAnimator;
    public Vector3 AnimOffset;
    public static bool isFacingRight;
    public static bool isPlayerOnOtherSide;
    public static bool isHunting=false;
    public static GameObject GetMonster;
    public static bool canInvokeStop=false;
    public static bool canInvokeRandom=true;
    bool isLastWallRight=false;
    bool nearWall=false;
    public AudioClip[] rndPlayList;
    Vector3 vec;
    int rndPlayListRange;
    public static int GameID;
    public static int deathCount=0;
    public static bool isMuted=false;
    public static bool isTeleporting=false;
    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "Tutorial" 
        && SceneManager.GetActiveScene().name != "Tutorial1.1" 
        && SceneManager.GetActiveScene().name != "Tutorial1.2" 
        && SceneManager.GetActiveScene().name != "Tutorial1.3" 
        && SceneManager.GetActiveScene().name != "Tutorial2"
        && SceneManager.GetActiveScene().name != "Tutorial2.1" )
            gameObject.transform.position = new Vector3(0,0,0);
    }
    void Start()
    {
        isTeleporting=false;
        isMuted=false;
        //Debug.Log(deathCount);
        string sceneName = SceneManager.GetActiveScene().name;
        sceneName = sceneName.Remove(0,4);
        try
        {
            GameID=System.Convert.ToInt32(sceneName);
            switch(GameID)
            {
                case int i when (i<11):
                    speed=-1.7f;
                    speedConst=1.7f;
                    break;
                case int i when (i>=11 && i<15):
                    speed=-1.8f;
                    speedConst=1.8f;
                    break;
                case int i when (i >= 15 && i<18):
                    speed = -1.9f;
                    speedConst = 1.9f;
                    break;
                case int i when (i>=18):
                    speed=-2.2f;
                    speedConst=2.2f;
                    break;
                default:
                    speed = -1.7f;
                    speedConst = 1.7f;
                    break;
            }
        }
        catch
        {
            Debug.Log("Incorrect scene name: ");
            GameID=0;
            speed=-1.7f;
            speedConst=1.7f;
        }
        rndPlayListRange=0;
        foreach(AudioClip el in rndPlayList) rndPlayListRange++;
        Invoke("RandomSounds",(float)Random.Range(10,20));
        Invoke("RandomMove",(float)Random.Range(2,5));
        GetMonster=gameObject;
        isLastWallRight=false;
        nearWall=false;
        canInvokeStop=false;
        canInvokeRandom=true;
        isHunting=false;
        isPlayerOnOtherSide=false;
        KillScreenAnimator[0].SetBool("Kill",false);
        KillScreen[0].SetActive(false);
        anim.SetBool("isKill",false);
        rigidBody=gameObject.GetComponent<Rigidbody2D>();
        isReady=false;
        if (gameObject.transform.lossyScale.x>0)
        {
            isFacingRight=true;
        }
        else
        {
            isFacingRight=false;
        }
        if (SceneManager.GetActiveScene().name != "Tutorial" && SceneManager.GetActiveScene().name != "Tutorial2")
            Invoke("RandomSpawn",0.05f);
    }
    void RandomSounds()
    {
        //DDOnLoad.SoundList[Random.Range(5,11)].Play();
        if (!isMuted)
        {
            switch(Random.Range(0,10))
            {
                case int i when (i > 1):
                    AudioSource.PlayClipAtPoint(rndPlayList[Random.Range(0, rndPlayListRange)], vec, (PlayerPrefs.GetFloat("optionvalueS", 0) + 25f) / 50f);
                    //DDOnLoad.HeartBeatData.Play();
                    break;
                default:
                    DDOnLoad.HeartBeatData.Play();
                    break;
            }
        }

        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "One shot audio");
        if (!isTeleporting)
        {
            foreach (GameObject el in objects)
            {
                //Debug.Log(el.GetComponent<AudioSource>().clip.ToString());
                switch(el.GetComponent<AudioSource>().clip.ToString())
                {
                    case "SoundRandom1 (UnityEngine.AudioClip)":
                        el.AddComponent<AudioClipFollow>();
                        break;
                    case "SoundRandom2 (UnityEngine.AudioClip)": 
                        el.AddComponent<AudioClipFollow>();
                        break;
                    case "ScreamerRandom1 (UnityEngine.AudioClip)": 
                        el.AddComponent<AudioClipFollow>();
                        break;
                    case "ScreamerRandom2 (UnityEngine.AudioClip)": 
                        el.AddComponent<AudioClipFollow>();
                        break;
                    case "ScreamerRandom3 (UnityEngine.AudioClip)": 
                        el.AddComponent<AudioClipFollow>();
                        break;
                    case "ScreamerRandom4 (UnityEngine.AudioClip)": 
                        el.AddComponent<AudioClipFollow>();
                        break;
                }
            }
        }
        Invoke("RandomSounds", Random.Range(10, 20));
    }
    void RandomSpawn()
    {
        var rand=new System.Random();
        int rnd=rand.Next(0,PlayerControl.RndPositionForMonster.Length);
        while(rnd == PlayerControl.WhereIsPlayer || rnd == PlayerControl.WhereIsPlayer-1 || rnd == PlayerControl.WhereIsPlayer+1)
        {
            rnd=rand.Next(0,PlayerControl.RndPositionForMonster.Length);
        }
        //gameObject.SetActive(false);
        gameObject.transform.position=PlayerControl.RndPositionForMonster[rnd].transform.position;
        gameObject.SetActive(true);
        isTeleporting=true;
    }
    void RandomMove()
    {
        if (canInvokeRandom)
        {
            isTeleporting=false;
            int rnd = Random.Range(0,3)+1;
            if (!nearWall)
            {
                switch(rnd)
                {
                    case 1:
                        speed = -1f*speedConst;
                        isReady=true;
                        break;
                    case 2:
                        speed = speedConst;
                        isReady=true;
                        break;
                    case 3:
                        isReady=false;
                        speed = 0f;
                        break;
                }
            }
            else
            {
                rnd = Random.Range(0,3)+1;
                switch (rnd)
                {
                    case int i when (i<=2):
                        if (isLastWallRight)
                        {
                            speed = -1f*speedConst;
                            isReady=true;
                        }
                        else
                        {
                            speed = speedConst;
                            isReady=true;
                        }
                        nearWall=false;
                        break;
                    case 3:
                        isReady=false;
                        speed = 0f;
                        break;
                }
                //Debug.Log("nearWall = "+nearWall+" isLastWallRight = "+isLastWallRight + " rnd = "+rnd);
            }
            CancelInvoke("RandomMove");
            Invoke("RandomMove",(float)Random.Range(2,7));
            //Debug.Log("Invoked");
            //Debug.Log(rnd);
        }
    }
    void FixedUpdate()
    {
        if (isReady)
            Run();
        else 
        {
            if (speed>0)
            {
                isFacingRight=true;
            }
            else
            {
                isFacingRight=false;
            }
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            anim.SetFloat("Speed",0f);
        }
        vec = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Run()
    {
        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
        if (speed > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (speed < 0 && isFacingRight)
        {
            Flip();
        }
    }
    public AudioClip breakClip;
    public Sprite newCab;
    GameObject tempGO;
    void FindPlayer()
    {
        PlayerControl.GetPlayer.SetActive(true);
        tempGO.gameObject.GetComponent<HidePlace>().light.SetActive(false);
        tempGO.gameObject.GetComponent<HidePlace>().isHidden = false;
        tempGO.gameObject.GetComponent<SpriteRenderer>().sprite = newCab;
        tempGO.gameObject.GetComponent<HidePlace>().Layout.SetActive(false);
        tempGO.gameObject.GetComponent<HidePlace>().enabled = false;
        PlayerControl.isTriggerEntered=false;
        PlayerControl.isCanEnter=true;
        

        Vector3 tp = transform.position;
        tp.x += (PlayerControl.GetPlayer.transform.position.x - transform.position.x);
        transform.position = tp;
    }
    int chance = 0;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Hide"))
        {
            chance = GameID switch
            {
                int i when (i < 7) => 0,
                int i when (i >= 7 && i < 10) => 2,
                int i when (i >= 10 && i < 14) => 4,
                int i when (i >= 14) => 5,
                _ => 0,
            };
            try
            {
                if (col.gameObject.GetComponent<HidePlace>().isHidden && chance > 0)
                {
                    int rnd;
                    if (!isHunting)
                        rnd = Random.Range(0, 101);
                    else
                        rnd = Random.Range(0, 51);
                    switch (rnd)
                    {
                        case int i when (i<chance):
                            AudioSource.PlayClipAtPoint(breakClip, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                            tempGO = col.gameObject;
                            Invoke("FindPlayer",0.1f);
                        break;
                    }
                }
            }
            catch
            {
                Debug.Log("err");
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("DestroyableWall"))
        {
            col.gameObject.SetActive(false);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            try
            {
                int screenRand=(int) Random.Range(0,KillScreen.Length-1); 
                switch(screenRand)
                {
                    case 0:
                        KSAnim(screenRand);
                        AudioSource.PlayClipAtPoint(killScreenAudio1, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                        break;
                    case 1: 
                        KSAnim(screenRand);
                        break;
                    case 2: 
                        KSAnim(screenRand);
                        break;
                    case 3: 
                        KSAnim(screenRand);
                        break;
                    case 4: 
                        KSAnim5(screenRand);
                        break;
                    default:
                        KSAnim(1);
                        break;
                }
                //Debug.Log(screenRand);
            }
            catch (System.Exception e)
            {
                Debug.Log(e + SceneManager.GetActiveScene().name);
            }
            isReady=false;
            DDOnLoad.StepSoundData.Stop();
            anim.SetBool("isKill",true);
            transform.position += AnimOffset;
            //var rand=new System.Random();
            //int rnd=rand.Next(0,3);
            DDOnLoad.MainMusicData.Pause();
            DDOnLoad.SoundList[Random.Range(0,3)].Play();
            DDOnLoad.HeartBeatData.pitch = 1f;
            DDOnLoad.HeartBeatData.Stop();
            //Debug.Log(rnd);
            PlayerOnCollision.p_isDestroyed=true;
            PlayerControl.GetPlayer.SetActive(false);
            deathCount++;
            if (!isHunting)
            {
                DDOnLoad.SoundList[Random.Range(3,5)].Play();
            }
            Invoke("restart",7f);
            Invoke("StopAnim",1.2f);
        }
        if (col.gameObject.CompareTag("WallSP"))
        {
            if (col.transform.position.x < gameObject.transform.position.x)
            {
                isLastWallRight=false;
                if (!isFacingRight)
                {
                    Flip();
                }
            }
            else
            {
                isLastWallRight=true;
                if (isFacingRight)
                {
                    Flip();
                }
            }
            isReady=false;
            canInvokeRandom=true;
            CancelInvoke("RandomMove");
            nearWall=true;
            Invoke("RandomMove",(float)Random.Range(2,5));
        }
    }
    public AudioClip killScreenAudio1;
    void KSAnim(int index)
    {
        KillScreen[index].SetActive(true);
        KillScreenAnimator[index].SetBool("Kill",true);
    }
    Vector3 GetVector()
    {
        return cameraVec = new Vector3(PlayerControl.GetPlayer.transform.position.x, PlayerControl.GetPlayer.transform.position.y, 0);
    }
    void KSAnim5(int index)
    {
        tempInd = index;
        AudioSource.PlayClipAtPoint(TvEffect, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
        KillScreen[index].SetActive(true);
        KillScreenAnimator[index].SetBool("Kill",true);
        KillScreenAnimator[index+1].SetBool("Kill",true);
        KillScreenAnimator[index+2].SetBool("Kill",true);
        Invoke("KSAnim5_1",3.05f);
    }
    int tempInd = 0;
    Vector3 cameraVec;
    public AudioClip TvEffect;
    void KSAnim5_1()
    {
        KillScreen[tempInd+1].SetActive(true);
        KillScreenAnimator[tempInd+3].SetBool("Kill",true);
    }
    void HuntingStop()
    {
        isHunting=false;
        canInvokeRandom=true;
        isReady=false;
        DDOnLoad.SoundList[11].Stop();
        DDOnLoad.SoundList[12].Stop();
        DDOnLoad.SoundList[13].Stop();
        DDOnLoad.MainMusicData.UnPause();
        DDOnLoad.HeartBeatData.pitch = 1f;
    }
    public static void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = GetMonster.transform.localScale;
        theScale.x *= -1;
        GetMonster.transform.localScale = theScale;
    }
    void StopAnim()
    {
        transform.position -= AnimOffset;
        anim.SetBool("isKill",false);
    }
    public void CancelRestart()
    {
        CancelInvoke("restart");
    }
    void restart()
    {
        foreach (AudioSource el in DDOnLoad.SoundList)
        {
            el.Stop();
        }
        DDOnLoad.MainMusicData.UnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
    void Update()
    {
        //Debug.Log(isTeleporting);
        if (canInvokeStop)
        {
            canInvokeStop=false;
            DDOnLoad.SoundList[Random.Range(11,14)].Play();
            Invoke("HuntingStop",(float)Random.Range(10,30));
        }
        if (isPlayerOnOtherSide && !isHunting)
        {
            isPlayerOnOtherSide=false;
            Invoke("RandomSpawn",0f);
        }
        if (gameObject.transform.lossyScale.x>0)
        {
            isFacingRight=true;
        }
        else
        {
            isFacingRight=false;
        }
    }
}
