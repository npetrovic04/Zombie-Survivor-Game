using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float HP;

    public static bool isDead;

    private Animator _anim;

	// Use this for initialization
	void Start ()
    {
        isDead = false;
        _anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void TakeDamage(float damage)
    {
        if (isDead == true)
            return;

        HP -= damage;

        if (HP <= 0)
        {
            isDead = true;

            _anim.SetBool("Dead", true);
        }
    }
}
