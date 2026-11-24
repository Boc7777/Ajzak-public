using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    private RoomsPrefabs _templates;
    private PlayerStats _playerStats;

    private GameObject _player;
    private GameObject _startRoom;

    private GameObject _camera;
    private GameObject _miniMapCamera;

    private GameObject _room;
    private GameObject _items;

    private GameObject _item;

    private GameObject _loadingScreen;

    private void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _startRoom = GameObject.FindGameObjectWithTag("StartRoom");
        _items = GameObject.FindGameObjectWithTag("ItemsInRoom");
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        _loadingScreen = GameObject.FindGameObjectWithTag("loadingScreen");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("HitBox")) 
        {
            if (_player.GetComponent<PlayerStats>().BloodLustActive)
            {
                _player.GetComponent<PlayerStats>().ResetBloodLust();
               
            }
            for (int i = 0; i < _items.gameObject.transform.childCount; i++)
            {
                _item = _items.transform.GetChild(i).gameObject;
                Destroy(_item);
            }

            for (int i = 0; i < _templates.Rooms.Count; i++)
            {
                _room = _templates.Rooms[i];
                Destroy(_room);
            }
            Destroy(_startRoom);

            _player.gameObject.transform.position = new Vector2(0, 0);

            _camera = GameObject.FindGameObjectWithTag("camera");
            _camera.transform.position = new Vector3(0, 0, -10);
            _miniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera");
            _miniMapCamera.transform.position = new Vector3(0, 1000, -10);

            Instantiate(_templates.StartRoom, new Vector2(0,0) , Quaternion.identity, gameObject.transform.parent.parent.parent.parent);

            _templates.Piwnica = true;

            _templates.Rooms.Clear();
            _templates.Rooms1.Clear();
            _templates.Rooms2.Clear();
            _templates.Rooms3.Clear();
            _templates.Rooms4.Clear();

            _templates.minusChanceDevilRoom = false;
            _templates.minusChanceDevilRoomBoosRoom = false;

            _templates._specialRoomsToSpawn = 3;
            _templates._spawnedBossRoom = false;
            _templates._spawnedTreasureRoom = false;
            _templates._spawnedShopRoom = false;

            _templates.Floor++;

            _playerStats.DevilRoomPercentages = 100;
            _playerStats.DevilRoomChance = "+100";
            _playerStats.NewStats();

            if(_templates.ChanceAngelRoom == true && _templates.PermanentZeroChanceAngelRoom == false)
            {
                if ( _templates.AngelRoomBooster == 0)
                {
                    _playerStats.AngelRoomPercentages = 33.3f;
                    _playerStats.AngelRoomChance = "+33.3";
                    _playerStats.NewStats();
                    _templates.AngelRoomBooster += 1;
                    Debug.Log("33");
                }
                else if(_templates.AngelRoomBooster == 1)
                {
                    _playerStats.AngelRoomPercentages = 66.6f;
                    _playerStats.AngelRoomChance = "+33.3";
                    _playerStats.NewStats();
                    _templates.AngelRoomBooster += 1;
                    Debug.Log("66");
                }
                else if (_templates.AngelRoomBooster == 2)
                {
                    _playerStats.AngelRoomPercentages = 66.6f;
                    _playerStats.AngelRoomChance = "+0";
                    _playerStats.NewStats();
                    _templates.AngelRoomBooster += 1;
                }
                _templates.ChanceAngelRoomChosen = false;
            }
            if(_templates.PermanentZeroChanceAngelRoom == true)
            {
                _playerStats.AngelRoomChance = "-" + _playerStats.AngelRoomPercentages;
                _playerStats.AngelRoomPercentages = 0f;
                _playerStats.NewStats();
                _templates.AngelRoomBooster = 40;
            }
            _loadingScreen.GetComponent<loadingScreen>().ColorReset();
        }
    }
}
