using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrooveController : MonoBehaviour
{
    [SerializeField] private PathPlacer pathPlacer;
    public GameObject[] Grooves;

    void Start()
    {
        Grooves = new GameObject[pathPlacer.groovesOnPath.Length];
        BallsArrayFiller();
    }

    private void BallsArrayFiller()
    {
        for (int i = 0; i < Grooves.Length; i++)
        {
            Grooves[i] = pathPlacer.groovesOnPath[i];
        }
    }
}
