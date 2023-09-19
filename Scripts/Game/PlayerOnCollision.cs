using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOnCollision : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";
    public static bool p_isDestroyed;

    void Start()
    {
        p_isDestroyed = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.gameObject.tag == "Ground")
        {
            PlayerControl.isGrounded=true;
            PlayerControl.JumpCount=0;
            if (!PlayerControl.stopALL)
                DDOnLoad.StepSoundData1.Play();
        }*/
    }
    void OnTriggerStay2D(Collider2D other)
    {
        /*if (other.gameObject.tag == "Ground" && PlayerControl.JumpCount==0 && !PlayerControl.isGrounded)
        {
            PlayerControl.isGrounded=true;
        }*/
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //PlayerControl.isGrounded=false;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "LoadGame102")
        {
            SceneManager.LoadScene("Game102");
        }
    }
}
