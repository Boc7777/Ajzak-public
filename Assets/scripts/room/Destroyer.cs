using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private int _rand;
    private int _NumberOfDoors = 0;

    public bool _spawnerTopActivated = false;
    public bool _spawnerBottomActivated = false;
    public bool _spawnerRightActivated = false;
    public bool _spawnerLeftActivated = false;

    private bool _topMinimap = false;
    private bool _bottomMinimap = false;
    private bool _rightMinimap = false;
    private bool _leftMinimap = false;


    [SerializeField] private GameObject _notVisited;
    [SerializeField] private Sprite _visited;
    [SerializeField] private Sprite _Actual;

    private GameObject _room;

    private bool _odpalony = false;
    private bool visited = false;

    private SpaceBar _spaceBar;


    private void Start()
    {
        _room = Instantiate(_notVisited, new Vector2(0, 1000), Quaternion.identity, transform.parent.parent);
        RandomActivationSpawners(4);

        if (_spawnerTopActivated == false)
        {
            gameObject.transform.parent.Find("DestroyerTOP").gameObject.SetActive(true);
        }
        if (_spawnerBottomActivated == false)
        {
            gameObject.transform.parent.Find("DestroyerBOTTOM").gameObject.SetActive(true);
        }
        if (_spawnerRightActivated == false)
        {
            gameObject.transform.parent.Find("DestroyerRIGHT").gameObject.SetActive(true);
        }
        if (_spawnerLeftActivated == false)
        {
            gameObject.transform.parent.Find("DestroyerLEFT").gameObject.SetActive(true);
        }

        _spaceBar = GameObject.Find("Player").GetComponent<SpaceBar>();

    }

    private void Update()
    {


        if (visited == true)
        {
            _room.GetComponent<SpriteRenderer>().sprite = _Actual;
            _room.transform.localScale = new Vector2(19, 19);
            _odpalony = true;
        }

        if (visited == false && _odpalony == true)
        {
            _room.GetComponent<SpriteRenderer>().sprite = _visited;
            _room.transform.localScale = new Vector2(25, 25);
        }


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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_spaceBar.ItemActiveOnRoom == true)
            {
                _spaceBar.ClearSpaceBarItems();
            }

            visited = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            visited = false;
        }
    }
    private void RandomActivationSpawners(int MaxDoors)
    {

        _NumberOfDoors = Random.Range(2, MaxDoors);



        for (int i = 0; i < _NumberOfDoors; i++)
        {
            _rand = Random.Range(1, 100);

            if (_rand < 25)
            {
                if (_spawnerTopActivated == true)
                {
                    i--;
                }
                else
                {
                    _spawnerTopActivated = true;
                    gameObject.transform.parent.parent.Find("Spawnery").Find("T").gameObject.SetActive(true);

                    gameObject.transform.parent.parent.Find("Grid").Find("door_top").gameObject.SetActive(true);
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
                    gameObject.transform.parent.parent.Find("Spawnery").Find("B").gameObject.SetActive(true);

                    gameObject.transform.parent.parent.Find("Grid").Find("door_bottom").gameObject.SetActive(true);
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
                    gameObject.transform.parent.parent.Find("Spawnery").Find("R").gameObject.SetActive(true);

                    gameObject.transform.parent.parent.Find("Grid").Find("door_right").gameObject.SetActive(true);
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
                    gameObject.transform.parent.parent.Find("Spawnery").Find("L").gameObject.SetActive(true);

                    gameObject.transform.parent.parent.Find("Grid").Find("door_left").gameObject.SetActive(true);
                }


            }
        }

    }
}
