using UnityEngine;
using UnityEngine.SceneManagement;

public class Follow_Player : MonoBehaviour {
    public Transform player;
    public float smoothTime=0.2f;
    public Vector3 offset;
    public Vector3 velocity;
    public static Camera cam;
    public static GameObject GetCamera;
    float playerOldVelocity;
    public static bool freeCam;
    void Start()
    {
        freeCam=false;
        GetCamera=gameObject;
        cam = GetComponent<Camera>();
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y+0.4f,0);
        if(SceneManager.GetActiveScene().name=="Tutorial" || SceneManager.GetActiveScene().name=="Tutorial1.3")
        {
            GetComponent<Camera>().orthographicSize = 10f;
            InvokeRepeating("CSDS0",0f,0.01f);
        }
    }
    void CSDS0()
    {
        GetComponent<Camera>().orthographicSize -= 0.01f;
        if (GetComponent<Camera>().orthographicSize <= 8f)
        {
            GetComponent<Camera>().orthographicSize = 8f;
            CancelInvoke("CSDS0");
            InvokeRepeating("CSDS",0f,0.01f);
        }
    }
    void CSDS()
    {
        GetComponent<Camera>().orthographicSize -= 0.015f;
        if (GetComponent<Camera>().orthographicSize <= 5f)
        {
            GetComponent<Camera>().orthographicSize = 5f;
            CancelInvoke("CSDS");
            InvokeRepeating("CSDS1",0f,0.01f);
        }
    }
    void CSDS1()
    {
        GetComponent<Camera>().orthographicSize -= 0.02f;
        if (GetComponent<Camera>().orthographicSize <= 2f)
        {
            GetComponent<Camera>().orthographicSize = 2f;
            CancelInvoke("CSDS1");
        }
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Tutorial" 
        && SceneManager.GetActiveScene().name != "Tutorial1.1" 
        && SceneManager.GetActiveScene().name != "Tutorial1.2" 
        && SceneManager.GetActiveScene().name != "Tutorial1.3" 
        && SceneManager.GetActiveScene().name != "Tutorial2"
        && SceneManager.GetActiveScene().name != "Tutorial2.1" 
        && !freeCam)
        {
            if (Mathf.Abs(PlayerControl.GetPlayer.GetComponent<Rigidbody2D>().velocity.x) < playerOldVelocity)
            {
                if (GetComponent<Camera>().orthographicSize >= 1.5f)
                {
                    GetComponent<Camera>().orthographicSize -= 0.01f;
                }
            }
            if (Mathf.Abs(PlayerControl.GetPlayer.GetComponent<Rigidbody2D>().velocity.x) > playerOldVelocity)
            {
                if (GetComponent<Camera>().orthographicSize <= 1.6f)
                {
                    GetComponent<Camera>().orthographicSize += 0.01f;
                }
            }
            if (Mathf.Abs(PlayerControl.GetPlayer.GetComponent<Rigidbody2D>().velocity.x) == playerOldVelocity)
            {
                if (playerOldVelocity == 0f && PlayerControl.isGrounded)
                {
                    GetComponent<Camera>().orthographicSize = 1.5f;
                }
            }
            playerOldVelocity=Mathf.Abs(PlayerControl.GetPlayer.GetComponent<Rigidbody2D>().velocity.x);
            //Debug.Log(playerOldVelocity);
        }
    }
    void Update() 
    {
        if (!freeCam)
        {
            Vector3 desiredPosition = new Vector3(player.position.x, transform.position.y, 0) + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        }
    }
}