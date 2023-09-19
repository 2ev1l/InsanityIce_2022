using UnityEngine;
public class LogicWay : MonoBehaviour
{
    public GameObject Layout;
    GameObject Player;
    bool isColEntered = false;
    Vector3 vec;
    public GameObject fakeLogicLift;
    public GameObject logicLift;
    public GameObject logicPanel;
    public AudioClip liftOpen;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public AudioClip errorSound;
    public AudioClip keySound;
    public GameObject arrow;
    public int logicCount = 2;
    public static int openLC;
    public string password;
    string currentPassword;
    bool isPasswordCorrect = false;
    bool isUsed = false;
    public GameObject findText;
    void Start()
    {
        if (openLC != 0)
            openLC = 0;
        Layout.SetActive(false);
    }
    Vector3 GetVector()
    {
        return new Vector3(logicPanel.transform.position.x, logicPanel.transform.position.y, 0);
    }
    Vector3 GetLiftVector()
    {
        return new Vector3(logicLift.transform.position.x, logicLift.transform.position.y, 0);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !PlayerControl.isTriggerEntered && !isUsed)
        {
            PlayerControl.isTriggerEntered = true;
            isColEntered = true;
            Player = col.gameObject;
            Layout.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && PlayerControl.isCanEnter && !isUsed)
        {
            Player = col.gameObject;
            PlayerControl.isCanEnter = false;
            PlayerControl.isTriggerEntered = true;
            isColEntered = true;
            Layout.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isColEntered = false;
            Layout.SetActive(false);
            PlayerControl.isTriggerEntered = false;
            PlayerControl.isCanEnter = true;
            logicPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (PlayerControl.PauseMenuForButtons.activeSelf && logicPanel.activeSelf)
        {
            ClosePanel();
        }
        if (Input.GetKeyDown(KeyCode.E) && isColEntered && PlayerControl.canMove && !isUsed)
        {
            if (!logicPanel.activeSelf && !PlayerControl.PauseMenuForButtons.activeSelf)
            {
                OpenPanel();
            }
            else
            {
                ClosePanel();
                isColEntered = false;
                PlayerControl.isTriggerEntered = true;
                PlayerControl.isCanEnter = false;
            }
        }
    }
    public void CheckPassword()
    {
        try
        {
            if (!PlayerControl.isKeyFinded && !isUsed)
            {
                if (password.Length == currentPassword.Length && password == currentPassword)
                {
                    isPasswordCorrect = true;
                    AudioSource.PlayClipAtPoint(keySound, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
                    AudioSource.PlayClipAtPoint(correctSound, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
                    openLC += 1;
                    isUsed = true;
                    Layout.SetActive(false);
                    ResetArrow();
                    if (openLC == logicCount)
                    {
                        PlayerControl.isKeyFinded = true;
                        logicLift.SetActive(true);
                        fakeLogicLift.SetActive(false);
                        AudioSource.PlayClipAtPoint(liftOpen, GetLiftVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
                        findText.SetActive(true);
                    }
                    ClosePanel();
                }
                else
                {
                    isPasswordCorrect = false;
                    AudioSource.PlayClipAtPoint(incorrectSound, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 25f) / 50f);
                    ResetPassword();
                    //Debug.Log("Incorrect " + currentPassword);
                }
            }
        }
        catch 
        {
            //Debug.Log(e);
            isPasswordCorrect = false;
            AudioSource.PlayClipAtPoint(incorrectSound, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 25f) / 50f);
            ResetPassword();
        }
    }
    void ClosePanel()
    {
        logicPanel.SetActive(false);
    }
    void OpenPanel()
    {
        logicPanel.SetActive(true);
    }
    public void ResetPassword()
    {
        if (!PlayerControl.isKeyFinded)
        {
            AudioSource.PlayClipAtPoint(keySound, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
            currentPassword = "";
            ResetArrow();
        }
    }
    void ResetArrow()
    {
        Color tmp = arrow.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        arrow.GetComponent<SpriteRenderer>().color = tmp;
        //Debug.Log("alphaR = " + arrow.GetComponent<SpriteRenderer>().color.a);
    }
    void SetActiveArrow()
    {
        Color tmp = arrow.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        arrow.GetComponent<SpriteRenderer>().color = tmp;
        //Debug.Log("alpha = " + arrow.GetComponent<SpriteRenderer>().color.a);
    }
    void RotateArrow(int degree)
    {
        SetActiveArrow();
        Quaternion rotation = Quaternion.AngleAxis(degree, Vector3.forward);
        arrow.transform.rotation = rotation;
        //Debug.Log("rotation = " + arrow.transform.rotation);
    }
    void CheckPress(string str)
    {
        try
        {
            if (!PlayerControl.isKeyFinded)
            {
                if (currentPassword.Length < password.Length)
                {
                    currentPassword += str;
                    AudioSource.PlayClipAtPoint(keySound, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
                    switch (str)
                    {
                        case "U":
                            RotateArrow(-90);
                            break;
                        case "D":
                            RotateArrow(90);
                            break;
                        case "R":
                            RotateArrow(180);
                            break;
                        case "L":
                            RotateArrow(0);
                            break;
                        default:
                            //ResetArrow();
                            break;
                    }
                }
                else
                {
                    AudioSource.PlayClipAtPoint(errorSound, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
                }
            }
        }
        catch
        {
            //Debug.Log(e);
            AudioSource.PlayClipAtPoint(keySound, GetVector(), (PlayerPrefs.GetFloat("optionvalueS", 0) + 50f) / 50f);
            currentPassword += str;
            switch (str)
            {
                case "U":
                    RotateArrow(-90);
                    break;
                case "D":
                    RotateArrow(90);
                    break;
                case "R":
                    RotateArrow(180);
                    break;
                case "L":
                    RotateArrow(0);
                    break;
                default:
                    //ResetArrow();
                    break;
            }
        }
    }
    public void ButtonUp()
    {
        CheckPress("U");
    }
    public void ButtonDown()
    {
        CheckPress("D");
    }
    public void ButtonLeft()
    {
        CheckPress("L");
    }
    public void ButtonRight()
    {
        CheckPress("R");
    }
}
