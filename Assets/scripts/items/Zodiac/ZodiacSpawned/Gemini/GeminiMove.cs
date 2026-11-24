using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeminiMove : MonoBehaviour
{
    private Transform _playerCord;

    private bool _detectedMonster;
    private float _movespeed = 3.8f;

    private GameObject _nearMonster;

    private void OnEnable()
    {
        _playerCord = GameObject.Find("Player").transform;

    }

    void Update()
    {

        if (_detectedMonster && Vector2.Distance(transform.position, _playerCord.position) < 5f)
        {

            GoToMonster();

        }
        else if(Vector2.Distance(transform.position, _playerCord.position) > 0.65f)
        {
            GoToPlayer();

        }
    }

    private void GoToPlayer()
    {

        transform.position = Vector2.MoveTowards(transform.position, _playerCord.position, _movespeed * Time.deltaTime);

    }

    private void GoToMonster()
    {
        if (_nearMonster)
        {
            transform.position = Vector2.MoveTowards(transform.position, _nearMonster.transform.position, _movespeed * Time.deltaTime);
        }
        else
        {
            _detectedMonster = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("monster"))
        {
            _nearMonster = collision.gameObject;
        }
    }
}
