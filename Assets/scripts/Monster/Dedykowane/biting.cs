using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biting : MonoBehaviour
{
    [SerializeField] bool _animatorNeeded;
    public Animator _animatorr;
    private bool _attacked =false;

    [SerializeField] private float _attackCoolDown = 1f;
    [SerializeField] private int _damage = 1;

    private PlayerHealth _playerHealth;
    private void Start()
    {
        _playerHealth = GameObject.Find("Player/HitBox").GetComponent<PlayerHealth>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            if (!_attacked)
            {
                StartCoroutine(Attack(_attackCoolDown));
            }
        }
        
    }
    IEnumerator Attack( float delay)
    {
        if (_animatorNeeded)
        {
            _animatorr.SetTrigger("attack");
        }
       
        _attacked = true;
        _playerHealth.TakeDamage(_damage);
        yield return new WaitForSeconds(delay);
        _attacked = false;
    }
}
