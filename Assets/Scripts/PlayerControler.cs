using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour {

    // variables de position
    Vector2 PositionPlayer;
    float speed = 5f;
    float limitx = 8.2f;
    // variable du tir
    public GameObject BulletPrefab;
    Transform ejectPosition;
    public bool tir = true;
    // variable de score
    Text TxtScore;
    // variable pour la vague
    Wave WaveScript;
    bool detect = true;

    private int score = 0;
    // getter setter pour pouvoir modifier le score
    public int Score
    {
        // quand on veut le score
        get
        {
            return score;
        }
        // pour modifier le score je le défini dans "value", je l'envoie dans "TxtScore"
        set
        {
            score = value;
            TxtScore.text = "Score : " + score;
        }
    }

    // début du jeu, on récupère les éléments du jeu
	void Start () {
        PositionPlayer = transform.position;
        ejectPosition = transform.Find("Eject");
        TxtScore = GameObject.Find("TxtScore").GetComponent<Text>();
        WaveScript = GameObject.Find("Wave").GetComponent<Wave>();
    }
	
    // actions du jeu
    
	void Update () {
        MovePlayer();
        PlayerShoot();

    }

    // déplacement de droite à gauche, avec limitation du cadre de jeu
    void MovePlayer()
    {
        if (tir)
        {
            PositionPlayer.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            PositionPlayer.x = Mathf.Clamp(PositionPlayer.x, -limitx, limitx);
            transform.position = PositionPlayer;
        }      
    }

    // tir du "Player"
    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && tir)
        {
            tir = false;
            Instantiate(BulletPrefab, ejectPosition.position, Quaternion.identity);
        }
    }

    //Entre dans le trigger du player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Alien"))
        {
            detect = false;
            StartCoroutine(AlienKillPlayer());
        }
    }


    //Temporisation de 0.2f (evite les collisions multiples)
    IEnumerator AlienKillPlayer()
    {
        WaveScript.StopWave();
        PlayerExplosion();
        GameObject.Find("TxtLive").GetComponent<Lives>().LoseSlot();//Perd un slot
        yield return new WaitForSeconds(0.2f);
        detect = true;
        WaveScript.RestartWave(1f);
    }

    //Methode explosion du player
    void PlayerExplosion()
    {
        GetComponent<Animator>().SetTrigger("explosion");//Lance l'animation dans l'animator
        GetComponent<AudioSource>().Play();//Joue le son
        tir = false;//ne peut plus tirer
    }

    //Initialisation du player
    public void InitPlayer() 
    {
        GetComponent<Animator>().SetTrigger("normal");//Lance l'animation dans l'animator
        tir = true;//peut tirer
    }
}
