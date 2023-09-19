using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
public class Serializer : MonoBehaviour
{
    static readonly string GameSave = "ss.data";
    
    void Awake()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, GameSave)))
        {
            int x = 800;
            int y = 600;
            try
            {
                foreach (var res in Screen.resolutions)
                {
                    x = res.width;
                    y = res.height;
                }
            }
            catch { Debug.LogError("???"); };
            SaveData data = new SaveData();
            List<string> List = new List<string> { x.ToString(), y.ToString(), "false", "ENG", " ", " " };
            data.scene = " ";
            data.checkpoint = " ";
            data.list = List;
            Saving.Save(data);
            //Debug.Log(JsonUtility.ToJson(Saving.GetData()));
        }
    }
}
