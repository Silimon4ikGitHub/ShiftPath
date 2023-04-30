using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private PathPlacer pathPlacer;
    public GameObject[] Balls;

    void Start()
    {
        Balls = new GameObject[pathPlacer.ballsOnPath.Length];
        BallsArrayFiller();
    }

    private void BallsArrayFiller()
    {
        for (int i = 0; i < Balls.Length; i++) 
        {
                Balls[i] = pathPlacer.ballsOnPath[i];
        }
    }
}
