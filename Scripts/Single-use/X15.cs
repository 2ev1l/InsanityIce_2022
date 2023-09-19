using UnityEngine;

public class X15 : MonoBehaviour
{
    public static bool isMoved;
    public GameObject Sub1;
    public GameObject Sub2;
    public GameObject other;
    private void Start()
    {
        isMoved = false;
        Invoke("Sub", 0.2f);
    }
    void Sub()
    {
        Sub1.SetActive(false);
        Sub2.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !isMoved)
        {
            isMoved = true;
            Sub1.SetActive(true);
            Sub2.SetActive(true);
            Sub1.transform.position = gameObject.transform.position;
            Sub2.transform.position = other.transform.position;
        }
    }
}
