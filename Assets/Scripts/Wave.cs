using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    // variables positions Aliens
    public GameObject[] AlienType;
    public float SpaceColumns = 1f, SpaceRows = 0.7f;
    public int TotalAlienInLigne = 10;

    // variables mouvements
    public bool CanMoove = true;
    public bool WalkRight = true;
    public float WaveStepRight = 1f, WaveStepDown = 0.3f, WaveSpeed = 0.8f;

    // variables sons
    public AudioClip[] ClipAudio;
    int curClip = 0;
    AudioSource audioSource;

    // variables du nombres d'Aliens
    public int total_alien_in_wave;
    public int Reste_alien;

    //Redemarrage de la vague
    Vector2 PositionInitialWave;
    PlayerControler playerController;

    private void Awake()
    {
        // Générateur de vague
        // boucle pour positionner sur y (hauteur) un alien sur chaque ligne 
        for (int i = 0; i < AlienType.Length; i++)
        {
            // calcul de la position Y : transform.position.y = 0 (position de Wave) moin (puisqu'on veut descendre) l'espacement des lignes * par i
            float posY = transform.position.y - (SpaceRows * i);
            // boucle pour créer le nombre d'aliens par ligne
            for (int j = 0; j < TotalAlienInLigne; j++)
            {
                // utilisation de "Vector 2" pour positionner chaque Alien en X et Y 
                // calcul de la position X : transform.position.x = 0 (position de Wave) plus (puisqu'on va à droite) l'espacement des colones * par j
                Vector2 pos = new Vector2(transform.position.x + (SpaceColumns * j), posY);
                // on instancie un GameObject pour créer et positionner des Aliens identique en colonnes et différents à chaque ligne
                // pour chaque Alien on le positionne en X et Y avec le quaternion pour une rotation de dépar3.t
                GameObject Go = Instantiate(AlienType[i].gameObject, pos, Quaternion.identity);
                // lier les Aliens à Wave
                Go.transform.SetParent(this.transform);
                // on renomme les Aliens
                Go.name = "Alien" + (j + 1) + "-row:" + (i + 1);
            }
        }
        // Assignation du nombres d'Aliens
        // on compte le nombre d'enfants créés par le générateur dAlien
        total_alien_in_wave = transform.childCount;
        // on récupére le résultat dans "Reste_alien"
        Reste_alien = total_alien_in_wave;


        //Position Initial
        PositionInitialWave = transform.position;
        playerController = GameObject.Find("Player").GetComponent<PlayerControler>();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(MooveWave());
    }

    // mouvements de la vague
    IEnumerator MooveWave()
    {
        // déplacements
        while (CanMoove)
        {
            // si la vague est vide, on arrète le jeu
            IsWaveEmpty();
            // si WalkRight est vrai alors direction vaux right, sinon direction vaux left
            Vector2 direction = WalkRight ? Vector2.right : Vector2.left;
            // on tranmet la direction * par le nombre de pas à effectuer
            transform.Translate(direction * WaveStepRight);
            // animations
            BroadcastMessage("AnimateAlien");
            // son de la vague
            PlayWaveSound();
            // temps de déplacement
            yield return new WaitForSeconds(WaveSpeed);
        }          
    }  

    // fonction qui inverse la valeur de "WalkRight" 
    public void WaveTouchBumper()
    {
        WalkRight = !WalkRight;
        // on tranmet la direction * par le nombre de pas à effectuer
        transform.Translate(Vector2.down * WaveStepDown);
    }

    // fonction qui joue les sons chacun leur tours
    void PlayWaveSound()
    {
        // si curClip est inférieur à 3 alors on ajoute 1 à curClip, sinon on remet curClip à 0
        curClip = curClip < ClipAudio.Length - 1 ? curClip += 1 : curClip = 0;
        // joue le son du tableau ClipAudio avec la valeur de curClip
        audioSource.PlayOneShot(ClipAudio[curClip]);
    }

    // fonction quand la vague n'a plus d'Alien
    void IsWaveEmpty()
    {
        if (Reste_alien == 0)
        {
            print("Fini!!!!!");
            // on arrète toutes les coroutines
            StopAllCoroutines();
            // on stop la création d'UFO
            GameObject.Find("SpawnPointUfo").GetComponent<SpawnUfo>().UfoStopSpawn();
        }
    }

    // Stop la vague
    public void StopWave()
    {
        StopAllCoroutines();
    }

    //Redemarre la vague
    public void RestartWave(float delay)
    {
        StartCoroutine(Restart(delay));
    }

    IEnumerator Restart(float delay)
    {
        yield return new WaitForSeconds(delay); //temporisation
        transform.position = PositionInitialWave; //Deplacement de la vague a la position de départ
        StartCoroutine(MooveWave());//Déplace la vague
        playerController.InitPlayer();//affichage du player
    }
}
