using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleDeleter : MonoBehaviour
{
    private float _timer = 0;
    [SerializeField] private float _timeToDelete;
    void Update()
    {
       _timer += Time.deltaTime;
       
        if( _timer > _timeToDelete)
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }
}
