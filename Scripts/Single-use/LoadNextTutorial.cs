using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class LoadNextTutorial : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";
    public GameObject Data;
    public GameObject Text;
    bool isEntered=false;
    public string whatToLoad="Tutorial1.1";
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Text.SetActive(true);
            isEntered=true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEntered=false;
            Text.SetActive(false);
            Data.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isEntered && PlayerControl.PauseMenuForButtons.activeSelf==false)
        {
            Data.SetActive(!Data.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Data.SetActive(false);
        }
    }
    public void PressedLoad()
    {
        StartCoroutine("PL");
    }
    IEnumerator PL()
    {
        SaveData data = Saving.GetData();
        data.scene = whatToLoad;
        Saving.Save(data);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(whatToLoad);
        if (asyncLoad.progress == 1f) 
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(whatToLoad));
            yield return null;
        }
    }

    public void PressedExit()
    {
        Data.SetActive(false);
    }
}
