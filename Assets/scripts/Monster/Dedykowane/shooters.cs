using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shooters : MonoBehaviour
{
    private NavMeshAgent _NavMeshAgent;
    public Animator _animator;

    private Vector3 _kordyGracza;
    [SerializeField] private float _movespeed;

    [SerializeField] private float _sizeMonster;
    [SerializeField] private Transform _magazynek;
    [SerializeField] private GameObject _bullet;

    private float _cooldown = 1;
    private bool _isMoving = true;
    private bool shotFired = false;

    private Vector3 previousPosition;
    private Vector3 currentDirection;

    private RaycastHit2D _hit;

    private Vector3 raycastDir;
    private float _distance;

    private GameObject _arrow;


    void Start()
    {
        _NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Animacja();

        _kordyGracza = GameObject.FindGameObjectWithTag("Player").transform.position;
        raycastDir = _kordyGracza - transform.position;
        _distance = Vector2.Distance((Vector2)_kordyGracza, (Vector2)gameObject.transform.position);

        _hit = Physics2D.Raycast(transform.position, raycastDir, Mathf.Infinity, 1 << 6 | 1 << 9);

        //Debug.Log(_hit.collider.name);

        if(_hit.collider.name == "HitBox" && _distance <3)
        {
            _isMoving = false;
            _NavMeshAgent.isStopped = true;
        }
        else if(_distance > 5 || _hit.collider.name != "HitBox")
        {
            Poruszanie();
            _isMoving = true;
            _NavMeshAgent.isStopped = false;
        }
        if(_isMoving == false)
        {
            if (shotFired == false)
            {
                StartCoroutine(Shooting(0.5f));
                shotFired = true;
            }
        }
        
    }

    private void Poruszanie()
    {     
        _NavMeshAgent.SetDestination(_kordyGracza);      
    }

    private void Animacja()
    {
        _animator.SetBool("isMoving", _isMoving);
        gameObject.transform.rotation = Quaternion.identity;
 
        currentDirection = (gameObject.transform.position - previousPosition).normalized;
        previousPosition = gameObject.transform.position;

        if (currentDirection.x >0)
        {
            gameObject.transform.localScale = new Vector3(-(_sizeMonster), _sizeMonster, 1);
        }
        else if (currentDirection.x < 0)
        {
            gameObject.transform.localScale = new Vector3(_sizeMonster, _sizeMonster, 1);
        }
    }

    IEnumerator Shooting(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        _arrow = Instantiate(_bullet, gameObject.transform.position, Quaternion.identity, _magazynek);
        _arrow.GetComponent<BulletMonster>().Damage = 2;
        shotFired = false;
    }
}
