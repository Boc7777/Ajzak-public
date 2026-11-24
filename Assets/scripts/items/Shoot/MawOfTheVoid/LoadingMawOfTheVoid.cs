using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LoadingMawOfTheVoid : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _mawOfTheVoid;
    public float Fill;
    public float Timer;

    private GameObject _loadingCircle;
    private PlayerShooting _playerShooting;
    private DirectionShoot _directionShoot;
    private GameObject _player;
    private PlayerStats _playerStats;


    void Awake()
    {
        _playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
        _loadingCircle = GameObject.Find("LoadingCircle");
        _player = GameObject.Find("Player");

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
        _loadingCircle.transform.position = _player.transform.position + new Vector3(0.5f, 0.2f, 0);
        Timer += Time.deltaTime;

        Fill += 1 / (_playerStats.Tears * 50f);
        _image.fillAmount = Fill;
        if (_image.fillAmount == 1.0f && _directionShoot.SumDirection == 0)
        {
            
            _mawOfTheVoid.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (_directionShoot.SumDirection == 0)
        {
            gameObject.SetActive(false);

        }


    }
}
