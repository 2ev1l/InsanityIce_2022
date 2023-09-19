using UnityEngine;

public class BloodNumbers : MonoBehaviour
{
    Color texture;
    public bool ultraviolet;
    bool isPInvoking = false;
    bool isMInvoking = false;
    float repeatRate = 0.05f;
    public float alphaStep = 0.05f;
    public GameObject[] toChange;
    void Start()
    {
        texture = GetComponent<SpriteRenderer>().color;
        if (ultraviolet)
        {
            texture.a = 0f;
            for (int i=0; i<toChange.Length; i++)
                toChange[i].GetComponent<SpriteRenderer>().color=texture;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Point Light 2D flashlight" && ultraviolet && !isPInvoking && texture.a<1f)
        {
            isPInvoking = true;
            CancelInvoke("AlphaMinus");
            isMInvoking = false;
            InvokeRepeating("AlphaPlus",0f,repeatRate);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Point Light 2D flashlight" && ultraviolet && !isMInvoking && texture.a>0f)
        {
            isMInvoking = true;
            CancelInvoke("AlphaPlus");
            isPInvoking = false;
            InvokeRepeating("AlphaMinus",0f,repeatRate);
        }
    }

    void AlphaPlus()
    {
        texture.a += alphaStep;
        for (int i=0; i<toChange.Length; i++)
            toChange[i].GetComponent<SpriteRenderer>().color=texture;
        if (texture.a >= 0.99f)
        {
            texture.a = 1f;
            for (int i=0; i<toChange.Length; i++)
                toChange[i].GetComponent<SpriteRenderer>().color=texture;
            isPInvoking = false;
            CancelInvoke("AlphaPlus");
        }
    }

    void AlphaMinus()
    {
        texture.a -= alphaStep;
        for (int i=0; i<toChange.Length; i++)
            toChange[i].GetComponent<SpriteRenderer>().color=texture;
        if (texture.a <= 0.01f)
        {
            texture.a = 0f;
            for (int i=0; i<toChange.Length; i++)
                toChange[i].GetComponent<SpriteRenderer>().color=texture;
            isMInvoking = false;
            CancelInvoke("AlphaMinus");
        }
    }
}
