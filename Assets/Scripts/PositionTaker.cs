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
    public GameObject NearestGrove;
    [SerializeField] private Transform myGroove;
    [SerializeField] private Vector3 position;
    [SerializeField] private float speed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float shiftSpeed;
    public float total;
    [SerializeField] private float closestDistance;
    [SerializeField] private float refresh = 100;
    [SerializeField] private Vector3 psn;
    public float MyDistanceOnPath;
    public bool isMakeSlowMove;
    void Awake()
    {
        GetParentComponents();
    }

    void Update()
    {
        MoveBalls();
        BallControl();
        SearchMyGroove();
        ShiftinGroove();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            MakeEqualArrayIndex();
            SearchMyGroove();
        }

        if (currentSpeed == 0)
        {
            SearchMyGroove();
        }
    }

    private void MoveBalls()
    {
        total += currentSpeed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(MyDistanceOnPath + total);
    }

    private void ShiftinGroove()
    {
        if (currentSpeed == 0)
        for (int i = 0; i < grooveController.Grooves.Length; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, NearestGrove.transform.position, shiftSpeed);
        }
    }

    public void SearchMyGroove()
    {
        if (currentSpeed == 0)
            for (int i = 0; i < grooveController.Grooves.Length; i++)
        {
            if (grooveController.Grooves[i] != null)
            {
                float distance = Vector3.Distance(transform.position, grooveController.Grooves[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    NearestGrove = grooveController.Grooves[i];
                }
            }
                
        }
    }

    private void BallControl()
    {
        if (Input.GetKey(KeyCode.Space))
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
        ballController.Balls[Array.IndexOf(grooveController.Grooves, NearestGrove)] = transform.gameObject;
    }

}
