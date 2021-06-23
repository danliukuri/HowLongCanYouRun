using UnityEngine;

public static class InputHandler
{
    public static Direction GetPlayerMovementDirection()
    {
        Direction playerMovementDirection = Direction.None;
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
                playerMovementDirection = Direction.Right;
            else if (touch.position.x < Screen.width / 2)
                playerMovementDirection = Direction.Left;
        }
#elif UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetButton("Horizontal"))
        {
            float horizontalAxisvalue = Input.GetAxis("Horizontal");
            if (horizontalAxisvalue > 0)
                playerMovementDirection = Direction.Right;
            else if (horizontalAxisvalue < 0)
                playerMovementDirection = Direction.Left;
        }
#endif
        return playerMovementDirection;
    }
}

public enum Direction
{
    Right,
    Left,
    None
}