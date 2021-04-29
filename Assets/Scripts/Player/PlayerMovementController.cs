using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float moveSpeedToForward;
    [SerializeField] float moveSpeedToSide;
    [SerializeField] float rotationSpeed;

    void Update()
    {
        Direction playerMovementDirection = InputHandler.GetPlayerMovementDirection();
        if(playerMovementDirection == Direction.Right)
        {
            transform.Translate(moveSpeedToSide, 0f, 0f, Space.World);
            transform.Rotate(0f, rotationSpeed, 0f);
        }
        else if (playerMovementDirection == Direction.Left)
        {
            transform.Translate(-moveSpeedToSide, 0f, 0f, Space.World);
            transform.Rotate(0f, -rotationSpeed, 0f);
        }
        transform.Translate(0f, 0f, moveSpeedToForward, Space.World);
    }
}