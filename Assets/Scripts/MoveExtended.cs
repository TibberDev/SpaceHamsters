using UnityEngine;
using GameFramework.Display.Placement.Components;

public class MoveExtended : FixedMovement
{
    public enum MoveModes
    {
        ConstantSpeed,
        PingPong,
        Once
    }

    [Tooltip("Distance is infinite for Constant mode!")]
    public Vector3 distance;
    [Tooltip("Used for PingPong mode!")]
    public float timeBetweenCycles;
    public MoveModes moveMode;
    [Tooltip("Results in different way of moving, separates distance and speed axes")]
    public bool separateAxes;
    public bool isSmoothed = true;

    private bool movingForward = true, timerRunning = false;
    private float startTime, currentTimer = 0f;
    private Transform cachedTransform;
    private Vector3 startPosition, endPosition;
    Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        cachedTransform = transform;
        startPosition = cachedTransform.localPosition;
        endPosition = cachedTransform.localPosition + distance;

        startTime = Time.time;
    }

    void Update()
    {
        if(timerRunning)
        {
            currentTimer += Time.deltaTime;

            if(currentTimer >= timeBetweenCycles)
            {
                currentTimer = 0f;
                timerRunning = false;
            }
        }
        else
        {
            if (MoveModes.ConstantSpeed == moveMode)
            {
                cachedTransform.Translate(Speed * Time.deltaTime);
            }
            else if (MoveModes.PingPong == moveMode)
            {
                if (separateAxes)
                    cachedTransform.localPosition = new Vector3(startPosition.x + (((distance.x != 0f) ? Mathf.PingPong(Time.time, distance.x) : 0f) * Speed.x), startPosition.y + (((distance.y != 0f) ? Mathf.PingPong(Time.time, distance.y) : 0f) * Speed.y), startPosition.z + (((distance.z != 0f) ? Mathf.PingPong(Time.time, distance.z) : 0f) * Speed.z));
                else
                {
                    if (movingForward)
                    {
                        if(!isSmoothed)
                            cachedTransform.localPosition = Vector3.MoveTowards(cachedTransform.localPosition, endPosition, Speed.magnitude * Time.deltaTime);
                        else
                            cachedTransform.localPosition = Vector3.SmoothDamp(cachedTransform.localPosition, endPosition, ref currentVelocity, 1f, Speed.magnitude);

                        if (Vector3.Distance(cachedTransform.localPosition, endPosition) < .1f)
                        {
                            if(timeBetweenCycles != 0f)
                                timerRunning = true;

                            movingForward = !movingForward;
                        }
                    }
                    else
                    {
                        if (!isSmoothed)
                            cachedTransform.localPosition = Vector3.MoveTowards(cachedTransform.localPosition, startPosition, Speed.magnitude * Time.deltaTime);
                        else
                            cachedTransform.localPosition = Vector3.SmoothDamp(cachedTransform.localPosition, startPosition, ref currentVelocity, 1f, Speed.magnitude);

                        if (Vector3.Distance(cachedTransform.localPosition, startPosition) < .1f)
                        {
                            if (timeBetweenCycles != 0f)
                                timerRunning = true;

                            movingForward = !movingForward;
                        }
                    }
                }
            }
            else if (MoveModes.Once == moveMode)
            {
                if (separateAxes)
                {
                    float duration = distance.magnitude / Speed.magnitude;
                    float t = (Time.time - startTime) / duration;
                    cachedTransform.localPosition = new Vector3(Mathf.SmoothStep(startPosition.x, startPosition.x + distance.x, t), Mathf.SmoothStep(startPosition.y, startPosition.y + distance.y, t), Mathf.SmoothStep(startPosition.z, startPosition.z + distance.z, t));
                }
                else
                {
                    if(!isSmoothed)
                        cachedTransform.localPosition = Vector3.MoveTowards(cachedTransform.localPosition, endPosition, Speed.magnitude * Time.deltaTime);
                    else
                        cachedTransform.localPosition = Vector3.SmoothDamp(cachedTransform.localPosition, startPosition, ref currentVelocity, 1f, Speed.magnitude);
                }
            }
        }       
    }

    void OnValidate()
    {
        cachedTransform = transform;

        cachedTransform.localPosition = startPosition;
        endPosition = startPosition + distance;
    }
}