using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Vector3 offset;

    private Transform _playerTransform;

	// Use this for initialization
	void Start ()
    {
        _playerTransform = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
      //  transform.position = _playerTransform.position + offset;      // za grubo kretanje kamere
        transform.position = Vector3.Lerp(transform.position, _playerTransform.position + offset, 5f * Time.deltaTime);
	}
}
