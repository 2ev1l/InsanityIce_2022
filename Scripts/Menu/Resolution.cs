using System;
using UnityEngine;
using Steamworks;
using System.IO;
using System.Collections.Generic;
public class Resolution : MonoBehaviour
{
    //static readonly string GameSave = "ss.json";
    public GameObject[] ResArray;
    int x,y,z;
    bool check;
    void Awake()
    {   
        foreach (var res in Screen.resolutions)
        {
            x=res.width; 
            y=res.height;
        }
        z=x*y;
        SaveData data = Saving.GetData();
        if (data.list[2]=="false" || data.list[2]==" ")
        {
            check=false;
        }
        else
        {
            check=true;
        }
        if (data.list[0]!=" " && data.list[1]!=" ")
            Screen.SetResolution(ReadX(), ReadY(), Screen.fullScreen);
    }

    int ReadX()
    {
        SaveData data = Saving.GetData();
        return Convert.ToInt32(data.list[0]);
    }

    int ReadY()
    {
        SaveData data = Saving.GetData();
        return Convert.ToInt32(data.list[1]);
    }

    List<string> ReadList()
    {
        SaveData data = Saving.GetData();
        return data.list;
    }
    void PasteXY(List<string> List)
    {
        SaveData data = Saving.GetData();
        data.list = List;
        Saving.Save(data);
    }

    void Start()
    {
        int i=1;
        int x1;
        string buf;
        int y1;
        int z1;
        while (i<=14)
        {
            buf=ResArray[i].name.ToString().Remove(0,9);
            y1=Convert.ToInt32(buf);
            buf=ResArray[i].name.ToString().Remove(0,4);
            buf=buf.Remove(4,4);
            if (buf.Length>4)
                buf=buf.Remove(4,1);
            x1=Convert.ToInt32(buf);
            z1=x1*y1;
            if (z<z1)
                ResArray[i].SetActive(false);
            i++;
        };
        
    }
    int nmb;
    public void R0()
    {
        nmb=0;
    }
    public void R1()
    { 
        nmb=1;
    }

    public void R2()
    {
        nmb=2;
    }

    public void R3()
    {
        nmb=3;
    }

    public void R4()
    {
        nmb=4;
    }

    public void R5()
    {
        nmb=5;
    }

    public void R6()
    {
        nmb=6;
    }

    public void R7()
    {
        nmb=7;
    }

    public void R8()
    {
        nmb=8;
    }

    public void R9()
    {
        nmb=9;
    }

    public void R10()
    {
        nmb=10;
    }

    public void R11()
    {
        nmb=11;
    }

    public void R12()
    {
        nmb=12;
    }

    public void R13()
    {
        nmb=13;
    }
    public void R14()
    {
        nmb=14;
    }
    public void PressedSetResolution()
    {
        string buf;
        int x1;
        int y1;
        if (nmb==0)
        {
            buf=ResArray[nmb].name.ToString().Remove(0,8);
            y1=Convert.ToInt32(buf);
            buf=ResArray[nmb].name.ToString().Remove(0,4);
            buf=buf.Remove(3,4);
            x1=Convert.ToInt32(buf);
        }
        else
        {
            buf=ResArray[nmb].name.ToString().Remove(0,9);
            y1=Convert.ToInt32(buf);
            buf=ResArray[nmb].name.ToString().Remove(0,4);
            buf=buf.Remove(4,4);
            if (buf.Length>4)
                buf=buf.Remove(4,1);
            x1=Convert.ToInt32(buf);
        }
        List<String> List = ReadList();
        List[0]=x1.ToString();
        List[1]=y1.ToString();
        PasteXY(List);
        Screen.SetResolution(x1, y1, Screen.fullScreen);
    }
}
