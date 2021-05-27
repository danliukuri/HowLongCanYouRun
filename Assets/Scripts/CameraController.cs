using UnityEngine;
using Utilities;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] MoveAndRotateToTargetBehaviour moveAndRotateToTheFloorBehaviour;
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
    public void MoveAndRotateToTheFloor()
    {
        moveAndRotateToTheFloorBehaviour.enabled = true;
        this.enabled = false;
    }
}