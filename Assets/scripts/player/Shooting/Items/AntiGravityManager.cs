using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravityManager : MonoBehaviour
{
    private DirectionShoot _directionShoot;
    void Start()
    {
        _directionShoot = GameObject.Find("Player").GetComponent<DirectionShoot>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.GetChild(0).childCount > 8)
        {
            StartOneTears();
        }

        if (_directionShoot.SumDirection == 0 && transform.GetChild(0).childCount != 0)
        {
            StartALlTears();
        }
    }

    private void StartOneTears()
    {
        transform.GetChild(0).GetChild(0).GetComponent<arrows>().StopMove = false;
        transform.GetChild(0).GetChild(0).SetParent(transform.GetChild(1));
    }
    

    private void StartALlTears()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            transform.GetChild(0).GetChild(i).GetComponent<arrows>().StopMove = false;
        }
    }
}
