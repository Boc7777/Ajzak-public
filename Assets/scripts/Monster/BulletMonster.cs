using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMonster : MonoBehaviour
{
    private Vector3 _kordyGracza;
    public float _movementArrow = 5;
    [SerializeField] private float _bulletRange = 5;
    private float _bulletPosition;
    private PlayerHealth _health;
    public int Damage;

    void Start()
    {
        _health = GameObject.Find("Player/HitBox").GetComponent<PlayerHealth>(); 
        _kordyGracza =  GameObject.Find("Player").transform.position;
        _kordyGracza = (_kordyGracza - transform.position).normalized;

        transform.rotation = Quaternion.FromToRotation(Vector3.right, _kordyGracza);
        _bulletPosition = 0;
        
    }

    
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, _kordyGracza *2, Time.deltaTime * _movementArrow); 
        transform.position += _kordyGracza*(Time.deltaTime * _movementArrow);

        
        _bulletPosition += Time.deltaTime * _movementArrow;
        if(_bulletPosition >= _bulletRange)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.transform.root.tag);
       
        if (collision.CompareTag("wall"))
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(transform.Find("bulletGraf").gameObject);
        }
        if (collision.CompareTag("HitBox"))
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            _health.TakeDamage(Damage);
            Destroy(transform.Find("bulletGraf").gameObject);
        }
        if (collision.CompareTag("Rocks"))
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(transform.Find("bulletGraf").gameObject);
        }

        if (collision.CompareTag("HitboxMapElement"))
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(transform.Find("bulletGraf").gameObject);
        }
            
    }

}
