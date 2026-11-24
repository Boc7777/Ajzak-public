using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    private prefabsContainer _prefabsContainer;
    private int _rand;
    private int _rand2;
    private int _rand3;

    private GameObject kamien;

    public bool leftColliderOn = false;
    public bool rightColliderOn = false;
    void Start()
    {
        _prefabsContainer = GameObject.FindGameObjectWithTag("container").GetComponent<prefabsContainer>();

        _rand = Random.Range(0, 100);
        if (_rand <=1)
        {
            _rand2 = Random.Range(0, _prefabsContainer.RocksX.Count);
            kamien =  Instantiate(_prefabsContainer.RocksX[_rand2], gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        }
        else
        {
            _rand3 = Random.Range(0, _prefabsContainer.Rocks.Count);
            kamien = Instantiate(_prefabsContainer.Rocks[_rand3], gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        }

        if (leftColliderOn)
        {
            kamien.transform.Find("colliders").GetComponent<rockCollidersControler>().leftColliderOn();
            leftColliderOn = false;
        }
        if (rightColliderOn)
        {
            kamien.transform.Find("colliders").GetComponent<rockCollidersControler>().rightColliderOn();
            rightColliderOn = false;
        }
        
        Destroy(gameObject, 1f);
    }  
}
