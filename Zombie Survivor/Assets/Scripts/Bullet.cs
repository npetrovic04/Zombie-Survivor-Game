using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

	
	void Start ()
    {
        Invoke("DestroyAfterSeconds", 5);
	}
	
	void Update ()
    {
        transform.position += transform.up * (-1) * speed * Time.deltaTime;
	}

    public void DestroyAfterSeconds()
    {
        GameObject.Destroy(gameObject);
    }

    public void SetSpeed(Vector3 characterSpeed)
    {
        speed += characterSpeed.magnitude; 
    }

    void OnTriggerEnter()
    {
        GameObject.Destroy(gameObject);
    }
}
