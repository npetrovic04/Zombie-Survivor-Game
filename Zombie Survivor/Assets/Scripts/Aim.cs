using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{

    Ray _cameraRay;
    RaycastHit _groundResult;
    LayerMask _groundMask = 1 << 9;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerStats.isDead)
            return;


        _cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(_cameraRay.origin, _cameraRay.direction, Color.red, 1);

		if(Physics.Raycast(_cameraRay, out _groundResult, 100, _groundMask))
        {
            if(Vector3.Distance(transform.position, _groundResult.point) > 1)
            {
                transform.LookAt(_groundResult.point);
            }

            Debug.DrawRay(_groundResult.point, Vector3.up, Color.yellow, 1);
        }
	}
}
