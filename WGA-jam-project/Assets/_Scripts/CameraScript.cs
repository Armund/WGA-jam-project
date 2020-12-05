using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;

    public float speed;

    public Vector3 offset;

    protected void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
