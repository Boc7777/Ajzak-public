using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravity : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    private PlayerStats _playerStats;

    void Start()
    {
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
     
            _playerStats.Tears -= 0.1f;
            _playerStats.TearsChange = "- 0.1";
            _playerStats.NewStats();
            _itemsPlayer.AntiGravityActive = true;
            GameObject.Find("Arrows").AddComponent<AntiGravityManager>();
            GameObject obj = new GameObject("AntyGravityRun");

            obj.transform.SetParent(GameObject.Find("Arrows").transform);
        }



    }
}
