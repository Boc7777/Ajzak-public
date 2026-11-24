using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBombs : MonoBehaviour
{
    private Pickups _pickups;
    void Start()
    {
        _pickups = GameObject.Find("HitBox").GetComponent<Pickups>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("HitBox"))
        {
            _pickups.BombsAmount = 99;
            _pickups.BombsText.text= _pickups.BombsAmount.ToString();
            
        }



    }
}
