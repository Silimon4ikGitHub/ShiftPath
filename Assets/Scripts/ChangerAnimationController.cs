using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject _oneBall;
    [SerializeField] private GameObject _otherBall;
    [SerializeField] private Transform _oneBallParent;
    [SerializeField] private Transform _otherBallParent;
    [SerializeField] private NewChanger changer;
    // Start is called before the first frame update
    void Start()
    {
        changer = GetComponent<NewChanger>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _oneBall = changer.OneBall;
        _otherBall = changer.OtherBall;

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("IsSwap", true);
        }
    }

    public void SaveParentTransform()
    {
        _oneBallParent = _oneBall.GetComponentInParent<Transform>();
        _otherBallParent = _otherBall.GetComponentInParent<Transform>();
    }
    public void SetBallsInChanger()
    {
        _oneBall.transform.SetParent(transform);
        _otherBall.transform.SetParent(transform);
    }

    public void BakcBallsInChanger()
    {
        _oneBall.transform.SetParent(_oneBallParent);
        _otherBall.transform.SetParent(_otherBallParent);
    }

    public void EndAnimation()
    {
        animator.SetBool("IsSwap", false);
        _oneBall.GetComponent<PositionTaker>().enabled = true;
        _oneBall.GetComponent<PositionTaker>().enabled = true;
    }

    public void UnableMoution()
    {
        _oneBall.GetComponent<PositionTaker>().enabled = false;
        _oneBall.GetComponent<PositionTaker>().enabled = false;
    }
}
