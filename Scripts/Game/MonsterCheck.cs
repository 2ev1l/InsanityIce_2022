using UnityEngine;

public class MonsterCheck : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MonsterControl.isPlayerOnOtherSide=true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !MonsterControl.isPlayerOnOtherSide)
        {
            MonsterControl.isPlayerOnOtherSide=true;
        }
    }
}
