using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangerController : MonoBehaviour
{
    [SerializeField] private NewChanger[] changers;

    void Start()
    {
        changers = GetComponentsInChildren<NewChanger>();
    }

    private void OnMouseDown()
    {
        foreach(var changer in changers)
        {
            changer.TakeGrooveIndex();
            changer.ChangeBalls();
        }

    }
}
