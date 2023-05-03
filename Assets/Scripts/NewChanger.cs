using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewChanger : MonoBehaviour
{
    public GameObject oneBall;
    public GameObject otherBall;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private BallController ballcontroller;
    [SerializeField] private int[] groovesIndexes;
    [SerializeField] private float radius;
    void Start()
    {
        colliders = Physics.OverlapSphere (transform.position, radius);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeGrooveIndex();
            ChangeBalls();
        }
    }

    private void TakeGrooveIndex()
    {
        groovesIndexes = new int[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            groovesIndexes[i] = Array.IndexOf(colliders[i].gameObject.GetComponentInParent<GrooveController>().Grooves, colliders[i].gameObject);
        }
    }

    private void ChangeBalls()
    {
        oneBall = colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]];
        otherBall = colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]];
        GameObject saveNearestGrove = colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().NearestGrove;
        Transform saveTransform = oneBall.transform;
        Transform saveParentTransform = colliders[0].GetComponentInParent<BallController>().transform;
        float saveMyDistanceOnPath = colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().MyDistanceOnPath;

        colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().transform.SetParent(colliders[1].GetComponentInParent<BallController>().transform);
        colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]].GetComponent<PositionTaker>().transform.SetParent(saveParentTransform);

        colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().GetParentComponents();
        colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]].GetComponent<PositionTaker>().GetParentComponents();

        colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]] = oneBall;
        colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]] = otherBall;

        colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().NearestGrove = otherBall.GetComponent<PositionTaker>().NearestGrove;
        colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]].GetComponent<PositionTaker>().NearestGrove = saveNearestGrove;

        colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]].GetComponent<PositionTaker>().MyDistanceOnPath = otherBall.GetComponent<PositionTaker>().MyDistanceOnPath;
        colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].GetComponent<PositionTaker>().MyDistanceOnPath = saveMyDistanceOnPath;

        colliders[0].GetComponentInParent<BallController>().Balls[groovesIndexes[0]].transform.position = otherBall.transform.position;
        colliders[1].GetComponentInParent<BallController>().Balls[groovesIndexes[1]].transform.position = saveTransform.position;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, radius);
    }
}
