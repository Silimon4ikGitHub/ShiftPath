using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTaker : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private Vector3 position;
    [SerializeField] private int t;
    [SerializeField] private float total;
    [SerializeField] private Vector3 psn;
    // Start is called before the first frame update
    void Start()
    {
        pathCreator = GetComponentInParent<PathCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        total += t * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(total + transform.position.x + transform.position.z);
        
    }
}
