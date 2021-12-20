using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUfo : MonoBehaviour {

    public GameObject Ufo;
    public float FirstSpawn, NextSpawn;

	void Start () {
        FirstSpawn = Random.Range(3f, 6f);
        NextSpawn = Random.Range(3f, 6f);
        InvokeRepeating("SpawnUfoPrefab", FirstSpawn, NextSpawn);
	}
	
	void SpawnUfoPrefab () {
        if (!GameObject.Find("UFO"))
        {
            GameObject Go;
            Go = Instantiate(Ufo, transform.position, Quaternion.identity);
            Go.name = "UFO";
        }
	}

    public void UfoStopSpawn()
    {
        CancelInvoke();
    }
}
