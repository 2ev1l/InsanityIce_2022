using UnityEngine;

public class Storage : MonoBehaviour
{
    public static bool isKeyStored;
    //public static string storageName;
    public GameObject Layout;
    GameObject Player;
    bool isColEntered;
    public AudioClip KeySound;
    public AudioClip Default;
    Vector3 vec;
    bool isChecked;
    public GameObject CountText;
    SpriteRenderer CT;
    public Sprite[] Sprites;
    private int c=3;
    bool isInvoking;
    public GameObject textIsFinded;
    void Start()
    {
        isInvoking=false;
        c=3;
        vec = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        isColEntered=false;
        textIsFinded.SetActive(false);
        Layout.SetActive(false);
        isChecked=false;
        GameObject[] st = GameObject.FindGameObjectsWithTag("Storage");
        if (st.Length > 0)
        {
            isKeyStored=true;
        }
        else
        {
            int rnd = Random.Range(0,10);
            if (rnd==5 && !isKeyStored)
            {
                isKeyStored=true;
                gameObject.tag = "Storage";
                //Debug.Log("Key stored in" + gameObject.name);
            }
            InvokeRepeating("Check",0.1f,0.1f);
        }
        CT=CountText.GetComponent<SpriteRenderer>();
        CountText.SetActive(false);
    }
    void Check()
    {
        if (!isKeyStored)
        {
            int rnd = Random.Range(0,10);
            if (rnd==5)
            {
                isKeyStored=true;
                gameObject.tag = "Storage";
                //Debug.Log("After invoke key stored in" + gameObject.name);
            }
        }
        else
        {
            CancelInvoke("Check");
        }
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
                c=3;
                isChecked=true;
                Layout.SetActive(false);
                CountText.SetActive(false);
                if (gameObject.CompareTag("Storage"))
                {
                    AudioSource.PlayClipAtPoint(KeySound, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                    GameObject[] arr = GameObject.FindGameObjectsWithTag("Lever");
                    if (arr.Length==0)
                    {
                        textIsFinded.SetActive(true);
                        PlayerControl.isKeyFinded=true;
                    }
                    else
                    {
                        if (!Lever.leverPressed)
                        {
                            Lever.keyFinded=true;
                        }
                        else
                        {
                            Lever.leverPressed=false;
                            textIsFinded.SetActive(true);
                            PlayerControl.isKeyFinded=true;
                        }
                    }
                }
                else
                {
                    AudioSource.PlayClipAtPoint(Default, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                }
                PlayerControl.isTriggerEntered=false;
                PlayerControl.isCanEnter=true;
                CancelInvoke("CountDown");
                isInvoking=false;
            }
        }
    }
}
