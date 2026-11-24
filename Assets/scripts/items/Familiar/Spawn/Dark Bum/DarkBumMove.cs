using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBumMove : MonoBehaviour
{
    private Transform _playerCord;

    private bool _detectedHeart;
    private float _movespeed = 3.15f;
    public bool Stay;


    private GameObject _nearHeart;

    void Start()
    {

    }
    private void OnEnable()
    {
        _playerCord = GameObject.Find("Player").transform;


    }


    void Update()
    {
        
        if (!_detectedHeart && Vector2.Distance(transform.position, _playerCord.position) > 2f)
        {

            GoToPlayer();
            Stay = false;

        }
        else if (_detectedHeart)
        {
            GoToHeart();
            Stay = false;

        }
        else
        {
            Stay = true;
        }
    }

    private void GoToPlayer()
    {

        transform.position = Vector2.MoveTowards(transform.position, _playerCord.position, _movespeed * Time.deltaTime);

    }

    private void GoToHeart()
    {
        if (_nearHeart)
        {
            transform.position = Vector2.MoveTowards(transform.position, _nearHeart.transform.position, _movespeed * Time.deltaTime);
        }
        else
        {
            _detectedHeart = false;
        }
    }

   

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.transform.name.Contains("RedHeart"))
        {
            List<GameObject> lista = GameObject.Find("Player").GetComponent<player_movement>().CurrentlyCentral.GetComponent<cetral>().ItemsInRoom;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].GetInstanceID() == collision.gameObject.GetInstanceID())
                {
                    Debug.Log(collision.gameObject.transform.GetInstanceID());
                    _detectedHeart = true;
                    _nearHeart = collision.gameObject;
                }
            }

        }


    }

   
}
