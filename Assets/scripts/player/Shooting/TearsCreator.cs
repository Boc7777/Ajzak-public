using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearsCreator : MonoBehaviour
{
    //zwi¹zane z tears
    public GameObject CurrentlyTears;

    private PlayerItems _playerItems;
    private PlayerShooting _playerShooting;

    //instancjonowanie
    public float MoveX, MoveY;
    private GameObject _tears;
    public float MoveValue;
    [SerializeField] public Transform _parent;

    private float _angelicPrismMove;

    private List<Color> color = new List<Color>();
    private int _randomColor;
    //

    //longRangeShoot
    [SerializeField] private Transform _LShootParent;

    [SerializeField] private GameObject _LGShootIns;
    public int ActiveLongRangeCount;
    private GameObject _ins;


    public List<GameObject> Tears;

    //lista wolnych parentów do u¿ycia
    List<int> _freeParentIndex = new List<int>();




    private void Start()
    {
        MoveValue = 0.05f;
        _playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        _playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();

        ActiveLongRangeCount = 0;
        _freeParentIndex.Add(0);
    }
    public void CreateTears(float Angle, int createCount, Vector3 spawnPosition)
    {
        //Vector3 hitPosition
        if (Angle == 0 || Angle == 180)
        {
            MoveX = 1;
        }
        else
        {
            MoveX = 0;
        }
        if (Angle == 90 || Angle == -90)
        {
            MoveY = 1;
        }
        else
        {
            MoveY = 0;
        }

        Tears = new List<GameObject>();


        for (int i = 0; i < createCount; i++)
        {
            //instancjowanie tears
            _tears = Instantiate(CurrentlyTears, spawnPosition + new Vector3(MoveValue * MoveX, MoveValue * MoveY, 0), Quaternion.Euler(0, 0, 0), _parent);
            _tears.GetComponent<arrows>().SetEndPosition(0, 0);
            _tears.GetComponent<arrows>().ShootingX = MoveX;
            _tears.GetComponent<arrows>().ShootingY = MoveY;
            _playerItems.ActiveItems(_tears);
            //dodawanie go do antigravity
            if (_playerItems.AntiGravityActive)
            {
                //_playerShooting.AntyGravity(_tears);
            }
            Tears.Add(_tears);


            //if(hit== "AngelicPrim")
            //{
            //    _tears.transform.position = hitPosition;
            //    _tears.GetComponent<arrows>().ChangeX = _angelicPrismMove;
            //    _tears.GetComponent<arrows>().ChangeY = _angelicPrismMove;
            //    _tears.GetComponent<arrows>().AngelicPrismSpawn = true;

            //    _randomColor = Random.Range(0, color.Count);
            //    _tears.GetComponent<Renderer>().material.color = color[_randomColor];
            //    color.RemoveAt(_randomColor);
            //    _angelicPrismMove += 3f;
            //}
        }


        //przesuniêcie tears
        if (MoveValue == 0.05f)
        {
            MoveValue = -0.05f;

        }
        else if (MoveValue == -0.05f)
        {
            MoveValue = 0.05f;

        }



    }

    public void CreateLongRange(int createCount)
    {
        for (int i = 0; i < createCount; i++)
        {
            _freeParentIndex.Add(_LShootParent.childCount);
            _ins = Instantiate(_LGShootIns, transform.position, Quaternion.identity, _LShootParent);


        }

    }
    public List<GameObject> ActiveLongRange(int toActiveCount,string typeShoot, string followObject)
    {
        if (_freeParentIndex.Count < toActiveCount)
        {
            CreateLongRange(toActiveCount - _freeParentIndex.Count);
        }

       

        List<GameObject> elements = new List<GameObject>();
        for (int i = 0; i < toActiveCount; i++)
        {
            //uruchamia pojedyñczego parenta 
            _LShootParent.GetChild(_freeParentIndex[0]).gameObject.SetActive(true);

            _LShootParent.GetChild(_freeParentIndex[0]).GetComponent<LongShootParent>().SelectTypeShoot(typeShoot);

            _LShootParent.GetChild(_freeParentIndex[0]).GetComponent<LongShootParent>().IndexCreatedLS = _freeParentIndex[0];

            _LShootParent.GetChild(_freeParentIndex[0]).GetComponent<LongShootParent>().FollowPositionName = followObject;

            //_LShootParent.GetChild(_freeParentIndex[0]).GetComponent<LongShootParent>().Direction = Direction;

            elements.Add(_LShootParent.GetChild(_freeParentIndex[0]).gameObject);


            _freeParentIndex.RemoveAt(0);

        }
        return elements;

    }
    public void DisableLongRange(int indexElement)
    {

       
        _LShootParent.GetChild(indexElement).gameObject.SetActive(false);

        _playerShooting.Stoploading = false;

        _freeParentIndex.Add(indexElement);


    }
}
