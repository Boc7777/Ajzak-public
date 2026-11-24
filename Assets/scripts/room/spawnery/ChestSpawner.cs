using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    private prefabsContainer _prefabsContainer;
    private int _rand;
    [SerializeField] private int _probably; 

    void Start()
    {
        _rand = Random.Range(0, 100);
        if (_rand <= _probably)
        {
            _prefabsContainer = GameObject.FindGameObjectWithTag("container").GetComponent<prefabsContainer>();
            _rand = Random.Range(0, _prefabsContainer.Chests.Count);
            Instantiate(_prefabsContainer.Chests[_rand], gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        }

        Destroy(gameObject, 1f);
    }
}
