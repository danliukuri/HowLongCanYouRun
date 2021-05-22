using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - targetTransform.position;
    }
    void LateUpdate()
    {
        transform.position = targetTransform.position + offset;
    }
}