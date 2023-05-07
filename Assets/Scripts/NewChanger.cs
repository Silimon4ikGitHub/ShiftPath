using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewChanger : MonoBehaviour
{
    public GameObject OneBall;
    public GameObject OtherBall;
    public bool IsMouseDown;

    [SerializeField] private Collider[] colliders;
    [SerializeField] private BallController ballcontroller;
    [SerializeField] private int[] groovesIndexes;
    [SerializeField] private float radius;
    void Start()
    {
        colliders = Physics.OverlapSphere (transform.position, radius);
    }

    public void TakeGrooveIndex()
    {
        groovesIndexes = new int[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            groovesIndexes[i] = Array.IndexOf(colliders[i].gameObject.GetComponentInParent<GrooveController>().Grooves, colliders[i].gameObject);
        }
    }

    public void ChangeBalls()
    {
        
        OneBall = colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]];
        OtherBall = colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]];

        GameObject saveNearestGrove = colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().NearestGrove;
        Transform saveTransform = OneBall.transform;
        Transform saveParentTransform = colliders[0].GetComponentInParent<BallController>().transform;
        float saveMyDistanceOnPath = colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().MyDistanceOnPath;
        float saveTotal = colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().Total;


        colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().transform.SetParent(colliders[1].GetComponentInParent<BallController>().transform);
        colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]].GetComponent<PositionTaker>().transform.SetParent(saveParentTransform);

        colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().GetParentComponents();
        colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]].GetComponent<PositionTaker>().GetParentComponents();

        colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().NearestGrove = OtherBall.GetComponent<PositionTaker>().NearestGrove;
        colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]].GetComponent<PositionTaker>().NearestGrove = saveNearestGrove;


        OneBall.GetComponent<PositionTaker>().MyDistanceOnPath = OtherBall.GetComponent<PositionTaker>().MyDistanceOnPath;
        OtherBall.GetComponent<PositionTaker>().MyDistanceOnPath = saveMyDistanceOnPath;

        OneBall.GetComponent<PositionTaker>().Total = OtherBall.GetComponent<PositionTaker>().Total;
        OtherBall.GetComponent<PositionTaker>().Total = saveTotal;

        colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]] = OneBall;
        colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]] = OtherBall;

    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, radius);
    }
}
