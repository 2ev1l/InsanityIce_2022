using UnityEngine;

public class Ladder : MonoBehaviour
{
    public GameObject Layout;
    GameObject Player;
    bool isColEntered;
    //public AudioClip clip;
    public GameObject Link;
    //Vector3 vec;
    public GameObject CountText;
    SpriteRenderer CT;
    public Sprite[] Sprites;
    int c=10;
    bool isInvoking;
    void Start()
    {
        isInvoking=false;
        c=10;
        //vec = new Vector3(Link.transform.position.x, Link.transform.position.y, 0);
        isColEntered=false;
        Layout.SetActive(false);
        CT=CountText.GetComponent<SpriteRenderer>();
        CountText.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !PlayerControl.isTriggerEntered)
        {
            PlayerControl.isTriggerEntered=true;
            Player=col.gameObject;
            isColEntered=true;
            Layout.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && PlayerControl.isCanEnter)
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
        if (col.gameObject.CompareTag("Player"))
        {
            isColEntered=false;
            Layout.SetActive(false);
            PlayerControl.isTriggerEntered=false;
            PlayerControl.isCanEnter=true;
            CancelInvoke("CountDown");
            isInvoking=false;
            c=10;
            CT.sprite=Sprites[9];
            CountText.SetActive(false);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColEntered && !isInvoking && PlayerControl.canMove)
        {
            CountText.SetActive(true);
            InvokeRepeating("CountDown",0f,0.1f);
            isInvoking=true;
        }
    }
    
    void CountDown()
    {
        if (!isColEntered)
        {
            CancelInvoke("CountDown");
            isInvoking=false;
            c=10;
            CT.sprite=Sprites[9];
            CountText.SetActive(false);
        }
        else
        {
            c--;
            if (c>=0)
            {
                CT.sprite=Sprites[c];
            }
            else
            {
                c=10;
                if (Link != null)
                {
                    Player.transform.position = Link.transform.position;
                    Follow_Player.GetCamera.transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y+0.4f,0);
                    CT.sprite=Sprites[9];
                    CountText.SetActive(false);
                    PlayerControl.isTriggerEntered=false;
                    PlayerControl.isCanEnter=true;
                    CancelInvoke("CountDown");
                    isInvoking=false;
                    //AudioSource.PlayClipAtPoint(clip, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                    //DDOnLoad.StepSoundData.Stop();
                }
            }
        }
    }
}
