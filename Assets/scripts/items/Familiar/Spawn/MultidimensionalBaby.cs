using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MultidimensionalBaby : MonoBehaviour
{
    private Vector3 _playerTransform;
    private bool _takePosition;
    private List<Vector3> _positionList = new List<Vector3>();
    private List<bool> _waitList = new List<bool>();
    private bool _wait;
    private bool _startWait;
    private bool _playerStay;

    [SerializeField]private GameObject _technology2;

    [SerializeField] private GameObject _longRangeShoot;
    private int _multiBabyIndex;

    private GameObject _createdTears;

    Dictionary<int, List<GameObject>> _dictonaryActiveLongRange = new Dictionary<int, List<GameObject>>();

    private DirectionShoot _directionShoot;


    void Start()
    {
        _directionShoot = GameObject.Find("Player").GetComponent<DirectionShoot>();
        _multiBabyIndex = 0;
        _playerTransform = GameObject.Find("Player").transform.position;
        _takePosition = true;
        _wait = true;
        _playerStay = false;
        EventManager.TeleportFamiliar += ReserList;


    }
    private void Update()
    {
        //sprawdza czy player stoi
        if (_waitList.Count > 3 && (_waitList[_waitList.Count - 1] == true && _waitList[_waitList.Count - 2] == true && _waitList[_waitList.Count - 3] == true))
        {
            _playerStay = true;
        }
        else
        {
            _playerStay = false;
        }
        //jeœli player nie stoi to z dealye pobiera kordy
        if (_takePosition && !_playerStay)
        {

            StartCoroutine(DelayPositon());
            _takePosition = false;
        }
        //jeœli obiekt przeby³ ca³¹ trase usuwa j¹ i bierze now¹ i sprawdza czy powinien staæ
        if (_positionList.Count > 0 && _positionList[0] == transform.position && !_wait)
        {
            if (_waitList.Count > 0)
            {
                _wait = _waitList[0];
                _waitList.RemoveAt(0);
            }

            _positionList.RemoveAt(0);
        }
    }

    void FixedUpdate()
    {
        //poruszanie obiektu 
        if (_positionList.Count > 0 && !_wait)
        {
            transform.position = Vector3.MoveTowards(transform.position, _positionList[0], 3.5f * Time.deltaTime);

        }
        //odpalanie funkcji od czekania
        else if (_positionList.Count > 0 && !_startWait)
        {

            _startWait = true;
            StartCoroutine(Wait());
        }
    }

    private IEnumerator DelayPositon()
    {
        //dodawanie kordów do tablicy
        _positionList.Add(GameObject.Find("Player").transform.position);

        yield return new WaitForSeconds(0.1f);

        //dodaje czekanie do tablicy
        if (_positionList.Count > 1&&_positionList[_positionList.Count - 1] == GameObject.Find("Player").transform.position )
        {
            _waitList.Add(true);
        }
        else
        {
            _waitList.Add(false);

        }

        _takePosition = true;
        
    }
        //czekanie
    private IEnumerator Wait()
    {

        yield return new WaitForSeconds(0.1f);
        _startWait=false;
        _wait=false;
    }
    public void ReserList()
    {
        _positionList = new List<Vector3>();
        _waitList=new List<bool>();

    }

  

    public void MultiBabyLongShootON(GameObject lineRenderer)
    {
        List<GameObject> _longShoot = GameObject.Find("Player").GetComponent<TearsCreator>().ActiveLongRange(2, lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().TypeShoot, gameObject.name);
        _longShoot[0].GetComponent<LongShootParent>().MovePosition(0.2f, GetComponent<BoxCollider2D>().bounds.size);
        _longShoot[1].GetComponent<LongShootParent>().MovePosition(-0.2f, GetComponent<BoxCollider2D>().bounds.size);
       



        _dictonaryActiveLongRange.Add(lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().IndexCreatedLS, _longShoot);
    }

    public void MultiBabyLongShootOff(GameObject lineRenderer)
    {
        _dictonaryActiveLongRange[lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().IndexCreatedLS][0].GetComponent<LongShootParent>().Disable();
        _dictonaryActiveLongRange[lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().IndexCreatedLS][1].GetComponent<LongShootParent>().Disable();

        _dictonaryActiveLongRange.Remove(lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().IndexCreatedLS);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<arrows>()&& !collision.GetComponent<arrows>().MultiBabyHit)
        {
            collision.GetComponent<arrows>().MultiBabyHit = true;
            collision.GetComponent<arrows>().ShootSpeed *=1.3f;
            collision.GetComponent<SpriteRenderer>().material.color = Color.gray;

            GameObject.Find("Player").GetComponent<TearsCreator>().CreateTears(collision.GetComponent<arrows>().Direction, 1, transform.position);

            _createdTears = GameObject.Find("Player").GetComponent<TearsCreator>().Tears[0];
            _createdTears.GetComponent<arrows>().SetVariableFromBrother(collision.gameObject);
            _createdTears.GetComponent<arrows>().MultiBabyHit = true;
            _createdTears.GetComponent<arrows>().NewEndPosition(0.25f, 0.25f);
            _createdTears.GetComponent<arrows>().ShootSpeed *= 1.3f;
            _createdTears.GetComponent<SpriteRenderer>().material.color = Color.gray;


        }

       
        

    }

    




}
