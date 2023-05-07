using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTaker : MonoBehaviour
{
    public float Total;
    public float MyDistanceOnPath;
    public bool IsChangerInWork;
    public bool isMakeSlowMove;

    [SerializeField] private int myType;
    [SerializeField] private float speed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float shiftSpeed;
    [SerializeField] private float closestDistance;
    [SerializeField] private float refresh = 100;

    private GameObject _myGroove;
    public GameObject NearestGrove;
    private PathCreator _pathCreator;
    private BallController _ballController;
    private GrooveController _grooveController;

    void Awake()
    {
        GetParentComponents();

        closestDistance = refresh;

        SearchMyGroove();
    }

    void Update()
    {
        MoveBalls();
        MoveBallByJoystick();

        if (currentSpeed == 0)
        {
            SearchMyGroove();
            ShiftinGroove();
        }
    }

    private void MoveBalls()
    {   
        if(currentSpeed != 0)
        {
            Total += currentSpeed * Time.deltaTime;
            transform.position = _pathCreator.path.GetPointAtDistance(MyDistanceOnPath + Total);
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
        if (closestDistance > 0.01 && currentSpeed == 0)
        {
            for (int i = 0; i < _grooveController.Grooves.Length; i++)
            {
                if (_grooveController.Grooves[i] != null)
                {
                    float distance = Vector3.Distance(transform.position, _grooveController.Grooves[i].transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        _myGroove = _grooveController.Grooves[i];
                    }
                }
            }
            NearestGrove = _myGroove;
            MakeEqualArrayIndex();
        }
    }

    private void MoveBallByJoystick()
    {
        if (_ballController.VerticalInput > 0)
        {
            currentSpeed = -speed;
            closestDistance = refresh;
        }
        else if (_ballController.VerticalInput < 0)
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
        _pathCreator = GetComponentInParent<PathCreator>();
        _ballController = GetComponentInParent<BallController>();
        _grooveController = GetComponentInParent<GrooveController>();
    }

    private void MakeEqualArrayIndex()
    {
        if (NearestGrove != null)
        _ballController.Balls[Array.IndexOf(_grooveController.Grooves, NearestGrove)] = transform.gameObject;
    }
}
