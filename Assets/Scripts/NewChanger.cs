using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewChanger : MonoBehaviour
{
    [SerializeField] private Collider[] colliders;
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
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, radius);
    }

    private void TakeGrooveIndex()
    {
        groovesIndexes = new int[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            groovesIndexes[i] = Array.IndexOf(colliders[i].gameObject.GetComponentInParent<GrooveController>().Grooves, colliders[i].gameObject);
        }
    }
}
