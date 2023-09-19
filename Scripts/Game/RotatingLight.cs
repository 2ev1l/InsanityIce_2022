using UnityEngine;

public class RotatingLight : MonoBehaviour
{
    public float rotationSpeed=5f;
    public static bool isStatic=false;
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        if (PlayerControl.isFacingRight && !isStatic)
        {
            if (angle<90f && angle >-90f)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed*Time.deltaTime);
            else
            {
                if (angle>-90f)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(0, Vector3.forward), rotationSpeed*Time.deltaTime);
                if (angle<90f)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(180, Vector3.forward), rotationSpeed*Time.deltaTime);
            }
        }
        if (!PlayerControl.isFacingRight && !isStatic)
        {
            if (angle>90f || angle <-90f)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed*Time.deltaTime);
            else
            {
                if (angle<0f)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(180, Vector3.forward), rotationSpeed*Time.deltaTime);
                if (angle>=0f)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(0, Vector3.back), rotationSpeed*Time.deltaTime);
            }
        }
    }
}
