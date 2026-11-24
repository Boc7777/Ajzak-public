using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MoveFly : MonoBehaviour
{
    private Transform _playerCord;

    [SerializeField]    private float _timer;
    private float x, y; 
    private Vector3 _positionNearestMonster;
    private Vector3 _flyPosition;
    private Rigidbody2D rb;

    private bool _detectedMonster;
    private float _movespeed =3.15f;

    private GameObject _nearMonster;
    private Vector3 _direction;

    private player_movement _playerMovement;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerMovement = GameObject.Find("Player").GetComponent<player_movement>();

        EventManager.TeleportFamiliar += TpFlyIEnumerator;
    }
    private void OnEnable()
    {
        _playerCord = GameObject.Find("Player").transform;

        _flyPosition = _playerCord.position;


        Vector2 _direction2 = transform.position - _flyPosition;
        float angle = Mathf.Atan2(_direction2.y, _direction2.x) * Mathf.Rad2Deg;
        if (angle < 0 && angle >= -90)
        {
            angle = 90 + angle + 270;
        }
        else if (angle < 0)
        {
            angle = 180 + angle + 180;

        }
        _timer = angle * Mathf.Deg2Rad;
    }
    public void TpFlyIEnumerator()
    {
        StartCoroutine(TpFlYToPlayer());
    }
    
    private IEnumerator TpFlYToPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = _flyPosition;
    }

    void Update()
    {
        x = Mathf.Cos(_timer) * 0.7f;
        y = Mathf.Sin(_timer) * 0.7f;
        _timer += Time.deltaTime *1.7f ;

        if (!_detectedMonster)
        {


            GoToPlayer();


            //if (Vector2.Distance(transform.position, _flyPosition) <= 0.2f && _playerMovement.DynamicMove < 4f)
            //{
            //    MoveAroundPlayer();
            //}
            //else
            //{
            //   GoToPlayer();
            //}
            _flyPosition = _playerCord.transform.position + new Vector3(x, y, 0);

        }
        else if (_detectedMonster)
        {
            GoToEnemy();
        }
    }


    private void MoveAroundPlayer()
    {


        transform.position = _flyPosition;
    }
    
    private void GoToPlayer()
    {

        _direction = _flyPosition - gameObject.transform.position;
        _direction.Normalize();

        transform.position = Vector2.MoveTowards(transform.position, _flyPosition, _movespeed * Time.deltaTime);

        //transform.position += _direction * _movespeed;

        //Vector2 _direction2 = transform.position - _positionPlayer;
        //float angle = Mathf.Atan2(_direction2.y, _direction2.x) * Mathf.Rad2Deg;
        //if (angle < 0 && angle >= -90)
        //{
        //    angle = 90 + angle + 270;
        //}
        //else if (angle < 0)
        //{
        //    angle = 180 + angle + 180;

        //}

    }
    private void GoToEnemy()
    {
        if (_nearMonster)
        {
            _positionNearestMonster = GameObject.Find(_nearMonster.gameObject.name).transform.position;
            transform.position = Vector2.MoveTowards(transform.position, _positionNearestMonster, _movespeed * Time.deltaTime);

        }
        else
        {
            _detectedMonster = false;

        }


    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.transform.CompareTag("monster"))
        {
            _detectedMonster = true;

            _nearMonster = collision.gameObject;
        }
        
        
    }

    private void OnDestroy()
    {
        EventManager.TeleportFamiliar -= TpFlyIEnumerator;

    }


}
