using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSpawner : MonoBehaviour
{
    private prefabsContainer _prefabsContainer;
    private int _rand;
    void Start()
    {
        _prefabsContainer = GameObject.FindGameObjectWithTag("container").GetComponent<prefabsContainer>();

        _rand = Random.Range(0, _prefabsContainer.Czachy.Count);

        Instantiate(_prefabsContainer.Czachy[_rand], gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);

        Destroy(gameObject, 1f);
    }

    
}
