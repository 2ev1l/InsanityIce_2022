using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break1 : MonoBehaviour
{
    public GameObject seta;
    public GameObject wall;
    public AudioClip clip;
    Vector3 vec;
    void Start()
    {
        vec = new Vector3(wall.transform.position.x, wall.transform.position.y, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(clip, vec, (PlayerPrefs.GetFloat("optionvalueS", 0)+50f)/50f);
            seta.SetActive(false);
            wall.SetActive(true);
        }
    }
}
