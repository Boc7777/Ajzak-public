using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _notVisited;
    [SerializeField] private Sprite _visited;
    [SerializeField] private Sprite _Actual;
    [SerializeField] private GameObject _czacha;
    [SerializeField] private GameObject _korona;
    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _curse;
    private GameObject _room;
    private float _mnoznik;

    private cetral _cetral;
    private AddRooms _addRooms;

    private bool _created = false;

    private bool _odpalony = false;

    private bool _spawnikona = false; 

    private void Start()
    {
        _cetral = gameObject.transform.Find("Spawnery/central").GetComponent<cetral>();
        _addRooms = gameObject.transform.GetComponent<AddRooms>();
    }
    private void Update()
    {
        if (_cetral.MiniMapSpawnRoom)
        {
            if (_created == false)
            {
                _room = Instantiate(_notVisited, new Vector2(transform.position.x, transform.position.y + 1000), Quaternion.identity, transform);

                _mnoznik = _room.transform.position.x / 18;

                if (_room.transform.position.x > 0)
                {
                    _room.gameObject.transform.position = new Vector2(_room.gameObject.transform.position.x - 5.5f * _mnoznik, transform.position.y + 1000);
                }
                else if (_room.transform.position.x < 0)
                {
                    _room.gameObject.transform.position = new Vector2(_room.gameObject.transform.position.x - 5.5f * _mnoznik, transform.position.y + 1000);
                }

                _created = true;
                
            }
            


            if (_cetral.visited == true && _created == true)
            {
                _room.GetComponent<SpriteRenderer>().sprite = _Actual;
                _room.transform.localScale = new Vector2(19, 19);
                _odpalony = true;
            }

            if (_cetral.visited == false && _odpalony == true && _created == true)
            {
                _room.GetComponent<SpriteRenderer>().sprite = _visited;
                _room.transform.localScale = new Vector2(25, 25);
            }

            if (_addRooms._bossRoom && _spawnikona == false)
            {
                Instantiate(_czacha, _room.transform.position, Quaternion.identity, _room.transform);
                _spawnikona = true;
            }
            else if (_addRooms._treasureRoom && _spawnikona == false )
            {
                Instantiate(_korona, _room.transform.position, Quaternion.identity, _room.transform);
                _spawnikona = true;
            }
            else if (_addRooms._shopRoom && _spawnikona == false)
            {
                Instantiate(_coin, _room.transform.position, Quaternion.identity, _room.transform);
                _spawnikona = true;
            }
            else if (_addRooms._curseRoom && _spawnikona == false)
            {
                Instantiate(_curse, _room.transform.position, Quaternion.identity, _room.transform);
                _spawnikona = true;
            }


        }
    }

}
