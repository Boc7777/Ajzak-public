using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.LightAnchor;


public class loadingShoot : MonoBehaviour
{
    [SerializeField] private Image _image;
    public float Fill;
    public float Timer;

    [SerializeField] private GameObject _brimStart;
    [SerializeField] private GameObject _brimEnd;
    [SerializeField] private GameObject _brimParent;
    private LongShootParent _longShootParent;

    [SerializeField] private GameObject _tears;

    private DirectionShoot _directionShoot;
    private PlayerShooting _playerShooting;
    private GameObject _loadingCircle;
    private GameObject _player;
    private GameObject _momsKnife;
    private PlayerStats _playerStats;
    private PlayerItems _playerItems;
    private float _sumDirection = 1;


    void Awake()
    {
        _playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
        _loadingCircle = GameObject.Find("LoadingCircle");
        _player = GameObject.Find("Player");
        _playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();

        _directionShoot = GameObject.Find("Player").GetComponent<DirectionShoot>();


        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();


        Timer = 0f;
        Fill = 0f;
        
    }
    private void OnEnable()
    {

        Timer = 0f;

        Fill = 0f;
    }


    void Update()
    {
        _loadingCircle.transform.position = _player.transform.position+ new Vector3(0.5f,0.2f,0);
        Timer += Time.deltaTime;

        if (_playerItems.MomsKnife)
            _momsKnife = GameObject.Find("Mom's Knife(Clone)");

        if (Timer > 0.07f)
        {
            Fill += 1/(_playerStats.Tears*50f);
            _image.fillAmount = Fill;
            if (_image.fillAmount < 0.3f)
            {
                _image.color = Color.red;
            }
            else if (_image.fillAmount < 0.6f)
            {
                _image.color = Color.yellow;
            }
            else 
            {
                _image.color = Color.green;
            }
            

            Timer =0f;
        }
        if (_playerItems.MomsKnife)
        {
            MomsKnife();
        }
        else if (_playerItems.BrimstoneActive)
        {
            Brimstone();
        }
        else if(_playerItems.CursedEye){
            CursedEye();
        }
        else if(_playerItems.ChocolateMilkActive)
        {
            ChocolateMilk();
        }

        
    }
    private void MomsKnife()
    {
        
        if (_directionShoot.SumDirection == 0)
        {

            //_momsKnifeScript.Throw = true;
            _momsKnife.transform.GetChild(0).GetComponent<MomsKnifeMove>().enabled = true;
            _momsKnife.transform.GetChild(0).GetComponent<MomsKnifeMove>().Distance = _image.fillAmount;
            _momsKnife.transform.GetChild(0).gameObject.GetComponent<MomsKnifeMove>().SetPatch(_sumDirection);


            gameObject.SetActive(false);

        }
        else
        {
            _sumDirection = _directionShoot.SumDirection;
        }
    }

    private void Brimstone()
    {
        if (_image.fillAmount == 1.0f && _directionShoot.SumDirection == 0)
        {
            List<GameObject> _brimstone = GameObject.Find("Player").GetComponent<TearsCreator>().ActiveLongRange(1, "Brimstone", "Player");
            _brimstone[0].GetComponent<LongShootParent>().MovePosition(0, GameObject.Find("Player").GetComponent<SpriteRenderer>().bounds.size);
            _brimstone[0].transform.GetChild(1).GetChild(0).GetComponent<RaycastLIneRenderer>().BrimstoneActive = true;
            gameObject.SetActive(false);

            
        }
        else if (_directionShoot.SumDirection == 0)
        {
            gameObject.SetActive(false);

        }
    }
    
    private void CursedEye()
    {
        if (_directionShoot.SumDirection == 0)
        {
            if (_image.fillAmount <= 0.5f&&_image.fillAmount>0.25f)
            {
                _playerShooting.CountTearsSpawn = 1;
                _playerShooting.CursedEyeDelay();
            }
            else if (_image.fillAmount > 0.5f && _image.fillAmount <= 0.75f)
            {

                _playerShooting.CountTearsSpawn = 2;
                _playerShooting.CursedEyeDelay();


            }
            else if (_image.fillAmount > 0.75f && _image.fillAmount < 1f)
            {

                _playerShooting.CountTearsSpawn = 3;
                _playerShooting.CursedEyeDelay();
            }
            else if(_image.fillAmount == 1f)
            {

                _playerShooting.CountTearsSpawn = 4;
                _playerShooting.CursedEyeDelay();
            }

            gameObject.SetActive(false);

        }
    }

    private void ChocolateMilk()
    {
        if (_directionShoot.SumDirection == 0)
        {

        }
    }
    
}
