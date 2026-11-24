using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    private PlayerStats _playerStats;
    void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("monster"))
        {
            collision.gameObject.GetComponent<MonsterTakeDmg>().MonsterHealth -= _playerStats.Dmg * 2;

            Destroy(transform.parent.parent.gameObject);
        }
    }
}
