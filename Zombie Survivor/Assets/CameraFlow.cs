using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFlow : MonoBehaviour
{
    public float speed;

    public float timer = 5f;

    public string levelName;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position + new Vector3(0, 0, speed * Time.deltaTime);

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            StartLevel(levelName);
        }
	}

    public void StartLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    
}
