using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public enum ZombieState
    {
        patrol,
        chase,
        attack,
        dead,
        none
    }

    private ZombieState _zombieState;
    private Transform _currentPoint;
    private NavMeshAgent _agent;
    private Transform _playerTransform;
    private PlayerStats _player;
    private Animator _anim;

    public List<Transform> PatrolPoints = new List<Transform>();

    public float patrolSpeed;
    public float chaseSpeed;

    public int HP;
    public float minDmg;
    public float maxDmg;

    public AudioSource zombieSound;
    public AudioSource zombieHit;
    public AudioSource zombieDie;
    public AudioSource zombieAttack;

    void Start ()
    {
        zombieHit.volume = 0.1f;
        zombieDie.volume = 0.2f;
        zombieAttack.volume = 0.3f;

        _anim = gameObject.GetComponent<Animator>();
        _playerTransform = GameObject.Find("Player").transform;
        _player = GameObject.Find("Player").GetComponent<PlayerStats>();
        _agent = gameObject.GetComponent<NavMeshAgent>();

        _currentPoint = PatrolPoints[0];
        _agent.destination = _currentPoint.position;

    }

    public bool IsDead()
    {
        if (_zombieState == ZombieState.dead)
            return true;
        else
            return false;
    }
	
	void Update ()
    {
        if(IsDead() == true)
        {
            zombieSound.volume = Mathf.Lerp(zombieSound.volume, 0, 5f * Time.deltaTime);
            return;
        }

        if (PlayerStats.isDead)
        {
            _zombieState = ZombieState.patrol;
            _anim.SetBool("Chase", false);
            _anim.SetBool("Attack", false);
        }

        switch (_zombieState)
        {
            case ZombieState.patrol:

                _agent.speed = patrolSpeed;
                _agent.destination = _currentPoint.position;

                if (Vector3.Distance(transform.position, _currentPoint.position) < 2)
                {
                    _currentPoint = PatrolPoints[Random.Range(0, PatrolPoints.Count)];
                }
                if (Vector3.Distance(transform.position, _playerTransform.position) < 10 && PlayerStats.isDead == false)
                {
                    _zombieState = ZombieState.chase;
                    _anim.SetBool("Chase", true);
                }

                break;
            case ZombieState.chase:

                _agent.speed = chaseSpeed;
                _agent.destination = _playerTransform.position;

                if (Vector3.Distance(transform.position, _playerTransform.position) > 12)
                {
                    _zombieState = ZombieState.patrol;
                    _anim.SetBool("Chase", false);
                }

                if(Vector3.Distance(transform.position, _playerTransform.position) < 2)
                {
                    //zombieAttack.Play();
                    _zombieState = ZombieState.attack;
                    _anim.SetBool("Attack", true);
                }

                break;
            case ZombieState.attack:

                transform.LookAt(_playerTransform.position);

                if (Vector3.Distance(transform.position, _playerTransform.position) > 3)
                {
                    if(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                    {
                        _zombieState = ZombieState.chase;
                        _anim.SetBool("Attack", false);
                    }
                }

                break;

            case ZombieState.dead:

                break;
            case ZombieState.none:

                break;

            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            HP--;

            zombieHit.Play();

            if(HP < 0)
            {
                zombieDie.Play();

                _agent.destination = transform.position;
                _agent.enabled = false;

                _anim.SetBool("Dead", true);

                _zombieState = ZombieState.dead;

                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void AttackSound()
    {
        zombieAttack.Play();
    }

    public void Hit()
    {
        if(Vector3.Distance(transform.position, _playerTransform.position) < 2.5f)
        {
            zombieHit.Play();

            _player.TakeDamage(Random.Range(minDmg, maxDmg));
        }
    }
}
