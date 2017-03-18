using UnityEngine;

public class AxisInput : MonoBehaviour
{
    private static bool isLeftPressed = false, isRightPressed = false;

    public static float inputTarget = 0f;

    public enum InputType
    {
        ButtonLeft,
        ButtonRight,
        SwipeArea
    }

    public InputType inputType;


    public void OnDrag()
    {
        if (inputType == InputType.SwipeArea)
        {
           //TODO
        }
    }

    public void OnDown()
    {
        Debug.Log("down");

        if(inputType == InputType.ButtonLeft)
        {
            isLeftPressed = true;

            inputTarget = -1;
        }
        else if (inputType == InputType.ButtonRight)
        {
            isRightPressed = true;

            inputTarget = 1;
        }
    }

    public void OnUp()
    {
        if(inputType == InputType.ButtonLeft)
        {
            isLeftPressed = false;
            
            if(isRightPressed)
            {
                inputTarget = 1;
            }
            else
            {
                inputTarget = 0;
            }
        }
        else if (inputType == InputType.ButtonRight)
        {
            isRightPressed = false;

            if (isLeftPressed)
            {
                inputTarget = -1;
            }
            else
            {
                inputTarget = 0;
            }
        }
    }
}
