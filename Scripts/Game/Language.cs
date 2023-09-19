using UnityEngine;
using System.IO;
public class Language : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";
    public GameObject[] ENG;
    public GameObject[] RUS;
    public static GameObject[] ENGshare;
    public static GameObject[] RUSshare;
    void Start()
    {
        ENGshare=ENG;
        RUSshare=RUS;
        LangCheck(); 
    }
    void LangCheck()
    {
        SaveData data = Saving.GetData();
        //Debug.Log(JsonUtility.ToJson(data));
        foreach (var el in ENG)
        {
            el.SetActive(false);
        }
        foreach(var el in RUS)
        {
            el.SetActive(false);
        }
        switch(data.list[3])
        {
            case "ENG":
                foreach(var el in ENG)
                    el.SetActive(true);
                break;
            case "RUS":
                foreach(var el in RUS)
                    el.SetActive(true);
                break;
            default:
                foreach(var el in ENG)
                    el.SetActive(true);
                break;
        }
    }
}
