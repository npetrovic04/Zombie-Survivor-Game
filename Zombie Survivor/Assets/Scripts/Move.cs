using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{

    private Animator _anim;
    private NavMeshAgent _agent;
    
	void Start ()
    {
        _anim = gameObject.GetComponent<Animator>();
        _agent = gameObject.GetComponent<NavMeshAgent>();
	}
	
	void Update ()
    {
        float forward = Input.GetAxis("Vertical");
        float side = Input.GetAxis("Horizontal");

        _anim.SetFloat("forward", forward);
        _anim.SetFloat("side", side);
	}

    void OnAnimatorMove()
    {
        _agent.velocity = _anim.deltaPosition / Time.deltaTime;
    }
}
