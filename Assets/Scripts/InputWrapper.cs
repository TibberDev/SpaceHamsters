using UnityEngine;

public class InputWrapper : MonoBehaviour
{
    public static InputWrapper instance;

    [HideInInspector]
    public float inputValue = 0f;

    private float yVelocity = 0f;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(!Mathf.Approximately(inputValue, AxisInput.inputTarget))
        {
            inputValue = Mathf.SmoothDamp(inputValue, AxisInput.inputTarget, ref yVelocity, .125f);
        }
    }
}
