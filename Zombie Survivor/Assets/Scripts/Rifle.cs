using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rifle : MonoBehaviour
{
    public float shootInterval;
    public Transform barrel;
    public GameObject bullet;

    private NavMeshAgent _agent;
    private Animator _anim;
    private AudioSource _audioSource;

	void Start ()
    {
        _agent = gameObject.transform.root.gameObject.GetComponent<NavMeshAgent>();
        _anim = gameObject.transform.root.gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.volume = 0.5f;
    }
	
	void Update ()
    {
        if (PlayerStats.isDead)
        {
            CancelInvoke();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("ShootBullet", 0, shootInterval);
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke();
        }
    }

    public void ShootBullet()
    {
        GameObject tmpBullet = GameObject.Instantiate(bullet, barrel.position, Quaternion.identity);
        
        tmpBullet.transform.up = barrel.right;

        tmpBullet.GetComponent<Bullet>().SetSpeed(_agent.velocity);

        _audioSource.pitch = 1.3f + Random.Range(0.3f, 0.5f);
        _audioSource.Play();

        _anim.Play("Shoot", 1, 0);
    }
}
