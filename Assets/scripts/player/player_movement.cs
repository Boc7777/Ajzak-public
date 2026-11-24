using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private audioManager _audioManager;

    public float Horizontal;
    public float Vertical;
    public float BlockMovement = 1;

    [HideInInspector]  public bool Sides;
    [HideInInspector]  public bool Top;
    [HideInInspector]  public bool Bottom;

    public Animator animator;

    private float _movementSpeed;
    private PlayerStats _playerStats;

    [SerializeField]private GameObject _skillCircle;
    [SerializeField] private GameObject _familiarPlayer;

    public float DynamicMove;

    public GameObject CurrentlyCentral;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
    }

    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        Poruszanie();
        
       
        Animacja();

        animator.SetBool("Sides", Sides);
        animator.SetBool("Top", Top);
        animator.SetBool("Bottom", Bottom);

       
        
    }
    private void Poruszanie() 
    {

       
        if(Horizontal != 0 && Vertical != 0)
        {
            transform.position += new Vector3(Horizontal * (_playerStats.MovementSpeed / 2), Vertical * (_playerStats.MovementSpeed / 2),0 ) * Time.deltaTime * BlockMovement;
            DynamicMove += 10 * Time.deltaTime;
            //_audioManager.PlaySound("chodzenie");
        }
        else if(Horizontal != 0 || Vertical != 0)
        {
            transform.position += new Vector3(Horizontal * _playerStats.MovementSpeed, Vertical * _playerStats.MovementSpeed,0) * Time.deltaTime * BlockMovement;
            DynamicMove += 10 * Time.deltaTime;
            //_audioManager.PlaySound("chodzenie");
        }
        else
        {
            DynamicMove = 0;
        }

        if (Horizontal < 0)
        {
            gameObject.transform.localScale = new Vector3(-5, 5, 1);
            _skillCircle.transform.localScale = new Vector3(-Mathf.Abs(_skillCircle.transform.localScale.x), _skillCircle.transform.localScale.y, _skillCircle.transform.localScale.z);
            _familiarPlayer.transform.localScale= new Vector3(-Mathf.Abs(_familiarPlayer.transform.localScale.x),_familiarPlayer.transform.localScale.y,_familiarPlayer.transform.localScale.z);
        }
        else if(Horizontal > 0)
        {
            gameObject.transform.localScale = new Vector3(5, 5, 1);
            _skillCircle.transform.localScale = new Vector3(Mathf.Abs(_skillCircle.transform.localScale.x), _skillCircle.transform.localScale.y, _skillCircle.transform.localScale.z);
            _familiarPlayer.transform.localScale = new Vector3(Mathf.Abs(_familiarPlayer.transform.localScale.x), _familiarPlayer.transform.localScale.y, _familiarPlayer.transform.localScale.z);

        }
    }
    private void Animacja()
    {
        if ( Vertical > 0.1f)
        {
           
            Top = true;
            Sides = false;
            Bottom = false;
        }
        else if (Vertical < -0.1f)
        {
           
            Bottom = true;
            Sides = false;
            Top = false;
            
        }

        else if (Horizontal != 0 && Vertical == 0 )
        {
            Sides = true;
            Top = false;
            Bottom = false;

        }
        else
        {
            Sides = false;
            Top = false;
            Bottom = false;
        }
            
        
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //coœ z lataniem
        if ((GameObject.Find("Player").GetComponent<PlayerItems>().Fly|| GameObject.Find("Player").GetComponent<SpaceBar>().TheBibleActive) &&collision.CompareTag("HitboxMapElement")|| collision.CompareTag("Rocks"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        }
        else if(collision.CompareTag("HitboxMapElement") || collision.CompareTag("Rocks"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<Collider2D>(),false);

        }

        
        if (collision.CompareTag("central"))
        {
            CurrentlyCentral = collision.gameObject;
        }
        
    }



}
