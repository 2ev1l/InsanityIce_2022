using UnityEngine;
using UnityEngine.SceneManagement;
public class Clearing : MonoBehaviour
{
    public GameObject Layout;
    GameObject Player;
    bool isColEntered;
    public AudioClip Default;
    Vector3 vec;
    public GameObject CountText;
    private int c=3;
    bool isChecked=false;
    bool isInvoking=false;
    public Sprite Clear;
    public Sprite[] Sprites;
    SpriteRenderer CT;
    public static int countWork;
    int completeNeeded=1;
    public GameObject textIsFinded;
    void Start()
    {
        if (SceneManager.GetActiveScene().name=="Tutorial1.1")
            completeNeeded=2;
        if (SceneManager.GetActiveScene().name=="Tutorial1.2")
            completeNeeded=1;
        countWork=0;
        CT=CountText.GetComponent<SpriteRenderer>();
        vec = new Vector3(Layout.transform.position.x, Layout.transform.position.y, 0);
        isColEntered=false;
        Layout.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isChecked && !PlayerControl.isTriggerEntered)
        {
            PlayerControl.isTriggerEntered=true;
            Player=col.gameObject;
            isColEntered=true;
            Layout.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isChecked && PlayerControl.isCanEnter)
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
        if (col.gameObject.CompareTag("Player") && !isChecked)
        {
            PlayerControl.isTriggerEntered=false;
            PlayerControl.isCanEnter=true;
            isColEntered=false;
            Layout.SetActive(false);
            c=3;
            CT.sprite=Sprites[2];
            CountText.SetActive(false);
            CancelInvoke("CountDown");
            c=3;
            CT.sprite=Sprites[2];
            CountText.SetActive(false);
            isInvoking=false;
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColEntered && !isChecked && !isInvoking)
        {
            CountText.SetActive(true);
            InvokeRepeating("CountDown",0f,1f);
            isInvoking=true;
        }
    }
    void CountDown()
    {
        if (!isColEntered)
        {
            CancelInvoke("CountDown");
            c=3;
            CT.sprite=Sprites[2];
            CountText.SetActive(false);
            isInvoking=false;
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
                countWork++;
                gameObject.GetComponent<SpriteRenderer>().sprite=Clear;
                c=3;
                isChecked=true;
                Layout.SetActive(false);
                CountText.SetActive(false);
                AudioSource.PlayClipAtPoint(Default, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                isChecked=true;
                PlayerControl.isTriggerEntered=false;
                PlayerControl.isCanEnter=true;
                if (countWork==completeNeeded)
                {
                    textIsFinded.SetActive(true);
                    PlayerControl.isKeyFinded=true;
                }
                CancelInvoke("CountDown");
            }
        }
    }
}
