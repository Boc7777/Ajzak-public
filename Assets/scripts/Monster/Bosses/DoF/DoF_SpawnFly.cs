using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoF_SpawnFly : MonoBehaviour
{
    private bool _isAbleToStart;
    private int _randomAmountOfFlies;
    [SerializeField]private GameObject _fly;
    private int _randomTimeToSpawn;
    private int amountOfChildren;
    [HideInInspector]public int _FliesSpawned;
    private int _PhaseCounter;
    private cetral _central;
    private GameObject _IFly;
    private EventManager _eventManager;

    [SerializeField] private Animator _animator;
    private void Start()
    {
        _FliesSpawned = 0;
        _isAbleToStart = true;
        _PhaseCounter = 0;
        amountOfChildren = gameObject.transform.childCount;
        _central = transform.parent.parent.parent.Find("Spawnery").Find("central").GetComponent<cetral>();
        _eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();

    }

    private void Update()
    {
        if (_isAbleToStart && _FliesSpawned <9)
        {
            _randomTimeToSpawn = Random.Range(1, 6);
            StartCoroutine(SpawnFlies(_randomTimeToSpawn));
        }
        if (_FliesSpawned>3)
        {
            _PhaseCounter = 1;
        }
        else
        {
            _PhaseCounter = 0;
        }

        if (_PhaseCounter == 1)
        {
            _eventManager.FuncSwitchAgro();
        }
    }
    private IEnumerator SpawnFlies(int delay)
    {
        _isAbleToStart = false;
        yield return new WaitForSeconds(delay);
        _randomAmountOfFlies = Random.Range(1, 3);
        if(_randomAmountOfFlies == 1)
        {
            _animator.SetTrigger("isAttacking");
            Debug.Log("attack");
            _IFly = Instantiate(_fly, transform.position,Quaternion.identity ,gameObject.transform.parent.parent);
            _central.SpawnedMonsters.Add(_IFly);
            _FliesSpawned++;
        }
        else if(_randomAmountOfFlies == 2)
        {
            _animator.SetTrigger("isAttacking");
            Debug.Log("attack");

            for (int i = 0; i < 3; i++)
            {
                
                _IFly = Instantiate(_fly, transform.position, Quaternion.identity, gameObject.transform.parent.parent);
                _central.SpawnedMonsters.Add(_IFly);
            }
            _FliesSpawned = _FliesSpawned + 3;
        }
        _isAbleToStart = true;


    }


}
