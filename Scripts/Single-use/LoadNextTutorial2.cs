using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class LoadNextTutorial2 : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DDOnLoad.MusicRandomData1.Stop();
            DDOnLoad.MainMusicData.Play();
            StartCoroutine("PL");
        }
    }
    IEnumerator PL()
    {
        SaveData data = Saving.GetData();
        data.scene = "Game1";
        Saving.Save(data);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game1");
        if (asyncLoad.progress == 1f) 
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game1"));
            yield return null;
        }
    }
}
