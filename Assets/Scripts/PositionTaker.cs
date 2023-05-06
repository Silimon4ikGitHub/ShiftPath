using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTaker : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private BallController ballController;
    [SerializeField] private GrooveController grooveController;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Transform myGroove;
    [SerializeField] private Vector3 psn;
    [SerializeField] private Vector3 position;
    [SerializeField] private float speed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float shiftSpeed;
    [SerializeField] private float closestDistance;
    [SerializeField] private float refresh = 100;
    public bool IsChangerInWork;
    public GameObject NearestGrove;
    public float total;
    public float MyDistanceOnPath;
    public bool isMakeSlowMove;
    private GameObject _myGroove;
    void Awake()
    {
        GetParentComponents();
        closestDistance = refresh;
        SearchMyGroove();
        //MakeEqualArrayIndex();
    }

    void Update()
    {
        MoveBalls();
        MoveBallByJoystick();
        SearchMyGroove();
        MakeEqualArrayIndex();
        ShiftinGroove();
    }

    private void MoveBalls()
    {   
        if(currentSpeed != 0)
        {
            total += currentSpeed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(MyDistanceOnPath + total);
        }

    }

    private void ShiftinGroove()
    {
            if (NearestGrove != null)
            if(currentSpeed == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, NearestGrove.transform.position, shiftSpeed);
            }
            
    }

    public void SearchMyGroove()
    {
        if (!IsChangerInWork)
            for (int i = 0; i < grooveController.Grooves.Length; i++)
            {
            if (grooveController.Grooves[i] != null)
                {
                float distance = Vector3.Distance(transform.position, grooveController.Grooves[i].transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        _myGroove = grooveController.Grooves[i];
                    
                    }
                }  
            }
        NearestGrove = _myGroove;
        
    }

    private void MoveBallByJoystick()
    {
        if (ballController.VerticalInput > 0)
        {
            currentSpeed = -speed;
            closestDistance = refresh;
        }
        else if (ballController.VerticalInput < 0)
        {
            currentSpeed = speed;
            closestDistance = refresh;
        }
        else
        {
            currentSpeed = 0;
        }
    }

    public void GetParentComponents()
    {
        pathCreator = GetComponentInParent<PathCreator>();
        ballController = GetComponentInParent<BallController>();
        grooveController = GetComponentInParent<GrooveController>();
    }

    private void MakeEqualArrayIndex()
    {
        if (NearestGrove == null)
        ballController.Balls[Array.IndexOf(grooveController.Grooves, NearestGrove)] = transform.gameObject;
    }
}
