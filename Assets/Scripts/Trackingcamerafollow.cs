using UnityEngine;

public class TrackingCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -8f);
    public float smoothSpeed = 6f;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desired = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
    }
}
