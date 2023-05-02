using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Changer : MonoBehaviour
{
    [SerializeField] private Collider[] colliders;
    [SerializeField] private GameObject[] grooves;
    [SerializeField] private GameObject[] ballsOneSide;
    [SerializeField] private GameObject[] ballsOtherSide;
    [SerializeField] private GameObject[] pathes;
    [SerializeField] private BallController _oneSidePath;
    [SerializeField] private BallController _otherSidePath;
    [SerializeField] private int[] groovePoints;
    [SerializeField] private float radius;
    [SerializeField] private bool isOneSide;
    void Start()
    {
        SelectObjects();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DivideObjects();
            Debug.Log(radius);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, radius);
    }

    private void SelectObjects()
    {
        colliders = Physics.OverlapSphere(transform.position, radius);
    }

    private void DivideObjects()
    {
        int pathesCounter = 0;
        int ballCounter = 0;
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<BallController>())
            {
                pathes[pathesCounter] = colliders[i].gameObject;
                pathesCounter++;
            }
            if (colliders[i].CompareTag("Groove"))
            {
                grooves[ballCounter] = colliders[i].gameObject;
                ballCounter++;
            }
        }
        TakeGroovesPoint();
        DivideGrooves();
        ChangeBalls();
    }

    private void ChangeBalls()
    {
        _oneSidePath = ballsOneSide[0].GetComponentInParent<BallController>();
        _otherSidePath = ballsOtherSide[0].GetComponentInParent<BallController>();
        GameObject saver;
        _oneSidePath.Balls[groovePoints[0]].transform.SetParent(_otherSidePath.transform);
        saver = _oneSidePath.Balls[groovePoints[0]];
        _oneSidePath.Balls[groovePoints[0]] = _otherSidePath.Balls[groovePoints[2]];
        _oneSidePath.Balls[groovePoints[0]].GetComponent<PositionTaker>().total = _otherSidePath.Balls[groovePoints[2]].GetComponent<PositionTaker>().total;
        _oneSidePath.Balls[groovePoints[0]].GetComponent<PositionTaker>().GetParentComponents();

        _otherSidePath.Balls[groovePoints[2]].transform.SetParent(_oneSidePath.transform);
        _otherSidePath.Balls[groovePoints[2]] = saver;
        _otherSidePath.Balls[groovePoints[2]].GetComponent<PositionTaker>().total = _oneSidePath.Balls[groovePoints[2]].GetComponent<PositionTaker>().total;
        _otherSidePath.Balls[groovePoints[2]].GetComponent<PositionTaker>().GetParentComponents();

        /*

        _otherSidePath.Balls[groovePoints[3]].transform.SetParent(_oneSidePath.transform);
        _otherSidePath.Balls[groovePoints[3]] = saver;
        _otherSidePath.Balls[groovePoints[3]].GetComponent<PositionTaker>().total = saver.GetComponent<PositionTaker>().total;
        _otherSidePath.Balls[groovePoints[3]].GetComponent<PositionTaker>().GetParentComponents();


        _oneSidePath.Balls[groovePoints[1]].transform.SetParent(_otherSidePath.transform);
        saver = _oneSidePath.Balls[groovePoints[1]];
        _oneSidePath.Balls[groovePoints[1]] = _otherSidePath.Balls[groovePoints[3]];
        //_oneSidePath.Balls[groovePoints[1]].GetComponent<PositionTaker>().total = saver.GetComponent<PositionTaker>().total;
        _oneSidePath.Balls[groovePoints[1]].GetComponent<PositionTaker>().GetParentComponents();
        
        */
        _otherSidePath.Balls[groovePoints[2]].GetComponent<PositionTaker>().GetParentComponents();
    }

    private void TakeGroovesPoint()
    {
        for (int i = 0; i < grooves.Length; i++)
        {
            if (grooves[i] != null)
            {
                groovePoints[i] = Array.IndexOf(grooves[i].GetComponentInParent<GrooveController>().Grooves, grooves[i]);
            }
        }
    }
    private void DivideGrooves()
    {
        ballsOneSide = new GameObject[2];
        ballsOtherSide = new GameObject[2];
        ballsOneSide[0] = grooves[0];
        ballsOneSide[1] = grooves[1];
        ballsOtherSide[0] = grooves[2];
        ballsOtherSide[1] = grooves[3];
    }
}
