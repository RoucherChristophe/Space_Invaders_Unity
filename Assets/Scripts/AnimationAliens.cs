using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAliens : MonoBehaviour {

    // variables
    public Sprite Sprite2;
    Sprite Sprite1;

    // initialisation du "Sprite" avant lancement du jeu
	private void Awake ()
    {
        Sprite1 = GetComponent<SpriteRenderer>().sprite;
	}
	
    // fonction qui change de sprite à chaque appel
    void AnimateAlien()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Sprite nextSprite = sr.sprite == Sprite1 ? Sprite2 : Sprite1;
        sr.sprite = nextSprite;
    }
}
