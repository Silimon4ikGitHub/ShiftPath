using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private PathPlacer pathPlacer;
    public GameObject[] balls;

    // Start is called before the first frame update
    void Start()
    {
        balls = new GameObject[pathPlacer.objectsOnPath.Length];
        BallsArrayFiller();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BallsArrayFiller()
    {
        for (int i = 0; i < balls.Length; i++) 
        {
            
                balls[i] = pathPlacer.objectsOnPath[i];
            
        }
    }
}
