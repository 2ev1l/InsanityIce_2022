using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ToggleVSYNC : MonoBehaviour
{
    public GameObject checkmark;
    bool check;
    //static readonly string GameSave = "ss.json";

    void Start()
    {
        SaveData data = Saving.GetData();
        if (data.list[2]=="false" || data.list[2]==" " || data.list[2]=="")
        {
            checkmark.SetActive(false);
            check=false;
            QualitySettings.vSyncCount = 0;
        }
        else
        {
            checkmark.SetActive(true);
            check=true;
            QualitySettings.vSyncCount = 1;
        }
    }

    public void VCheck()
    {
        if (check==true)
        {
            QualitySettings.vSyncCount = 0;
            check=false;
            SaveData data = Saving.GetData();
            List<string> Listx = data.list;
            Listx[2]="false";
            data.list = Listx;
            Saving.Save(data);
            checkmark.SetActive(false);
        }
        else
        {
            QualitySettings.vSyncCount = 1;
            check=true;
            SaveData data = Saving.GetData();
            List<string> Listx = data.list;
            Listx[2]="true";
            data.list = Listx;
            Saving.Save(data);
            checkmark.SetActive(true);
        }
    }

}
