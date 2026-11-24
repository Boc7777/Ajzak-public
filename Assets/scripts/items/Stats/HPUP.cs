using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUP : MonoBehaviour
{
    private PlayerHealth _playerhealth;


    private void Start()
    {        
        _playerhealth = GameObject.FindGameObjectWithTag("HitBox").gameObject.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _playerhealth.HPUP = true;
        }
    }


}
