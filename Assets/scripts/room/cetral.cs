using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cetral : MonoBehaviour
{
    private RoomsPrefabs _templates;
    private GameObject RoomCetrala;
    private SpaceBar _spaceBar;
    private PlayerHealth _playerHealth;

    

    public float _RandKonflikt;
    public float _CompareRandKonflikt = 69;
    private float _timer;
    private float _timerPusher;

    public bool spawnAll = false;

    private bool spawnItem = false;

    public List<GameObject> SpawnedMonsters;

    public List<GameObject> ItemsInRoom;

    public bool _opened = false;
    private bool _itemspawned = false;

    public bool RandomItemSpawn = false;

    public bool MiniMapSpawnRoom = false; 

    public int _doorTopActivated = 0;
    public int _doorBottomActivated = 0;
    public int _doorRightActivated = 0;
    public int _doorLeftActivated = 0;

    public int _SumaDoorAcicated = 0;
    public int _StaraSumaDoorAcicated = 0;

    public bool visited = false;

    private bool _topMinimap = false;
    private bool _bottomMinimap = false;
    private bool _rightMinimap = false;
    private bool _leftMinimap = false;

    private bool _updateNavMesh = false;

    void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();
        _spaceBar = GameObject.Find("Player").GetComponent<SpaceBar>();
        _playerHealth = GameObject.Find("Player/HitBox").GetComponent<PlayerHealth>();  
    }
    private void Update()
    {
        _timerPusher += Time.deltaTime;
        CheckRandomKonflikt();

        if (_timerPusher<5)
        {
            ListDoorsRoomPusher();  
        }

        if (spawnAll && SpawnedMonsters.Count == 0 && _timer > 1f && !_opened)
        {
            OpenDoors();
            _opened = true;
            _spaceBar.Charging();
        }
        if (spawnItem && SpawnedMonsters.Count == 0 && _timer > 1f && _playerHealth.TakenDamage== false && !_itemspawned)
        {
            RandomItemSpawn = true;
            _itemspawned = true;
        }

        if (spawnItem && _playerHealth.TakenDamage == true)
        {
            _itemspawned = true;
        }

        foreach (GameObject item in ItemsInRoom)
        {
            if (item == null)
            {
                ItemsInRoom.Remove(item);
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("central"))
        {
            RoomCetrala = collision.gameObject.transform.parent.parent.gameObject;
           
            
            bool wylosowana = false;
            if (!wylosowana)
            {
                _RandKonflikt = Random.value;
                wylosowana = true;
            }

            cetral codekonflikt = collision.GetComponent<cetral>();
            codekonflikt._CompareRandKonflikt = _RandKonflikt;

           
        }


        if (collision.CompareTag("Player"))
        {
            spawnAll = true;

            if (_updateNavMesh == false)
            {
                StartCoroutine(_templates.UpdateNavMesh(0.01f));
                _updateNavMesh = true;
            }

            spawnItem = true;
            
            _timer += Time.deltaTime;
            
            if(_timer < 0.5)
                _playerHealth.TakenDamage = false;
        }


        if (collision.CompareTag("FullHeart") || collision.CompareTag("HalfHeart"))
        {
            if (ItemsInRoom.Contains(collision.gameObject))
            {

            }
            else
            {
                ItemsInRoom.Add(collision.gameObject);

            }

            
        }


        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            visited = true;

            _spaceBar.MonsterParent = gameObject.transform.parent.parent.Find("Monstery").gameObject;
            if (_spaceBar.ItemActiveOnRoom == true)
            _spaceBar.ClearSpaceBarItems();


            if (gameObject.transform.parent.parent.Find("Grid/door_top").gameObject.activeSelf == true && _topMinimap == false)
            {
                _topMinimap = true;
                gameObject.transform.parent.parent.Find("MiniMap/TMiniMap").gameObject.SetActive(true);
            }
            if (gameObject.transform.parent.parent.Find("Grid/door_bottom").gameObject.activeSelf == true && _bottomMinimap == false)
            {
                _bottomMinimap = true;
                gameObject.transform.parent.parent.Find("MiniMap/BMiniMap").gameObject.SetActive(true);
            }
            if (gameObject.transform.parent.parent.Find("Grid/door_right").gameObject.activeSelf == true && _rightMinimap == false)
            {
                _rightMinimap = true;
                gameObject.transform.parent.parent.Find("MiniMap/RMiniMap").gameObject.SetActive(true);
            }
            if (gameObject.transform.parent.parent.Find("Grid/door_left").gameObject.activeSelf == true && _leftMinimap == false)
            {
                _leftMinimap = true;
                gameObject.transform.parent.parent.Find("MiniMap/LMiniMap").gameObject.SetActive(true);
            }

        }

        if (collision.CompareTag("MiniMapDetector"))
        {
            MiniMapSpawnRoom = true;
            Destroy(collision.gameObject);
        }

        }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            visited = false;
        }
    }

    private void CheckRandomKonflikt()
    {
        if (_RandKonflikt > _CompareRandKonflikt && _CompareRandKonflikt != 69)
        {
            _templates.Rooms.Remove(RoomCetrala);
            Destroy(RoomCetrala);

        }
    }

    private void OpenDoors()
    {
        if (gameObject.transform.parent.parent.Find("Grid/door_top").gameObject.activeSelf == true)
        {
            gameObject.transform.parent.parent.Find("Grid/door_top").gameObject.SetActive(false);
            gameObject.transform.parent.parent.Find("Grid/door_top_open").gameObject.SetActive(true);
        }

        if (gameObject.transform.parent.parent.Find("Grid/door_bottom").gameObject.activeSelf == true)
        {
            gameObject.transform.parent.parent.Find("Grid/door_bottom").gameObject.SetActive(false);
            gameObject.transform.parent.parent.Find("Grid/door_bottom_open").gameObject.SetActive(true);
        }

        if (gameObject.transform.parent.parent.Find("Grid/door_right").gameObject.activeSelf == true)
        {
            gameObject.transform.parent.parent.Find("Grid/door_right").gameObject.SetActive(false);
            gameObject.transform.parent.parent.Find("Grid/door_right_open").gameObject.SetActive(true);
        }

        if (gameObject.transform.parent.parent.Find("Grid/door_left").gameObject.activeSelf == true)
        {
            gameObject.transform.parent.parent.Find("Grid/door_left").gameObject.SetActive(false);
            gameObject.transform.parent.parent.Find("Grid/door_left_open").gameObject.SetActive(true);
        }
    }

    private void ListDoorsRoomPusher()
    {
        if (gameObject.transform.parent.parent.Find("Grid/door_top").gameObject.activeSelf == true)
        {
            _doorTopActivated= 1;
        }
        if (gameObject.transform.parent.parent.Find("Grid/door_bottom").gameObject.activeSelf == true)
        {
            _doorBottomActivated = 1;
        }
        if (gameObject.transform.parent.parent.Find("Grid/door_right").gameObject.activeSelf == true)
        {
            _doorRightActivated = 1;
        }
        if (gameObject.transform.parent.parent.Find("Grid/door_left").gameObject.activeSelf == true)
        {
            _doorLeftActivated = 1;
        }
        _SumaDoorAcicated = _doorTopActivated + _doorBottomActivated + _doorRightActivated + _doorLeftActivated;

        if (_SumaDoorAcicated == 1 && _StaraSumaDoorAcicated != _SumaDoorAcicated)
        {
            _templates.Rooms1.Add(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms2.Remove(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms3.Remove(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms4.Remove(gameObject.transform.parent.parent.gameObject);
        }
        else if (_SumaDoorAcicated == 2 && _StaraSumaDoorAcicated != _SumaDoorAcicated)
        {
            _templates.Rooms1.Remove(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms2.Add(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms3.Remove(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms4.Remove(gameObject.transform.parent.parent.gameObject);
        }
        else if (_SumaDoorAcicated == 3 && _StaraSumaDoorAcicated != _SumaDoorAcicated)
        {
            _templates.Rooms1.Remove(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms2.Remove(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms3.Add(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms4.Remove(gameObject.transform.parent.parent.gameObject);
        }
        else if (_SumaDoorAcicated == 4 && _StaraSumaDoorAcicated != _SumaDoorAcicated)
        {
            _templates.Rooms1.Remove(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms2.Remove(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms3.Remove(gameObject.transform.parent.parent.gameObject);
            _templates.Rooms4.Add(gameObject.transform.parent.parent.gameObject);
        }
        _StaraSumaDoorAcicated = _SumaDoorAcicated;
    }


}
