using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTaker : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private BallController ballController;
    [SerializeField] private GrooveController grooveController;
    [SerializeField] private GameObject nearestGrove;
    [SerializeField] private Transform myGroove;
    [SerializeField] private Vector3 position;
    [SerializeField] private int speed;
    [SerializeField] private int currentSpeed;
    [SerializeField] private float shiftSpeed;
    [SerializeField] private float total;
    [SerializeField] private float closestDistance;
    [SerializeField] private float refresh = 100;
    [SerializeField] private Vector3 psn;
    public float MyDistanceOnPath;
    // Start is called before the first frame update
    void Awake()
    {
        pathCreator = GetComponentInParent<PathCreator>();
        ballController = GetComponentInParent<BallController>();
        grooveController = GetComponentInParent<GrooveController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBalls();
        BallControl();
        SearchMyGroove();
        ShiftinGroove();
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
            transform.position = Vector3.MoveTowards(transform.position, nearestGrove.transform.position, shiftSpeed);
        }
    }

    private void SearchMyGroove()
    {
        for (int i = 0; i < grooveController.Grooves.Length; i++)
        {
            
            if (grooveController.Grooves[i] != null)
            {
                float distance = Vector3.Distance(transform.position, grooveController.Grooves[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestGrove = grooveController.Grooves[i];
                    ballController.Balls[i] = transform.gameObject;
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
}
