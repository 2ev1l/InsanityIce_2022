using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CC2 : MonoBehaviour
{
    public GameObject Audio;
    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void Start()
    {
        Audio.SetActive(true);
        Audio.SetActive(false);
    }
}
