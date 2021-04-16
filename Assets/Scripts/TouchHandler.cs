using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchHandler
{
    public static TouchPosition GetTouchPosition()
    {
        TouchPosition touchPosition = TouchPosition.None;
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
                touchPosition = TouchPosition.Right;
            else if (touch.position.x < Screen.width / 2)
                touchPosition = TouchPosition.Left;
        }
        return touchPosition;
    }
}
public enum TouchPosition
{
    Right,
    Left,
    None
}