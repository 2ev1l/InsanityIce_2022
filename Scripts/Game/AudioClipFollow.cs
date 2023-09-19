using UnityEngine;

public class AudioClipFollow : MonoBehaviour
{
    GameObject Monster;
    void Start()
    {
        Monster = MonsterControl.GetMonster;
    }
    void Update()
    {
        transform.position = new Vector3(Monster.transform.position.x, Monster.transform.position.y, gameObject.transform.position.z);
    }
}
