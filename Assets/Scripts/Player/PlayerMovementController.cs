using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float moveSpeedToForward;
    [SerializeField] float moveSpeedToSide;
    [SerializeField] float rotationSpeed;

    void Update()
    {
        TouchPosition touchPosition = TouchHandler.GetTouchPosition();
        if(touchPosition == TouchPosition.Right)
        {
            transform.Translate(moveSpeedToSide * Vector3.right, Space.World);
            transform.Rotate(0f, rotationSpeed, 0f);
        }
        else if (touchPosition == TouchPosition.Left)
        {
           transform.Translate(moveSpeedToSide * Vector3.left, Space.World);
           transform.Rotate(0f, -rotationSpeed, 0f);
        }
        transform.Translate(moveSpeedToForward * Vector3.forward, Space.World);
    }
}