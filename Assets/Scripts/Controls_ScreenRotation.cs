using UnityEngine;

public class Controls_ScreenRotation : MonoBehaviour
{
    public float rotateSpeed = 200f;

    float input = 0f, actualZ = 0f;

    void Awake()
    {
        Physics2D.gravity = new Vector2(0f, -10f);
    }

    void Update()
    {
        input = Input.GetAxis("Horizontal");

        if (Mathf.Approximately(input, 0f))
        {
            input = InputWrapper.instance.inputValue;
        }
        
        if (!Mathf.Approximately(input, 0f))
        {
            transform.Rotate(0f, 0f, input * rotateSpeed * Time.deltaTime);
            actualZ = transform.localRotation.eulerAngles.z;
            Physics2D.gravity = new Vector2(Mathf.Sin(actualZ / (180/Mathf.PI)) * 10f, -Mathf.Cos(actualZ / (180 / Mathf.PI)) * 10f);
        }
    }
}
