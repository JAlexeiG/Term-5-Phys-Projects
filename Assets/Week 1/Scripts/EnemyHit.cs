using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHit : MonoBehaviour {
    
    private float timer;
    private bool playing;

	// Use this for initialization
	void Start () {
        playing = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && !playing)
        {
            timer = Time.timeSinceLevelLoad;
            playing = true;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log(Time.timeSinceLevelLoad - timer);
            SceneManager.LoadScene(0);
            playing = false;
            Debug.Log("Your score was: " + collision.transform.GetComponent<PlayerMovement>().score);
            CancelInvoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().score += 10;
        Debug.Log("Got 10 points");
    }
}
