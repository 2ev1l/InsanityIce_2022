using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";
    public static GameObject GetPlayer;
    public static Rigidbody2D rigidBody;
    public static bool isGrounded = false;
    float camera1=0.95f;
    float camera2=0.87f;
    float camera3=0.75f;
    float cameraX;
    public static float move;
    public static bool isFacingRight;
    public GameObject PauseMenu;
    public static GameObject PauseMenuForButtons;
    public Animator animator;
    public static int JumpCount=0;
    public float JumpForce=190f;
    public float MoveForce=2f;
    bool StepBool=false;
    float count=1f;
    float count2=0f;
    public GameObject FlashLight;
    public GameObject Lighter;
    public static bool stopALL=false;
    public GameObject[] RndPosition;
    public static GameObject[] RndPositionForMonster;
    int cpos;
    public static int WhereIsPlayer;
    public static bool isKeyFinded;
    public static bool isTriggerEntered;
    public static bool isCanEnter;
    public static GameObject FlashLightForMonster;
    public static int GameID;
    public static bool canMove=true;
    void Start()
    {
        canMove=true;
        try
        {
            string sceneName = SceneManager.GetActiveScene().name;
            sceneName = sceneName.Remove(0,4);
            GameID=System.Convert.ToInt32(sceneName);
            switch(GameID)
            {
                case int i when (i<11):
                    MoveForce=2f;
                    break;
                case int i when (i>=11 && i<101):
                    MoveForce=1.9f;
                    break;
                case int i when (i>=101):
                    MoveForce=1.7f;
                    break;
            }
        }
        catch
        {
            //Debug.Log("Incorrect scene name: "+ e);
            MoveForce=2f;
        }
        JumpCount=0;
        DDOnLoad.StepSoundData.Stop();
        if (SceneManager.GetActiveScene().name=="Game1")
        {
            isKeyFinded=true;
        }
        else
        {
            isKeyFinded=false;
        }
        isCanEnter=true;
        isTriggerEntered=false;
        Storage.isKeyStored=false;
        FlashLightForMonster=FlashLight;
        //Storage.storageName="";
        if (SceneManager.GetActiveScene().name != "Tutorial" 
        && SceneManager.GetActiveScene().name != "Tutorial1.1" 
        && SceneManager.GetActiveScene().name != "Tutorial1.2" 
        && SceneManager.GetActiveScene().name != "Tutorial1.3" 
        && SceneManager.GetActiveScene().name != "Tutorial2"
        && SceneManager.GetActiveScene().name != "Tutorial2.1" )
        {
            cpos=0;
            RndPositionForMonster=RndPosition;
            foreach (GameObject el in RndPosition)
            {
                cpos++;
            }
            var rand=new System.Random();
            int rnd=rand.Next(0,cpos);
            gameObject.transform.position=RndPosition[rnd].transform.position;
            Follow_Player.GetCamera.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+0.4f,0);
            int rnd2=rand.Next(0,2);
            if (rnd2==0)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
        GetPlayer=gameObject;
        stopALL=false;
        if (Camera.main.aspect >=1.6807)
        {
            cameraX=1f;
        }
        if (Camera.main.aspect >=1.3752 && Camera.main.aspect <1.6807)
        {
            cameraX=camera1;
        }
        if (Camera.main.aspect >=1.0697 && Camera.main.aspect <1.3752)
        {
            cameraX=camera2;
        }
        if (Camera.main.aspect <1.0697)
        {
            cameraX=camera3;
        }
        rigidBody = GetComponent<Rigidbody2D>();
        PauseMenuForButtons=PauseMenu;
        if (gameObject.transform.lossyScale.x>0)
        {
            isFacingRight=true;
        }
        else
        {
            isFacingRight=false;
        }
    }
    void FixedUpdate()
    {
        if (PlayerOnCollision.p_isDestroyed == false && canMove)
        {
            Run();
        }
        else
        {
            animator.SetFloat("Speed", 0);
            DDOnLoad.StepSoundData.Stop();
            animator.SetBool("IsJumping", false);
            rigidBody.velocity = new Vector2(0, 0);
        }
    }

    void Run()
    {
        move = Input.GetAxis("Horizontal")*MoveForce;
        rigidBody.velocity = new Vector2(move, rigidBody.velocity.y);
        if (!stopALL)
            animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
            else
            animator.SetFloat("Speed", 0);
        if (Mathf.Abs(rigidBody.velocity.x)>0)
        {
            if (!StepBool && isGrounded && !stopALL)
            {
                StepBool=true;
                CancelInvoke("StepStop");
                DDOnLoad.StepSoundData.pitch=1f;
                DDOnLoad.StepSoundData.volume=1f;
                DDOnLoad.StepSoundData.Play();
            }
        }
        if (Mathf.Abs(rigidBody.velocity.x)==0)
        {
            if (StepBool)
            {
                StepBool=false;
                count=1f;
                CancelInvoke("StepStop");
                InvokeRepeating("StepStop",0f,0.04f);
            }
        }
        if (!isGrounded && StepBool)
        {
            StepBool=false;
            count=0.5f;
            InvokeRepeating("StepStop",0f,0.01f);
        }
        if (move > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (move < 0 && isFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void StepStop()
    {
        count-=0.1f;
        if (count>0)
        DDOnLoad.StepSoundData.pitch=(count/2)+0.5f;
        DDOnLoad.StepSoundData.volume=count;
        if (count==0f)
        {
            CancelInvoke("StepStop");
            DDOnLoad.StepSoundData.Stop();
        }
    }
    void JumpA()
    {
        if (!stopALL)
        animator.SetBool("IsJumping", true);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            stopALL=true;
            StepBool=false;
            count=1f;
            InvokeRepeating("StepStop",0f,0.04f);
        }
        if (col.gameObject.CompareTag("DestroyableWall"))
        {
            stopALL=true;
            StepBool=false;
            count=1f;
            InvokeRepeating("StepStop",0f,0.04f);
        }
        if (col.gameObject.CompareTag("WallSP"))
        {
            stopALL=true;
            StepBool=false;
            count=1f;
            InvokeRepeating("StepStop",0f,0.04f);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Position"))
        {
            cpos=-1;
            foreach (GameObject el in RndPosition)
            {
                cpos++;
                if (el.name==col.gameObject.name)
                {
                    WhereIsPlayer=cpos;
                    
                }
            }
            //Debug.Log(WhereIsPlayer);
        }
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            //if (JumpCount == 1)
                //JumpCount = 0;
            if (!stopALL)
                DDOnLoad.StepSoundData1.Play();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            //Debug.Log(isGrounded);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
                isGrounded = true;
            //isGrounded=true;
            //if (JumpCount == 1)
                //JumpCount = 0;
            //Debug.Log(isGrounded);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            stopALL=false;
        }
        if (col.gameObject.CompareTag("DestroyableWall"))
        {
            stopALL=false;
        }
        if (col.gameObject.CompareTag("WallSP"))
        {
            stopALL=false;
        }
    }
    int lastTimeJumped;
    void Update()
    {
        if (canMove)
        {
            if (stopALL)
            {
                animator.SetBool("IsJumping", false);
                animator.SetFloat("Speed", 0);
            }
            if (PlayerOnCollision.p_isDestroyed==false && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
            {
                if (isGrounded && Time.frameCount-lastTimeJumped>=120)// && JumpCount==0)
                {
                    lastTimeJumped = Time.frameCount;
                    //Debug.Log(lastTimeJumped);
                    //Debug.Log("Jumped");
                    //JumpCount = 1;
                    isGrounded = false;
                    rigidBody.AddForce(new Vector2(0, JumpForce));
                    Invoke("JumpA",0.1f);
                    DDOnLoad.JumpSoundData.Play();
                }
            }
            if (isGrounded && JumpCount==0)
            {
                animator.SetBool("IsJumping", false);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                /*try
                {
                    GameObject.Find("Monster").SetActive(false);
                }
                catch (System.Exception e) { };*/
                //var rand=new System.Random();
                //int pint=rand.Next(0,11); //(0,11) === [0..10]
                //Debug.Log(pint);
                //DDOnLoad.SoundList[pint].Play();
                //AudioSource.PlayClipAtPoint(clip, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            }
            if (Input.GetKeyDown(KeyCode.F) && PauseMenuForButtons.activeSelf==false && PlayerOnCollision.p_isDestroyed==false)
            {
                if (FlashLight.activeSelf==true)
                {
                    DDOnLoad.FlashLightOnData.Play();
                    FlashLight.SetActive(false);
                }
                else
                {
                    DDOnLoad.FlashLightOnData.Play();
                    FlashLight.SetActive(true);
                    Lighter.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.R) && PauseMenuForButtons.activeSelf==false && PlayerOnCollision.p_isDestroyed==false)
            {
                if (Lighter.activeSelf==true)
                {
                    DDOnLoad.FlashLightOffData.Play();
                    Lighter.SetActive(false);
                }
                else
                {
                    DDOnLoad.FlashLightOffData.Play();
                    FlashLight.SetActive(false);
                    Lighter.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.G) && PauseMenuForButtons.activeSelf==false && PlayerOnCollision.p_isDestroyed==false)
            {
                RotatingLight.isStatic=!RotatingLight.isStatic;
            }
            if (PlayerOnCollision.p_isDestroyed==false && Input.GetKeyDown(KeyCode.Escape))
            {
                if (PauseMenuForButtons.activeSelf==false)
                {
                    PauseMenuForButtons.SetActive(true);
                    Time.timeScale=0f;
                    DDOnLoad.MainMusicData.pitch=0f;
                    DDOnLoad.FlashLightOnData.pitch=0f;
                    DDOnLoad.FlashLightOffData.pitch=0f;
                    DDOnLoad.StepSoundData.pitch=0f;
                    DDOnLoad.StepSoundData1.pitch=0f;
                    foreach (AudioSource el in DDOnLoad.SoundList)
                    {
                        el.pitch=0f;
                    }
                    DDOnLoad.MusicRandomData1.pitch=0f;
                    DDOnLoad.MusicRandomData2.pitch=0f;
                    DDOnLoad.MusicRandomData3.pitch=0f;
                    DDOnLoad.HeartBeatData.pitch = 0f;
                }
                else
                {
                    PauseMenuForButtons.SetActive(false);
                    Time.timeScale=1f;
                    DDOnLoad.MainMusicData.pitch=1f;
                    DDOnLoad.FlashLightOnData.pitch=1f;
                    DDOnLoad.FlashLightOffData.pitch=1f;
                    DDOnLoad.StepSoundData.pitch=1f;
                    DDOnLoad.StepSoundData1.pitch=1f;
                    foreach (AudioSource el in DDOnLoad.SoundList)
                    {
                        el.pitch=1f;
                    }
                    DDOnLoad.MusicRandomData1.pitch=1f;
                    DDOnLoad.MusicRandomData2.pitch=1f;
                    DDOnLoad.MusicRandomData3.pitch=1f;
                    DDOnLoad.HeartBeatData.pitch = 1f;
                }
            }
        }
        else
        {
            if (FlashLight.activeSelf)
                FlashLight.SetActive(false);
            if (Lighter.activeSelf)
                Lighter.SetActive(false);
        }
    }
    //public Vector3 vec;
    //public AudioClip clip;
}
