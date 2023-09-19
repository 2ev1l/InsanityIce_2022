using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            MonsterControl.canInvokeRandom=false;
            if (PlayerControl.GetPlayer.transform.position.x < MonsterControl.GetMonster.transform.position.x)
            {
                if (!MonsterControl.isHunting)
                {
                    MonsterControl.speed = Random.Range(-250, -230) / 100f;
                    MonsterControl.isReady=true;
                    MonsterControl.isHunting=true;
                    DDOnLoad.SoundList[Random.Range(3,5)].Play();
                    DDOnLoad.MainMusicData.Pause();
                    MonsterControl.canInvokeStop=true;
                    if (!DDOnLoad.HeartBeatData.isPlaying)
                    {
                        DDOnLoad.HeartBeatData.Play();
                    } 
                    DDOnLoad.HeartBeatData.pitch = 1.2f;
                }
                else
                {
                    MonsterControl.speed = Random.Range(-250, -230) / 100f;
                    MonsterControl.isReady=true;
                }
            }
            else
            {

                if (!MonsterControl.isHunting)
                {
                    MonsterControl.speed = Random.Range(230, 250) / 100f;
                    MonsterControl.isReady=true;
                    MonsterControl.isHunting=true;
                    DDOnLoad.SoundList[Random.Range(3,5)].Play();
                    DDOnLoad.MainMusicData.Pause();
                    MonsterControl.canInvokeStop=true;
                    if (!DDOnLoad.HeartBeatData.isPlaying)
                    {
                        DDOnLoad.HeartBeatData.Play();
                    }
                    DDOnLoad.HeartBeatData.pitch = 1.2f;
                }
                else
                {
                    MonsterControl.speed = Random.Range(230, 250) / 100f;
                    MonsterControl.isReady=true;
                }
            }
        }
    }
}
