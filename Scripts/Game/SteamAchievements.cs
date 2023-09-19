using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using Steamworks;
using UnityEngine.SceneManagement;
using System.IO;

public class SteamAchievements : MonoBehaviour
{
    void Update()
    {
        if (SteamManager.Initialized)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Tutorial":
                    Ach("ACH_START");
                    break;
                case "Game104":
                    Ach("ACH_END");
                    break;
            }
            if (MonsterControl.deathCount == 1)
                Ach("ACH_DIE1");
            if (MonsterControl.deathCount == 100)
                Ach("ACH_DIE100");
        }
    }
    public static void Ach(string name)
    {
        if (SteamManager.Initialized)
        {
            Steamworks.SteamUserStats.SetAchievement(name);
            SteamUserStats.StoreStats();
        }
    }

}
