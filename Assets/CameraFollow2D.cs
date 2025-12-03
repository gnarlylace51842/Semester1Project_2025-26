using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 5f; //how quickly camera moves (higher = snappier)
    public Vector3 offset; //optional offset from player

    void LateUpdate()
    {
        if (target == null) return; //basically returns nothing

        Vector3 desiredPos = new Vector3(transform.position.x, target.position.y + offset.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
    }
}
