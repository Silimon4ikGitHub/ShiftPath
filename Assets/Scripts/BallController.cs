using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private PathPlacer pathPlacer;
    [SerializeField] private Transform myPath;
    [SerializeField] private Transform myCanvas;
    [SerializeField] private FixedJoystick JoyStick;
    [SerializeField] private ParentPath parentPath;
    public GameObject[] Balls;
    public float VerticalInput;

    [System.Obsolete]
    void Start()
    {
        Balls = new GameObject[pathPlacer.ballsOnPath.Length];
        myPath = GetComponentInParent<ParentPath>().transform;
        parentPath = myPath.GetComponent<ParentPath>();
        JoyStick = myPath.GetComponentInChildren<FixedJoystick>();
        BallsArrayFiller();
    }

    private void BallsArrayFiller()
    {
        for (int i = 0; i < Balls.Length; i++) 
        {
                Balls[i] = pathPlacer.ballsOnPath[i];
        }
    }
    private void FixedUpdate()
    {
        CheckJoyStickInput();
    }

    private void CheckJoyStickInput()
    {
        if (parentPath.IsRightSidePath)
        VerticalInput = JoyStick.Vertical;

        else if (!parentPath.IsRightSidePath)
            VerticalInput = -JoyStick.Vertical;
    }
}
