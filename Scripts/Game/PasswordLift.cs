using UnityEngine;
using UnityEngine.UI;

public class PasswordLift : MonoBehaviour
{
    public GameObject Layout;
    GameObject Player;
    bool isColEntered;
    public AudioClip open;
    Vector3 vec;
    public GameObject PasswordLiftX;
    public string keyPassword;
    string password;
    bool isPasswordCorrect;
    public AudioClip passwordError;
    public AudioClip passwordIncorrect;
    public AudioClip keyPressed;
    public GameObject passwordPanel;
    public Text[] passwordText;
    public GameObject Cell;
    public GameObject findText;
    public GameObject textPanel;
    bool isObjEntered;

    void Start()
    {
        passwordPanel.SetActive(false);
        isColEntered=false;
        Layout.SetActive(false);
        ClosePanel();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !PlayerControl.isTriggerEntered)
        {
            PlayerControl.isTriggerEntered=true;
            isColEntered=true;
            Player = col.gameObject;
            isObjEntered = true;
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
            isObjEntered = true;
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
            isObjEntered = false;
            ClosePanel();
        }
    }
    
    void Update()
    {
        if (PlayerControl.PauseMenuForButtons.activeSelf || !PlayerControl.GetPlayer.activeSelf)
        {
            ClosePanel();
        }
        if (isObjEntered && Input.GetKeyDown(KeyCode.E) && PlayerControl.canMove && !PlayerControl.PauseMenuForButtons.activeSelf)
        {
            isColEntered=false;
            PlayerControl.isTriggerEntered=true;
            PlayerControl.isCanEnter=false;
            if (!passwordPanel.activeSelf && !PlayerControl.isKeyFinded)
            {
                OpenPanel();
            }
            else
            {
                ClosePanel();
            }
            //Debug.Log("Key = "+ PlayerControl.isKeyFinded + "; panel = " + passwordPanel.activeSelf);
        }

        if (isPasswordCorrect && PlayerControl.canMove)
        {
            PlayerControl.isKeyFinded=true;
            findText.SetActive(true);
            isPasswordCorrect=false;
            AudioSource.PlayClipAtPoint(open, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            PasswordLiftX.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            Cell.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            Layout.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
            Invoke("Event",1f);
            Invoke("Event2",0.8f);
        }
    }
    Vector3 GetVector()
    {
        return new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }
    void Event2()
    {
        foreach (var item in passwordText)
        {
            item.text="";
        }
    }
    void Event()
    {
        ClosePanel();
        gameObject.SetActive(false);
    }
    void ClosePanel()
    {
        textPanel.SetActive(false);
        passwordPanel.SetActive(false);
    }
    void OpenPanel()
    {
        textPanel.SetActive(true);
        passwordPanel.SetActive(true);
    }
    public void CheckPassword()
    {
        try
        {
            if (!PlayerControl.isKeyFinded)
            {
                if (password.Length==keyPassword.Length && password==keyPassword)
                {
                    isPasswordCorrect=true;
                    AudioSource.PlayClipAtPoint(keyPressed, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                    foreach (var item in passwordText)
                    {
                        int i = 0;
                        item.text="";
                        while (i<keyPassword.Length)
                        {
                            item.text+="#";
                            i++;
                        }
                    }
                }
                else
                {
                    isPasswordCorrect=false;
                    AudioSource.PlayClipAtPoint(passwordIncorrect, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0)+25f)/50f);
                    ResetPassword();
                }
            }
        }
        catch 
        {
            isPasswordCorrect=false;
            AudioSource.PlayClipAtPoint(passwordIncorrect, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0)+25f)/50f);
            ResetPassword();
        }
    }
    public void ResetPassword()
    {
        if (!PlayerControl.isKeyFinded)
        {
            AudioSource.PlayClipAtPoint(keyPressed, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
            password ="";
            WritePassword();
        }
    }
    void WritePassword()
    {
        if (!PlayerControl.isKeyFinded)
        {
            foreach (var item in passwordText)
            {
                item.text=password.ToString();
            }
        }
    }
    void CheckPress(string nmb)
    {
        try
        {
            if (!PlayerControl.isKeyFinded)
            {
                if (password.Length<keyPassword.Length)
                {
                    password+=nmb;
                    AudioSource.PlayClipAtPoint(keyPressed, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(passwordError, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
                }
            }
        }
        catch 
        {
            password+=nmb;
            AudioSource.PlayClipAtPoint(keyPressed, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
        }
    }
    public void N0()
    {
        CheckPress("0");
        WritePassword();
    }
    public void N1()
    {
        CheckPress("1");
        WritePassword();
    }
    public void N2()
    {
        CheckPress("2");
        WritePassword();
    }
    public void N3()
    {
        CheckPress("3");
        WritePassword();
    }
    public void N4()
    {
        CheckPress("4");
        WritePassword();
    }
    public void N5()
    {
        CheckPress("5");
        WritePassword();
    }
    public void N6()
    {
        CheckPress("6");
        WritePassword();
    }
    public void N7()
    {
        CheckPress("7");
        WritePassword();
    }
    public void N8()
    {
        CheckPress("8");
        WritePassword();
    }
    public void N9()
    {
        CheckPress("9");
        WritePassword();
    }
}
