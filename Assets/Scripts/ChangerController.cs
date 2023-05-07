using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangerController : MonoBehaviour
{
    [SerializeField] private NewChanger[] changers;
    // Start is called before the first frame update
    void Start()
    {
        changers = GetComponentsInChildren<NewChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
