using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilAngelRoomItemSpawner : MonoBehaviour
{
    [SerializeField] private Sprite threeSoulHearts;
    private PlayerHealth _playerHealth;
    private RoomsPrefabs _templates;
    private float _timer;
    private bool _timerWork = true;
    public bool HeartPrice;
    public int _serca;
    public bool _podmiana = false;
    private bool _wybranaCena = false;

    //true == czerwone false == soulHearty
    private bool _wybraneSerca = false;

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("HitBox").GetComponent<PlayerHealth>();
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();

        _templates.DevilRoomExist = true;
    }

    private void Update()
    {
        if (_timerWork == true) {
            _timer += Time.deltaTime;
        }

        if (_timer > 2)
        {
            _timerWork = false;
        }

        //okreslana cena w sercach po nazwie serduch pod itemkiem
        if (_timer > 1f && _wybranaCena == false)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).name.Contains("FullHeart"))
                {
                    _serca = 1;
                }
                if (gameObject.transform.GetChild(i).name.Contains("2redheart"))
                {
                    _serca = 2;
                }
                if (gameObject.transform.GetChild(i).name.Contains("3SoulHearts"))
                {
                    _serca = 3;
                }
            }
            _wybranaCena = true;
        }

        if (_playerHealth.RedHeartExist == false && _podmiana == false)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).name.Contains("FullHeart") || gameObject.transform.GetChild(i).name.Contains("2redheart"))
                {
                    gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = threeSoulHearts;
                    _serca = 3;
                    _podmiana = true;
                }
            }

        }
    
        //if itemek zostanie zabrany usuwan sie spawner z serduchami pod
        if (gameObject.transform.childCount < 2 && _timer >1 )
        {
            Debug.Log("spawner");
            if (_serca == 1 || _serca == 2)
            {
                _playerHealth.HeartDeleter(_serca, 40);
            }
            else if(_serca == 3)
            {
                _playerHealth.HeartDeleter(_serca, 52);
            }
            if (gameObject.transform.parent.GetComponent<DevilAngelRoomItemManager>().DevilAngelRoom == false)
            {
                _templates.ChanceAngelRoom = false;
                _templates.PermanentZeroChanceAngelRoom = true;
            }

            Destroy(gameObject);
        }
    }

}
