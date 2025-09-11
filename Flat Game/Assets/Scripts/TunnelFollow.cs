using UnityEngine;

public class TunnelFollow : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 offset;

    void Update()
    {
        transform.position = cameraTransform.position + offset;
    }
}
