using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{


    private float _timer = 2;
    //pojawianie tears
    public GameObject CurrentlyTears;
    [SerializeField] private GameObject _arrow;
    [SerializeField] public Transform _parent;
    [HideInInspector] public Vector3 ArrowScale;
    [HideInInspector] public Color ArrowColor;
    public float MoveX;
    public float MoveY;
    public float MoveValue;

    private int _oldAngle;

    //script direction
    private DirectionShoot _directionShoot;


    //loadingShot
    [SerializeField] private Image _loadingShoot;
    [HideInInspector]public bool Stoploading;
    private bool _activeLaser;
    private bool _longShootDelay;


    //ilosc tears cursed eye
    public int CountTearsSpawn;

    //long shot
    //  [SerializeField] private GameObject _brimStart;
    [SerializeField]private LongShootParent _longShootParent;

    //antiGraivty
    private List<GameObject> _tearsList = new List<GameObject>();
    private List<GameObject> _doubleShootList = new List<GameObject>();

    //DrFetus
    [HideInInspector]public GameObject TearsBomb;

    //MawOfTheVoid
    [SerializeField] private Image _mawOfTheVoidImage;


    //player
    private PlayerItems _playerItems;
    private PlayerStats _playerStats;

    

    [HideInInspector] public float Angle2;


    private bool technology2Active;

    private float _oldDirectionTechnology2, _oldDirectionLaser;

    private List<GameObject> _technology2;
    private List<GameObject> _laser = new List<GameObject>();

    private void Start()
    {
        _directionShoot = GetComponent<DirectionShoot>();
        _playerStats = GetComponent<PlayerStats>();
        _playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        CurrentlyTears = _arrow;




        MoveValue = 0.1f;
    }
    private void Update()
    {
        _timer+= Time.deltaTime;
        
    }

    void FixedUpdate()
    {

        
        SelectTypeShoot();


        
        


    }

    private void SelectTypeShoot()
    {
        if (_playerItems.Technology2 && !_longShootDelay)
        {
            Technology2();
        }

        if(_playerItems.MawOfTheVoidActive && !_longShootDelay)
        {
            MawOfTheVoid();
        }

        if ((_playerItems.BrimstoneActive  || _playerItems.CursedEye || _playerItems.ChocolateMilkActive) && !_longShootDelay)
        {

            LoadingShoot();

        }
        else if (_playerItems.MomsKnife  && !_longShootDelay)
        {
            GameObject _momsKnife = GameObject.Find("Mom's Knife(Clone)");

            if(!_momsKnife.transform.GetChild(0).GetComponent<MomsKnifeMove>().enabled)
            {
                LoadingShoot();
            }
        }
        else if (_playerItems.LaserActive && !_longShootDelay)
        {
            LaserCreate();
        }
        else if (_timer > _playerStats.Tears && _directionShoot.SumDirection != 0)
        {
            ArrowsCreator();


        }
    }

    

    private void ArrowsCreator()
    {

        if (_playerItems.SpiderActive)
        {
            GameObject.Find("Player").GetComponent<TearsCreator>().CreateTears(_directionShoot.Angle, 3, transform.position);
            _tearsList = GameObject.Find("Player").GetComponent<TearsCreator>().Tears;
            _tearsList[0].GetComponent<arrows>().SetEndPosition(2.5f, 2.5f);
            _tearsList[1].GetComponent<arrows>().SetEndPosition(-2.5f, -2.5f);


        }
        else
        {

            GameObject.Find("Player").GetComponent<TearsCreator>().CreateTears(_directionShoot.Angle, 1, transform.position);
            _tearsList = GameObject.Find("Player").GetComponent<TearsCreator>().Tears;

        }

        if (_playerItems.DoubleShoot)
        {
            GameObject.Find("Player").GetComponent<TearsCreator>().CreateTears(_directionShoot.Angle, _tearsList.Count,transform.position);
            _doubleShootList = GameObject.Find("Player").GetComponent<TearsCreator>().Tears;
            for (int i = 0; i < _tearsList.Count; i++)
            {
                _doubleShootList[i].GetComponent<arrows>().EndPosition = _tearsList[i].GetComponent<arrows>().EndPosition;
                _doubleShootList[i].GetComponent<arrows>().NewEndPosition(0.25f,0.25f);
                
            }
        }


        _timer = 0;

    }
    public void CursedEyeDelay()
    {
        StartCoroutine(DelayTears(0.04f));
    }

    public IEnumerator DelayTears(float _delay)
    {
        ArrowsCreator();
        CountTearsSpawn--;
        yield return new WaitForSeconds(_delay);
        if(CountTearsSpawn > 0)
        {
            StartCoroutine(DelayTears(0.04f));
        }

    }
    private void LoadingShoot()
    {
        
        if(_loadingShoot.gameObject.activeSelf==false&&Stoploading==false && _directionShoot.SumDirection != 0)
        {
            if(_longShootParent.gameObject.activeSelf)
            _longShootParent.Disable();

            _loadingShoot.gameObject.SetActive(true); 

        }


    }

    private void MawOfTheVoid()
    {
        

        if (_mawOfTheVoidImage.gameObject.activeSelf == false && Stoploading == false && _directionShoot.SumDirection != 0)
        {

            _mawOfTheVoidImage.gameObject.SetActive(true);

        }

        
    }
    
    private void LaserCreate()
    {
        

        if (!_activeLaser && _directionShoot.SumDirection != 0)
        {
            

            _laser = GameObject.Find("Player").GetComponent<TearsCreator>().ActiveLongRange(1,"Laser", "Player");
            _laser[0].GetComponent<LongShootParent>().MovePosition(-0.2f, GetComponent<BoxCollider2D>().bounds.size);
            _activeLaser =true;
            _oldDirectionLaser = _directionShoot.SumDirection;

        }
        else if (_activeLaser)
        {
            if ( (_oldDirectionLaser != _directionShoot.SumDirection || _directionShoot.SumDirection == 0) && _laser[0].activeSelf)
            {
                _laser[0].GetComponent<LongShootParent>().Disable();
                _activeLaser = false;
            }
            else if(!_laser[0].activeSelf)
            {
                _activeLaser = false;

            }
        }




    }

   
    

    private void Technology2()
    {
        if (!technology2Active && _directionShoot.SumDirection != 0)
        {
            _technology2 = GameObject.Find("Player").GetComponent<TearsCreator>().ActiveLongRange(1, "Technology2", "Player");
            _technology2[0].GetComponent<LongShootParent>().MovePosition(0.2f, GetComponent<BoxCollider2D>().bounds.size);

            _oldDirectionTechnology2 = _directionShoot.Angle;
            technology2Active = true;


        }
        else if (technology2Active)
        {
            if ((_oldDirectionTechnology2 != _directionShoot.Angle || _directionShoot.SumDirection == 0)&& _technology2[0].activeSelf) {
                _technology2[0].GetComponent<LongShootParent>().Disable();
                technology2Active = false;
            }
            else if (!_technology2[0].activeSelf)
            {
                technology2Active = false;

            }
        }
    }

}
