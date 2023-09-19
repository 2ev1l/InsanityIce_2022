using UnityEngine;
using System.IO;

public class X100 : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";
    void Start()
    {
        SaveData data = Saving.GetData();
        data.checkpoint = " ";
        Saving.Save(data);
    }
}
