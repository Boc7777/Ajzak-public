using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusMove : MonoBehaviour
{
    [SerializeField]private float _direction;
    void Start()
    {
        _direction = Random.Range(-180, 180);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _direction);

    }

    void FixedUpdate()
    {
       transform.parent.transform.Translate(transform.up * 3f*Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            if ((transform.rotation.z > -0.7f && collision.gameObject.name == "right") || (transform.rotation.z > 0.7f && collision.gameObject.name == "left") || (transform.rotation.z > 0 && collision.gameObject.name == "top") || transform.rotation.z < 0 && collision.gameObject.name == "bottom")
            {
                _direction += Random.Range(100, 120);

            }
            else
            {
                _direction -= Random.Range(100, 120);

            }

            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _direction);
        }
    }
}
