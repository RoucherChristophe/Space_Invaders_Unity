using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    //Variables 
    public GameObject[] slots; //Tableau de gameobjects
    public int remainingSlot; //Slot(s) restants

    private void Awake()
    {
        //Assignations variables
        slots = GameObject.FindGameObjectsWithTag("Slot");
        remainingSlot = slots.Length;
    }

    //Methode publique : perte de slot
    public void LoseSlot()
    {
        remainingSlot -= 1;

        switch (remainingSlot)
        {
            case 2:
                slots[0].SetActive(false);
                break;

            case 1:
                slots[1].SetActive(false);
                break;

            case 0:
                slots[2].SetActive(false);
                GameOver();
                break;
        }
    }

    //Game Over
    void GameOver()
    {
        print("Game Over");        
    }
}
