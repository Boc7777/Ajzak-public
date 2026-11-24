using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletParticleDestructor : MonoBehaviour
{
    GameObject _bullet;
    private float _timer = 0;
    void Start()
    {
        
    }

    
    void Update()
    {

        if(transform.parent.transform.Find("bulletGraf") == null)
        {
            _timer += Time.deltaTime;

            if(_timer > 0.5)
            {
                Destroy(transform.parent.gameObject);
            }
        }
        
    }

}
