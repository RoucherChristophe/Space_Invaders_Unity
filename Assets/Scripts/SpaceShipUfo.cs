using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipUfo : MonoBehaviour {

    public float Speed;
	void Start () {
        Speed = Random.Range(2f, 6f);
	}
	
	void Update () {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }

    
}
