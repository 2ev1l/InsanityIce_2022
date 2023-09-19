using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class Saving : MonoBehaviour
{
    static readonly string GameSave = "ss.data";
    void Start()
    {
        SaveData data = GetData();
        if (data.scene != SceneManager.GetActiveScene().name)
        {
            data.scene = SceneManager.GetActiveScene().name;
            Save(data);
        }
    }
    public static void Save(SaveData data)
    {
        string filename = Path.Combine(Application.persistentDataPath, GameSave);
        string json = Crypt.AESEncrypt(JsonUtility.ToJson(data));
        FileStream fsc = new FileStream(filename, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(fsc, json);
        //Debug.Log(json);
        fsc.Close();
    }
    public static SaveData GetData()
    {
        string filename = Path.Combine(Application.persistentDataPath, GameSave);
        FileStream fso = new FileStream(filename, FileMode.Open);
        BinaryFormatter converter = new BinaryFormatter();
        string json = converter.Deserialize(fso).ToString();
        json = Crypt.AESDecrypt(json);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        //Debug.Log(json);
        fso.Close();
        return data;
    }
}
