using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTakeDmg : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deadParticle;

    private PlayerStats _playerStats;
    public float MonsterHealth;
    private arrows _arrowsScript;
    private bool _startEffect;
    private bool _hitted;
    private float _timerPoison = 2f;
    private float _timerHitted;
    private int _poisonCount;
    private cetral _cetral;

    private audioManager _audioManager;

    private int _bombDamage = 100;
    private float _bombDamageMultiplier = 1f;

    private PlayerItems _itemsPlayer;

    private float _playerDmg;

    private bool _succubusDmg;
    private bool _spearofDestinyDmg;

    private delegate void Callback();
    private void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        _cetral = gameObject.transform.parent.parent.parent.Find("Spawnery/central").gameObject.GetComponent<cetral>();
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();

    }

    private void Update()
    {
        if (MonsterHealth <= 0)
        {
            _cetral.SpawnedMonsters.Remove(transform.parent.gameObject);
            Instantiate(_deadParticle, transform.position, Quaternion.identity, transform.parent);
            _audioManager.PlaySound("gipsisDeath");
            Destroy(transform.parent.gameObject);
        }
        if (_startEffect)
        {

            Effect();
        }


        if (_hitted)
            HittedColor();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("arrow") && !collision.GetComponent<arrows>().Ipecac)
        {
            _arrowsScript = collision.gameObject.GetComponent<arrows>();

            _playerDmg = _arrowsScript.Dmg;
            if (collision.GetComponent<arrows>().Copy == true)
                _playerDmg /= 2;


            _startEffect = true;
            if (_arrowsScript.Poison == false)
            {
                _hitted = true;
            }
            _poisonCount = 0;
            _timerPoison = 2;
            if (_arrowsScript.Proptosis)
            {
                _playerDmg *= _arrowsScript.PercentScale;
            }
            MonsterHealth -= _playerDmg;

            if (MonsterHealth > 0)
            {
                _audioManager.PlaySound("gipsisDamage");
            }
        }
        if (collision.CompareTag("Explosion"))
        {
            if (collision.gameObject.GetComponent<IpecacBoom>())
            {
                MonsterHealth -= _playerStats.Dmg;

            }
            if (collision.gameObject.GetComponent<arrows>())
            {
                MonsterHealth -= collision.gameObject.GetComponent<arrows>().Dmg * 3;

            }
            else
            {
                MonsterHealth -= (_bombDamage * _bombDamageMultiplier);

            }
        }
        if (collision.CompareTag("LongRangeShoot"))
        {
            if (collision.transform.parent.parent.GetComponent<LongShootParent>().TypeShoot == "Laser")
            {
                MonsterHealth -= _playerStats.Dmg * 0.2f;
                _hitted = true;
            }
        }



    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "rangeArea" && !_succubusDmg)
        {
            _succubusDmg = true;
            StartCoroutine(DmgDelay(0.5f, 1, () => _succubusDmg = false));
        }
        if (collision.CompareTag("LongRangeShoot"))
        {

            if (collision.transform.parent.parent.GetComponent<LongShootParent>().TypeShoot == "Brimstone")
            {
                MonsterHealth -= 0.04f * _playerStats.Dmg;
                _hitted = true;

            }

            if (collision.transform.parent.parent.GetComponent<LongShootParent>().TypeShoot == "Technology2")
            {
                MonsterHealth -= 0.01f * _playerStats.Dmg;
                _hitted = true;
            }

        }
        if (collision.CompareTag("MomsKnife"))
        {
            MonsterHealth -= 0.2f * _playerStats.Dmg;


        }

        if (collision.gameObject.name == "SpearofDestiny(Clone)" && !_spearofDestinyDmg)
        {
            _spearofDestinyDmg = true;
            StartCoroutine(DmgDelay(1f, _playerStats.Dmg * 2, () => _spearofDestinyDmg = false));

        }
    }

    private void Effect()
    {



        if (_arrowsScript.Poison == true)
        {
            _timerPoison += Time.deltaTime;
            if (_timerPoison > 2f && _poisonCount < 2)
            {
                for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
                {
                    Transform child = gameObject.transform.GetChild(childIndex);
                    child.GetComponent<Renderer>().material.color = Color.green;

                }
                MonsterHealth -= 2f;
                _poisonCount++;
                _timerPoison = 0f;

            }
            else if (_poisonCount >= 2)
            {
                for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
                {
                    Transform child = gameObject.transform.GetChild(childIndex);
                    child.GetComponent<Renderer>().material.color = Color.white;

                }

                _startEffect = false;

            }



        }

    }
    private void HittedColor()
    {

        _timerHitted += Time.deltaTime;
        for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
        {
            Transform child = gameObject.transform.GetChild(childIndex);
            child.GetComponent<Renderer>().material.color = Color.red;
        }
        if (_timerHitted > 0.2f)
        {
            for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
            {
                Transform child = gameObject.transform.GetChild(childIndex);
                child.GetComponent<Renderer>().material.color = Color.white;

            }
            _timerHitted = 0f;
            _hitted = false;
        }
    }

    private IEnumerator DmgDelay(float delay, float dmg, Callback callback)
    {
        MonsterHealth -= dmg;
        _hitted = true;
        yield return new WaitForSeconds(delay);
        if (callback != null)
        {
            callback();
        }
    }
}