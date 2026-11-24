using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFromDOF : MonoBehaviour
{
    private GameObject _spawnerDoF;

    private void Start()
    {
        _spawnerDoF = transform.parent.parent.Find("DukeOfFly(Clone)").gameObject;
    }
    private void OnDestroy()
    {
        _spawnerDoF.transform.GetChild(1).GetComponent<DoF_SpawnFly>()._FliesSpawned--;
    }

}
