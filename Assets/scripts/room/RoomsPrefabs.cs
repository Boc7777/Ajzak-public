using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomsPrefabs : MonoBehaviour
{
    [SerializeField] private NavMeshSurface2d _NavMeshSurface;

    private audioManager _audioManager;

    public float MinRooms = 20;
    public float Floor = 1;

    public GameObject StartRoom;
    public GameObject Universal;
    public GameObject BossRoom;
    public GameObject TresureRoom;
    public GameObject ShopRoom;
    public GameObject DevilRoom;
    public GameObject AngelRoom;

    public bool DevilRoomSpawned = false;
    public bool AngelRoomSpawned = false;

    [SerializeField] private GameObject _startRoom;

    [SerializeField] private GameObject _spawnerTop;
    [SerializeField] private GameObject _spawnerBottom;
    [SerializeField] private GameObject _spawnerRight;
    [SerializeField] private GameObject _spawnerLeft;

    [SerializeField] private GameObject _chosenBossRoom;

    public List<GameObject> Rooms;
    public List<GameObject> Rooms1;
    public List<GameObject> Rooms2;
    public List<GameObject> Rooms3;
    public List<GameObject> Rooms4;

    public List<GameObject> ConfigurationRooms;

    public List<GameObject> ConfigurationDevilRoom;

    public List<GameObject> ConfigurationCurseRoom;

    public List<GameObject> Monsters;

    public List<GameObject> BossList;

    private int RandomRoomNumber;
    private GameObject RandomRoom;

    private float _timer = 0;
    private float _timer2 = 0;

    private GameObject _RandomRoomTreasure;
    private GameObject _RandomBoosRoom;

    private AddRooms _AddRooms;

    public bool _spawnedBossRoom = false;
    public bool _spawnedShopRoom = false;
    public bool _spawnedTreasureRoom = false;
    public bool _spawdedCurseRoom = false;

    //public bool _bossRoomCoroutine = false;
    //public bool _ShopRoomCoroutine = false;
    //public bool _treasureRoomCoroutine = false;
    //public bool _CurseRoomCorutine = false;

    private int _biggestDistanceIncex;
    private float _distance;

    public bool Piwnica = false;

    public List<GameObject> Destroyers;
    private int _randomDestroyer;
    private GameObject _destroyerOne;
    private Vector3 _destroyerOnePosition;
    private string _destroyerOneName;

    public float _specialRoomsToSpawn = 3;

    public bool minusChanceDevilRoom = false;
    public bool minusChanceDevilRoomBoosRoom = false;


    public bool ChanceAngelRoom = false;
    public bool PermanentZeroChanceAngelRoom = false;

    public bool DevilRoomExist = false;

    public bool ChanceAngelRoomChosen = false;

    public int AngelRoomBooster = 0;

    private bool dorabiacCzyNie = false;
    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();

        StartCoroutine(UpdateNavMesh(1f));
        int rnd = Random.Range(0, 1);
        if (rnd == 0)
        {
            _specialRoomsToSpawn++;
        }

        _audioManager.PlaySound("muzadogiery");
    }
    private void Update()
    {
        
        _timer += Time.deltaTime;

        _timer2 += Time.deltaTime;

        if (Piwnica)
        {
            Piwnica = false;
            StartCoroutine(UpdateNavMesh(1f));
            _timer = -1;
        }

        if ((Rooms.Count < MinRooms) && (_timer > 1f) && !Piwnica)
        {
            RoomBooster();
        }

        if (Rooms.Count >= MinRooms)
        {
            if (Rooms1.Count >= _specialRoomsToSpawn && _timer > 2f)
            {

                if (!_spawnedBossRoom && _specialRoomsToSpawn != 0 )
                {
                    BossChooseRoom();
                    
                }

                if (_spawnedBossRoom && !_spawnedTreasureRoom && _specialRoomsToSpawn != 0)
                {
                    RandomTreasureRoom();
                }

                if (_spawnedTreasureRoom && !_spawnedShopRoom && _specialRoomsToSpawn != 0)
                {
                    RandomShopRoom();
                }

                if (_spawnedShopRoom && !_spawdedCurseRoom && _specialRoomsToSpawn != 0)
                {
                    RandomCurseRoom();
                }

            }
            else if(Rooms1.Count < _specialRoomsToSpawn && _specialRoomsToSpawn != 0 && _timer2 >0.5f )
            {
                _timer2 = 0;
                MinRooms++;
            }
        }

        if(DevilRoomExist == true && ChanceAngelRoomChosen == false)
        {
            ChanceAngelRoom = true;
            ChanceAngelRoomChosen = true;
        }
    }

    public void RoomBooster()
    {
        if (Rooms.Count != 0)
        {
            RandomRoomNumber = Random.Range(0, Rooms.Count - 1);

            RandomRoom = Rooms[RandomRoomNumber];

            if (RandomRoom.transform.Find("Spawnery").Find("DestroyerTOP") != null)
            {
                Destroyers.Add(RandomRoom.transform.Find("Spawnery").Find("DestroyerTOP").gameObject);
            }
            if (RandomRoom.transform.Find("Spawnery").Find("DestroyerBOTTOM") != null)
            {
                Destroyers.Add(RandomRoom.transform.Find("Spawnery").Find("DestroyerBOTTOM").gameObject);
            }
            if (RandomRoom.transform.Find("Spawnery").Find("DestroyerRIGHT") != null)
            {
                Destroyers.Add(RandomRoom.transform.Find("Spawnery").Find("DestroyerRIGHT").gameObject);
            }
            if (RandomRoom.transform.Find("Spawnery").Find("DestroyerLEFT") != null)
            {
                Destroyers.Add(RandomRoom.transform.Find("Spawnery").Find("DestroyerLEFT").gameObject);
            }

            if (Destroyers.Count > 0)
            {
                _randomDestroyer = Random.Range(0, Destroyers.Count);

                _destroyerOne = Destroyers[_randomDestroyer];


                _destroyerOnePosition = _destroyerOne.transform.position;

                _destroyerOneName = _destroyerOne.name;

                Destroy(_destroyerOne);

                if (_destroyerOneName == "DestroyerTOP")
                {
                    Instantiate(_spawnerTop, _destroyerOnePosition, Quaternion.identity, RandomRoom.transform);
                }
                else if (_destroyerOneName == "DestroyerBOTTOM")
                {
                    Instantiate(_spawnerBottom, _destroyerOnePosition, Quaternion.identity, RandomRoom.transform);
                }
                else if (_destroyerOneName == "DestroyerRIGHT")
                {
                    Instantiate(_spawnerRight, _destroyerOnePosition, Quaternion.identity, RandomRoom.transform);
                }
                else if (_destroyerOneName == "DestroyerLEFT")
                {
                    Instantiate(_spawnerLeft, _destroyerOnePosition, Quaternion.identity, RandomRoom.transform);
                }

                Destroyers.Clear();

            }
        }
    }

    private void BossChooseRoom()
    {
            float _biggestDistance = 0;
            for (int i = 0; i < Rooms1.Count; i++)
            {
                _distance = Vector2.Distance(Rooms1[i].transform.position, _startRoom.gameObject.transform.position);   
                
                if (_distance > _biggestDistance)
                {

                 _biggestDistance = _distance;
                 _biggestDistanceIncex = i; 

                } 
            }
            _RandomBoosRoom = Rooms1[_biggestDistanceIncex];
            _chosenBossRoom = _RandomBoosRoom;

            _AddRooms = _RandomBoosRoom.GetComponent<AddRooms>();
            _AddRooms._bossRoom = true;

            Rooms1.Remove(_RandomBoosRoom);

            _specialRoomsToSpawn--;
            _spawnedBossRoom = true;
    }

    private void RandomTreasureRoom()
    {
            int _rand = Random.Range(0, Rooms1.Count);
            _RandomRoomTreasure = Rooms1[_rand];
            _AddRooms = _RandomRoomTreasure.GetComponent<AddRooms>();
            _AddRooms._treasureRoom = true;
            Rooms1.Remove(_RandomRoomTreasure);

            _specialRoomsToSpawn--;
            _spawnedTreasureRoom = true;   
    }

    private void RandomShopRoom()
    {
            int _rand = Random.Range(0, Rooms1.Count);
            _RandomRoomTreasure = Rooms1[_rand];
            _AddRooms = _RandomRoomTreasure.GetComponent<AddRooms>();
            _AddRooms._shopRoom = true;
            Rooms1.Remove(_RandomRoomTreasure);

            _specialRoomsToSpawn--;
            _spawnedShopRoom = true;  
    }

    private void RandomCurseRoom()
    {
        int _rand = Random.Range(0, Rooms1.Count);
        _RandomRoomTreasure = Rooms1[_rand];
        _AddRooms = _RandomRoomTreasure.GetComponent<AddRooms>();
        _AddRooms._curseRoom = true;
        Rooms1.Remove(_RandomRoomTreasure);

        _specialRoomsToSpawn--;
        _spawdedCurseRoom = true;
    }

    public IEnumerator UpdateNavMesh(float delay)
    {
        yield return new WaitForSeconds(delay);
        _NavMeshSurface.BuildNavMesh();
    }
}
