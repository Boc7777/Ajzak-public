using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongShootParent : MonoBehaviour
{
    public String TypeShoot;

    //numer LongShoot który jest uruchomiony
    [HideInInspector] public int IndexCreatedLS;


    //LongeShoot Element
    [SerializeField] private Sprite _brimStart;
    [SerializeField] private GameObject _lineRenderer;
    [SerializeField] private GameObject _raycastLineRenderer;
    [SerializeField]private GameObject _lastBrim;
    [SerializeField]private GameObject _middle;

    //Material
    [SerializeField] private Material _laserMaterial, _brimMidMaterial;


    public int Direction;

    public bool BrimWork;

    //Last Brim Position
    public float WallCord;
    public Vector3 BrimEndCord;

    // zmienne od playera
    private PlayerShooting _playerShooting;
    private PlayerItems _playerItems;

    //przesuniêcie dla laserów
    private Vector3 _moveTech2;

    //zmienne do przesuniêcia
    private float _moveX, _moveY;

    //porusza siê za podanym obiektem
    public String FollowPositionName;
    private Vector3 _followPosition;

    



    void Start()
    {
        BrimWork = false;
        _playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
        _playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();

    }
    
    void FixedUpdate()
    {
        _followPosition = GameObject.Find(FollowPositionName).transform.position;
        _lastBrim.transform.position = transform.GetChild(1).GetChild(1).GetComponent<LongShootMid>().EndPosition;
        transform.position = _followPosition + _moveTech2 + new Vector3(_moveX, _moveY, 0);


    }
    private IEnumerator DelayDisable(float time)
    {
        yield return new WaitForSeconds(time);
        Disable();

    }
    public void Disable()
    {
        GameObject.Find("Player").GetComponent<TearsCreator>().DisableLongRange(IndexCreatedLS);

        BrimWork = false;
    }
    public void SelectTypeShoot(String typeShoot)
    {
        TypeShoot=typeShoot;
        switch (typeShoot)
        {
            case "Brimstone":
                Brimstone();
                break;
            case "Laser":
                Laser();
                break;
            case "Technology2":
                Technology2();
                break;
            
        }
    }

    private IEnumerator StartLongShoot()
    {
        yield return new WaitForSeconds(0.1f);

        _middle.SetActive(true);
        if(TypeShoot=="Brimstone")
        _lastBrim.SetActive(true);

    }
    public void Brimstone()
    {
        StartCoroutine(DelayDisable(1.2f));
        BrimWork = true;

        if (transform.GetChild(1).GetChild(1).GetComponent<LineRenderer>().material != _brimMidMaterial)
        {
            transform.GetChild(1).GetChild(1).GetComponent<LineRenderer>().material = _brimMidMaterial;
            _raycastLineRenderer.GetComponent<RaycastLIneRenderer>().Layer = 7;
        }


        StartCoroutine(StartLongShoot());

        _lastBrim.transform.rotation = Quaternion.Euler(0, 0, Direction);

        

    }

    public void Laser()
    {
        StartCoroutine(DelayDisable(0.2f));


        
        if (transform.GetChild(1).GetChild(1).GetComponent<LineRenderer>().material != _laserMaterial)
        {
            transform.GetChild(1).GetChild(1).GetComponent<LineRenderer>().material = _laserMaterial;
            _raycastLineRenderer.GetComponent<RaycastLIneRenderer>().Layer = 6;
            _raycastLineRenderer.GetComponent<RaycastLIneRenderer>().Layer2 = 7;
        }
        StartCoroutine(StartLongShoot());

        _lineRenderer.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void Technology2()
    {


        if (transform.GetChild(1).GetChild(1).GetComponent<LineRenderer>().material != _laserMaterial)
        {
            transform.GetChild(1).GetChild(1).GetComponent<LineRenderer>().material = _laserMaterial;
            _raycastLineRenderer.GetComponent<RaycastLIneRenderer>().Layer = 6;
            _raycastLineRenderer.GetComponent<RaycastLIneRenderer>().Layer2 = 7;
        }
        StartCoroutine(StartLongShoot());

        _lineRenderer.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void MovePosition(float cord, Vector3 sizeObject)
    {
        if (Direction == 0)
        {
            _moveY = sizeObject.y / 1.9f;
            _moveX = cord;
            


        }
        if (Direction == 180)
        {
            _moveY = -(sizeObject.y / 1.9f);
            _moveX = cord;
            

        }
        if (Direction == -90)
        {

            _moveX = sizeObject.x/1.9f;
            _moveY = cord;

           
        }
        if (Direction == 90)
        {
            _moveX = -(sizeObject.x / 1.9f);
            _moveY = cord;

           
        }

    }
    private void OnEnable()
    {
        Direction = GameObject.Find("Player").GetComponent<DirectionShoot>().Angle;
        transform.rotation = Quaternion.Euler(0,0,GameObject.Find("Player").GetComponent<DirectionShoot>().Rotation);
    }

    private void OnDisable()
    {


        for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
        {
            Transform child = gameObject.transform.GetChild(childIndex);
            child.gameObject.SetActive(false);

        }
      
    }
}