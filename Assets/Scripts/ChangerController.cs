using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangerController : MonoBehaviour
{
    [SerializeField] private NewChanger changer;
    // Start is called before the first frame update
    void Start()
    {
        changer = GetComponentInChildren<NewChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        changer.TakeGrooveIndex();
        changer.ChangeBalls();
    }
}
