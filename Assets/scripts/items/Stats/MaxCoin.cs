using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCoin : MonoBehaviour
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
            _pickups.CoinsAmount = 99;
            _pickups.CoinsText.text = _pickups.CoinsAmount.ToString();
            
        }



    }
}
