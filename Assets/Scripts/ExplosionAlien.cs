using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAlien : MonoBehaviour {

    // variables
    SpriteRenderer sr;
    // pour enregistrer le son
    AudioSource audioSource;
    // delai avant destruction de l'objet
    public float Delay = 0.1f;

    private void Awake()
    {
        // Récupère les components
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start () {
        StartCoroutine(DestroyExplosion());
	}
	
	IEnumerator DestroyExplosion()
    {
        // lancement du son
        audioSource.Play();
        // on donne le délai pour exécuter la destruction
        yield return new WaitForSeconds(Delay);
        // on décrémente le nombre d'Alien
        GameObject.Find("Wave").GetComponent<Wave>().Reste_alien -= 1;
        // on détruit ce gameObject aprés le délai
        Destroy(this.gameObject, Delay);
    }
}
