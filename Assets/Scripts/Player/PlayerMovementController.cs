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
            transform.Translate(moveSpeedToSide * Time.deltaTime, 0f, 0f, Space.World);
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
        else if (playerMovementDirection == Direction.Left)
        {
            transform.Translate(-moveSpeedToSide * Time.deltaTime, 0f, 0f, Space.World);
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
        transform.Translate(0f, 0f, moveSpeedToForward * Time.deltaTime, Space.World);
    }
}