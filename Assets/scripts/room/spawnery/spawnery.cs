using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnery : MonoBehaviour
{
    public string _direction;

    private int _rand;
    private int _NumberOfDoors = 0;

    private Transform _parent;
    private RoomsPrefabs _templates;

    public bool _spawned = false;
    private float _spawnDelay = 0.1f;

    public bool _spawnerTopActivated = false ;
    public bool _spawnerBottomActivated = false;
    public bool _spawnerRightActivated = false;
    public bool _spawnerLeftActivated = false;

    private GameObject _roomOne;
        
    void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();    
        _parent = GameObject.Find("Rooms").transform;
    }
    private void Update()
    {
        Invoke("Spawn", _spawnDelay);
       
        //_timer += Time.deltaTime;
        //Deleter();
    }
    void Spawn()
    {
        if (_spawned == false && _templates.Rooms.Count < _templates.MinRooms) 
        {
            _roomOne = Instantiate(_templates.Universal, gameObject.transform.position, Quaternion.identity , _parent);
            
            RandomActivationSpawners(1);

            if (_spawnerTopActivated == false)
            {
                _roomOne.transform.Find("Spawnery/DestroyerTOP").gameObject.SetActive(true);
            }
            if (_spawnerBottomActivated == false)
            {
                _roomOne.transform.Find("Spawnery/DestroyerBOTTOM").gameObject.SetActive(true);
            }
            if (_spawnerRightActivated == false)
            {
                _roomOne.transform.Find("Spawnery/DestroyerRIGHT").gameObject.SetActive(true);
            }
            if (_spawnerLeftActivated == false)
            {
                _roomOne.transform.Find("Spawnery/DestroyerLEFT").gameObject.SetActive(true);
            }

            _spawned = true;
            Destroy(gameObject);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("central")) //jezeli spwaner dotknie cetrala wypierdala sie 
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("destroyer")) 
        {
            Destroy(gameObject);
        }
    }
    private void RandomActivationSpawners(int MaxDoors)
    {    
        _NumberOfDoors = Random.Range(1, MaxDoors+1);

        for (int i = 0; i < _NumberOfDoors; i++)
        {
            _rand = Random.Range(1, 100);

            if (_rand < 25)
            {
               if(_spawnerTopActivated == true)
               {
                    i--;
               }
               else
               {
                    _spawnerTopActivated = true;
                    _roomOne.transform.Find("Spawnery").Find("T").gameObject.SetActive(true);
               }

            }
            else if (_rand < 50)
            {
                if (_spawnerBottomActivated == true)
                {
                    i--;
                }
                else
                {
                    _spawnerBottomActivated = true;
                    _roomOne.transform.Find("Spawnery").Find("B").gameObject.SetActive(true);
                }

            }
            else if (_rand < 75)
            {
                if (_spawnerRightActivated == true)
                {
                    i--;
                }
                else
                {
                    _spawnerRightActivated = true;
                    _roomOne.transform.Find("Spawnery").Find("R").gameObject.SetActive(true);
                }

            }
            else if (_rand <= 100) 
            {
                if (_spawnerLeftActivated == true)
                {
                    i--;
                }
                else
                {
                    _spawnerLeftActivated = true;
                    _roomOne.transform.Find("Spawnery").Find("L").gameObject.SetActive(true);
                }
            }
        }
    }
}
