using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ContinueCheck : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";
    void Start()
    {
        SaveData data = Saving.GetData();
        if (data.scene == " " || data.scene == "")
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
