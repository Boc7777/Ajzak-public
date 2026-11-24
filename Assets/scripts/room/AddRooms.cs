using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRooms : MonoBehaviour
{
    private RoomsPrefabs _templates;
    private cetral _cetral;

    private PlayerStats _playerStats;

    private int _rand;

    public bool _spawned = false;
    private bool _spawnedTrapDoor = false;
    private bool _permSpawned = false;

    public bool _treasureRoom = false;
    public bool _bossRoom = false;
    public bool _shopRoom = false;
    public bool _curseRoom = false;

    public List<GameObject> ListaPermDestroyerow;
    private GameObject _destroyerOne;
    private GameObject _devilRoomOne;
    private GameObject _angelRoomOne;

    public bool DevilRoomLosowanie;

    void Start()
    {
        
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();
        _templates.Rooms.Add(this.gameObject);
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        if (gameObject.transform.Find("Spawnery").Find("central") != null)
            _cetral = gameObject.transform.Find("Spawnery").Find("central").gameObject.GetComponent<cetral>();

    }
    private void Update()
    {

        if (_bossRoom == true && _spawned == false && _cetral.spawnAll)
        {
            _templates.minusChanceDevilRoom = false; //po wybudowaniu boosrooma wlacza sie mozliwosc zmniejszenia procentu na devilrooma
            _templates.minusChanceDevilRoomBoosRoom = true; //bedac w bossroomie gdy dostanie demage szansa = 1%
            Instantiate(_templates.BossRoom, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            PermanentneDestroyery();
            _spawned = true;
        }

        else if (_treasureRoom == true && _spawned == false)
        {
            Instantiate(_templates.TresureRoom, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            PermanentneDestroyery();
            _spawned = true;

        }

        else if (_shopRoom == true && _spawned == false)
        {
            Instantiate(_templates.ShopRoom, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            PermanentneDestroyery();
            _spawned = true;
        }

        else if (_curseRoom == true && _spawned == false && _cetral.spawnAll)
        {

            PlayerHealth _health = GameObject.Find("Player/HitBox").GetComponent<PlayerHealth>();
            _health.TakeDamage(2);

            _rand = Random.Range(0, _templates.ConfigurationCurseRoom.Count);
            Instantiate(_templates.ConfigurationCurseRoom[_rand], gameObject.transform.position, Quaternion.identity, gameObject.transform);
            PermanentneDestroyery();
            _spawned = true;
        }

        else if (_spawned == false && _cetral.spawnAll)
        {
            _rand = Random.Range(0, _templates.ConfigurationRooms.Count);
            Instantiate(_templates.ConfigurationRooms[_rand], transform.position, Quaternion.identity, gameObject.transform);
            _spawned = true;
        }

        if (!_spawnedTrapDoor)
        {
            Invoke("TrapDoorSpawner", 1f);
        }

        if (_bossRoom == true && _permSpawned == false)
        {
            PermanentneDestroyery();
            _permSpawned = true;

        }
    }

    private void TrapDoorSpawner()
    {
        if (_bossRoom == true && _cetral.SpawnedMonsters.Count == 0 && !_spawnedTrapDoor && _spawned)
        {
            gameObject.transform.Find("BossRoom(Clone)/TrapDoorSpawner").gameObject.SetActive(true);
            _spawnedTrapDoor = true;
            DevilRoomLosowanie = true;

            StartCoroutine(DevilRoomCreator(0.1f));

            StartCoroutine(AngelRoomCreator(0.2f));


        }
    }

    private void PermanentneDestroyery()
    {
        if (gameObject.transform.Find("Spawnery").Find("PermDestroyerTOP") != null)
            gameObject.transform.Find("Spawnery").Find("PermDestroyerTOP").gameObject.SetActive(true);

        if (gameObject.transform.Find("Spawnery").Find("PermDestroyerBOTTOM") != null)
            gameObject.transform.Find("Spawnery").Find("PermDestroyerBOTTOM").gameObject.SetActive(true);

        if (gameObject.transform.Find("Spawnery").Find("PermDestroyerRIGHT") != null)
            gameObject.transform.Find("Spawnery").Find("PermDestroyerRIGHT").gameObject.SetActive(true);

        if (gameObject.transform.Find("Spawnery").Find("PermDestroyerLEFT") != null)
            gameObject.transform.Find("Spawnery").Find("PermDestroyerLEFT").gameObject.SetActive(true);
    }

    IEnumerator OpenDoors(float delay)
    {

        yield return new WaitForSeconds(delay);

        if (gameObject.transform.Find("Grid/door_top").gameObject.activeSelf == true)
        {
            gameObject.transform.Find("Grid/door_top").gameObject.SetActive(false);
            gameObject.transform.Find("Grid/door_top_open").gameObject.SetActive(true);
        }

        if (gameObject.transform.Find("Grid/door_bottom").gameObject.activeSelf == true)
        {
            gameObject.transform.Find("Grid/door_bottom").gameObject.SetActive(false);
            gameObject.transform.Find("Grid/door_bottom_open").gameObject.SetActive(true);
        }

        if (gameObject.transform.Find("Grid/door_right").gameObject.activeSelf == true)
        {
            gameObject.transform.Find("Grid/door_right").gameObject.SetActive(false);
            gameObject.transform.Find("Grid/door_right_open").gameObject.SetActive(true);
        }

        if (gameObject.transform.Find("Grid/door_left").gameObject.activeSelf == true)
        {
            gameObject.transform.Find("Grid/door_left").gameObject.SetActive(false);
            gameObject.transform.Find("Grid/door_left_open").gameObject.SetActive(true);
        }

    }

    IEnumerator DevilRoomCreator(float delay)
    {
        yield return new WaitForSeconds(delay);

        int DevilRoomChance = Random.Range(1, 100);

        if (DevilRoomChance <= _playerStats.DevilRoomPercentages)
        {

            if (gameObject.transform.Find("Spawnery").Find("PermDestroyerTOP") != null)
            {
                ListaPermDestroyerow.Add(gameObject.transform.Find("Spawnery").Find("PermDestroyerTOP").gameObject);
            }
            if (gameObject.transform.Find("Spawnery").Find("PermDestroyerBOTTOM") != null)
            {
                ListaPermDestroyerow.Add(gameObject.transform.Find("Spawnery").Find("PermDestroyerBOTTOM").gameObject);
            }
            if (gameObject.transform.Find("Spawnery").Find("PermDestroyerRIGHT") != null)
            {
                ListaPermDestroyerow.Add(gameObject.transform.Find("Spawnery").Find("PermDestroyerRIGHT").gameObject);
            }
            if (gameObject.transform.Find("Spawnery").Find("PermDestroyerLEFT") != null)
            {
                ListaPermDestroyerow.Add(gameObject.transform.Find("Spawnery").Find("PermDestroyerLEFT").gameObject);
            }

            int _randomDestroyer = Random.Range(0, ListaPermDestroyerow.Count);

            _destroyerOne = ListaPermDestroyerow[_randomDestroyer];

            _devilRoomOne = Instantiate(_templates.DevilRoom, _destroyerOne.transform.position, Quaternion.identity, transform);

            _devilRoomOne.GetComponent<AddRooms>()._spawned = true;

            Destroy(_destroyerOne);

            ListaPermDestroyerow.Clear();

            StartCoroutine(OpenDoors(0.5f));
        }
    }

    IEnumerator AngelRoomCreator(float delay)
    {
        yield return new WaitForSeconds(delay);

        int AngelRoomChance = Random.Range(1, 100);
        if (AngelRoomChance <= _playerStats.AngelRoomPercentages)
        {

            if (gameObject.transform.Find("Spawnery").Find("PermDestroyerTOP") != null)
            {
                ListaPermDestroyerow.Add(gameObject.transform.Find("Spawnery").Find("PermDestroyerTOP").gameObject);
            }
            if (gameObject.transform.Find("Spawnery").Find("PermDestroyerBOTTOM") != null)
            {
                ListaPermDestroyerow.Add(gameObject.transform.Find("Spawnery").Find("PermDestroyerBOTTOM").gameObject);
            }
            if (gameObject.transform.Find("Spawnery").Find("PermDestroyerRIGHT") != null)
            {
                ListaPermDestroyerow.Add(gameObject.transform.Find("Spawnery").Find("PermDestroyerRIGHT").gameObject);
            }
            if (gameObject.transform.Find("Spawnery").Find("PermDestroyerLEFT") != null)
            {
                ListaPermDestroyerow.Add(gameObject.transform.Find("Spawnery").Find("PermDestroyerLEFT").gameObject);
            }

            int _randomDestroyer = Random.Range(0, ListaPermDestroyerow.Count);

            _destroyerOne = ListaPermDestroyerow[_randomDestroyer];

            _angelRoomOne = Instantiate(_templates.AngelRoom, _destroyerOne.transform.position, Quaternion.identity, transform);

            _angelRoomOne.GetComponent<AddRooms>()._spawned = true;

            Destroy(_destroyerOne);

            ListaPermDestroyerow.Clear();

            StartCoroutine(OpenDoors(0.5f));
        }
    }

   
}

