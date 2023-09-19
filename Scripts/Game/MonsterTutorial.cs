using UnityEngine;
using UnityEngine.SceneManagement;
public class MonsterTutorial : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";
    public float speed=-2.3f;
    public Animator anim;
    public static Rigidbody2D rigidBody;
    public static bool isReady=false;
    public GameObject KillScreen;
    public Animator KillScreenAnimator;
    public Vector3 AnimOffset;
    void Start()
    {
        KillScreenAnimator.SetBool("Kill",false);
        KillScreen.SetActive(false);
        anim.SetBool("isKill",false);
        rigidBody=gameObject.GetComponent<Rigidbody2D>();
        isReady=false;
    }

    void FixedUpdate()
    {
        if (isReady)
            Run();
        else 
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            anim.SetFloat("Speed",0f);
        }
    }

    void Run()
    {
        rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("DestroyableWall"))
        {
            col.gameObject.SetActive(false);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            KillScreen.SetActive(true);
            KillScreenAnimator.SetBool("Kill",true);
            isReady=false;
            DDOnLoad.StepSoundData.Stop();
            anim.SetBool("isKill",true);
            transform.position += AnimOffset;
            DDOnLoad.ScreamerData1.Play();
            PlayerOnCollision.p_isDestroyed=true;
            PlayerControl.GetPlayer.SetActive(false);
            Invoke("restart",7f);
            Invoke("StopAnim",1.2f);
        }
    }
    void StopAnim()
    {
        transform.position -= AnimOffset;
        anim.SetBool("isKill",false);
    }
    void restart()
    {
        DDOnLoad.MusicRandomData1.Stop();
        DDOnLoad.MainMusicData.Play();
        SceneManager.LoadScene("Tutorial2.1");
    }
}
