using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Monster : MonoBehaviour
{
    private NavMeshAgent _NavMeshAgent;
    private Rigidbody2D rb;
    [HideInInspector] public Vector3 _kordyGracza;
    [SerializeField] private float _movespeed;
    public Animator _animator;
    private bool _isMoving = true;
    [SerializeField] private float _sizeMonster;
    [SerializeField] private bool _strzelajacy;
    [SerializeField] private Transform _magazynek;
    [SerializeField] private GameObject _bullet;
    private GameObject _bulletInst;
    private float _distance;
    private bool _idzie;
    [SerializeField] private bool _latajacy;

    private Vector3 previousPosition;
    private Vector3 currentDirection;


    private float _timer =1 ;
    private float _cooldown =1 ;

   


    void Start()
    {
        _NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
       
        rb = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        transform.rotation = Quaternion.identity;
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (_strzelajacy == true)
        {
            Animacja();
            PoruszanieStrzelajacego();

        }

        if (_strzelajacy == false&&_latajacy==false)
        {
            PoruszanieAI();
            Animacja();
        }

        if (_latajacy)
        {
            Poruszanie();

        }

        
            
        _timer += Time.deltaTime;

        
        
    }
    
    void Poruszanie()
    {
        _kordyGracza = GameObject.Find("Player").transform.position;
        Vector2 _direction = _kordyGracza - gameObject.transform.position;
        _direction.Normalize();
        

        rb.velocity = _direction * _movespeed;
    }
    void Animacja()
    {
        _animator.SetBool("isMoving", _isMoving);
        gameObject.transform.rotation = Quaternion.identity;

        currentDirection = (gameObject.transform.position - previousPosition).normalized;
        previousPosition = gameObject.transform.position;

        if (currentDirection.x > 0)
        {
            gameObject.transform.localScale = new Vector3(-(_sizeMonster), _sizeMonster, 1);
        }
        else if (currentDirection.x < 0)
        {
            gameObject.transform.localScale = new Vector3(_sizeMonster, _sizeMonster, 1);
        }

    }

    void PoruszanieStrzelajacego()
    {
        _kordyGracza = GameObject.Find("Player").transform.position;

        Vector2 _direction = _kordyGracza - gameObject.transform.position;
        _distance = Vector2.Distance((Vector2)_kordyGracza, (Vector2)gameObject.transform.position);
        
        if (_distance < 3)
        {
            _idzie = false;
            rb.velocity = Vector2.zero;
            

        }
        else if (_distance >7)
        {
            
            _idzie = true;
        }

        if (_idzie)
        {
            Poruszanie();
            _isMoving = true;
        }
        else
        {
            Shooting();
            _isMoving = false;
        }       
    }


    void Shooting()
    {
        if (_timer > _cooldown)
        {
            _bulletInst = Instantiate(_bullet, gameObject.transform.position, Quaternion.identity, _magazynek);
            _timer = 0;
            
        }
    }

    void PoruszanieAI()
    {
        _kordyGracza = GameObject.FindGameObjectWithTag("Player").transform.position;
        _NavMeshAgent.SetDestination(_kordyGracza);
    }


}
