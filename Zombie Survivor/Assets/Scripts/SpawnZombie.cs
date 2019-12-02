using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnZombie : MonoBehaviour
{
    public List<GameObject> zombieToInstantiate;

    public List<Transform> PatrolPoints = new List<Transform>();

    private Zombie _currentZombie;
    private Transform _playerTransform;

    // Use this for initialization
    void Start ()
    {
        InstantiateZombie();
        _playerTransform = GameObject.Find("Player").transform;

        Renderer[] _allRenderers = gameObject.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < _allRenderers.Length; i++)
        {
            _allRenderers[i].enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(_currentZombie.IsDead() == true)
        {
            if(transform.position.z + 12 < _playerTransform.position.z)
            {
                InstantiateZombie();
            }
        }
	}

    public void InstantiateZombie()
    {
        GameObject tmpZombie = GameObject.Instantiate(zombieToInstantiate[Random.Range(0,zombieToInstantiate.Count)]);
        Vector3 startPos = PatrolPoints[Random.Range(0,PatrolPoints.Count)].position;
        tmpZombie.GetComponent<NavMeshAgent>().Warp(startPos);

        tmpZombie.GetComponent<Zombie>().PatrolPoints = PatrolPoints;
        _currentZombie = tmpZombie.GetComponent<Zombie>();
    }
}
