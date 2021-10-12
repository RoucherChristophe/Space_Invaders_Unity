using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumpers : MonoBehaviour {

    // variable
    bool detect = true;

    // détect les collisions entre les tags "Aliens" et "Bumpers"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Alien") && detect)
        {
            detect = false;
            collision.GetComponentInParent<Wave>().WaveTouchBumper();
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        detect = true;
    }

}
