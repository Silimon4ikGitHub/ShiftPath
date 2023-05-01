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
    [SerializeField] private int[] groovePoints;
    [SerializeField] private float radius;
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
    }

    private void ChangeBalls()
    {

    }

    private void TakeGroovesPoint()
    {
        int _groovePointsCounter = 0;
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
        for(int i = 0; i < groovePoints.Length; i++)
        {
            //if (groovePoints[i] )
        }
        
    }

}
