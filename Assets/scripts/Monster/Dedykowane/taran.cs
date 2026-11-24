using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class taran : MonoBehaviour
{
    [SerializeField] private ParticleSystem _taranParticle;

    private NavMeshAgent _NavMeshAgent;
    private Vector3 _kordyGracza;

    public Animator _animator;

    private bool _isMoving = true;

    private Vector3 previousPosition;
    private Vector3 currentDirection;

    [SerializeField] private float _sizeMonster;

    private RaycastHit2D _hit;
   

    private Vector3 raycastDir;
    private float _distance;

    Rigidbody2D rb;

    private bool _taran = false;
    private bool _podczasTaranu = false;
    private bool _uderzenieCooldown = false;

    private bool _zatrzymanieChodzenia = false;
    private bool _blokowaniaDublowaniaTaranu = false;
    void Start()
    {
        _NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Animacja();

        _kordyGracza = GameObject.FindGameObjectWithTag("Player").transform.position;

        raycastDir = _kordyGracza - transform.position;
        _distance = Vector2.Distance((Vector2)_kordyGracza, (Vector2)gameObject.transform.position);

        _hit = Physics2D.Raycast(transform.position, raycastDir, Mathf.Infinity, 1 << 6 | 1 << 9);


      

        //sprawdzanie czy moze przygotowac sie do taranu
        if (_hit.collider.name == "HitBox" && _distance < 5f)
        {
            _isMoving = false;
            StopCoroutine(Taran(0));
        }

        //sprawdzanie czy player jest w zasiegu widzenia i nie jest za daleko
        else if (_distance > 5f || _hit.collider.name != "HitBox")
        {
            _isMoving = true;
        }

        

        //chodzenie
        if (_isMoving == true && _zatrzymanieChodzenia == false)
        {
            
            _NavMeshAgent.isStopped = false;
            rb.velocity = Vector2.zero;

            _podczasTaranu = false;
            _taran = false;

            ColorChanger(Color.white);
            Poruszanie();
        }

        //stanie w miejscu 
        else if (_isMoving == false && _zatrzymanieChodzenia == false)
        {

            //Debug.Log("stanie");

            _NavMeshAgent.isStopped = true;

            ColorChanger(Color.red);

            if (_taran == false)
            {
                //Debug.Log("proba taranu za 1s");
                StartCoroutine(Taran(1f));
            }

        }
        
        //zatrzymuje chodzenie 
        else if (_zatrzymanieChodzenia == true)
        {
            _NavMeshAgent.isStopped = true;
        }
    }


    private void Animacja()
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

    private void ColorChanger(Color _color)
    {
        for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
        {
            Transform child = gameObject.transform.GetChild(childIndex);
            child.GetComponent<Renderer>().material.color = _color;
        }
    }

    private void Poruszanie()
    {
        _NavMeshAgent.SetDestination(_kordyGracza);
    }

    IEnumerator Taran(float _delay)
    {
        _taran = true;

        yield return new WaitForSeconds(_delay);

        if (_isMoving == false && _podczasTaranu != true && _uderzenieCooldown != true )
        {

            if (_hit.collider.name == "HitBox")
            {
                _podczasTaranu = true; 


                _NavMeshAgent.isStopped = true;
                rb.velocity = raycastDir.normalized * 9;

            }

        }

    }

    IEnumerator Ogluszenie(float _delay)
    {
        _zatrzymanieChodzenia = true;
        ColorChanger(Color.green);

        //Debug.Log("start ogluszenia");

        yield return new WaitForSeconds(_delay/2);
        _zatrzymanieChodzenia = false;
        _taran = false;
        yield return new WaitForSeconds(_delay/2);
        _uderzenieCooldown = false;
        
       
        
        //gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_podczasTaranu == true)
        {
            if ((collision.CompareTag("HitboxMapElement") || collision.CompareTag("wall")) && _uderzenieCooldown == false)
            {
                _uderzenieCooldown = true;
                StartCoroutine(Ogluszenie(3));

                rb.velocity = Vector2.zero;

                //Debug.Log("kraksa");
                Instantiate(_taranParticle, transform.position, Quaternion.identity, transform.parent);
                
            }
        }


        //if (collision.CompareTag("HitboxMapElement") || collision.CompareTag("wall") || collision.CompareTag("wall") && _podczasTaranu == true)
        //{
        //    Debug.Log("kolor");
        //    ColorChanger(Color.black);
        //}
    }




    //    _taran = true;
    //    //gameObject.GetComponent<Collider2D>().enabled = false;
    //    yield return new WaitForSeconds(_delay);

    //    if (_hit.collider.name == "HitBox")
    //    {
    //        //Debug.Log("faktyczny taran");
    //        _NavMeshAgent.isStopped = true;
    //        rb.velocity = raycastDir.normalized * 6;

    //        _zatrzymanieChodzenia = true;
    //        _podczasTaranu = true;

    //        StartCoroutine(ColliderOn(0.8f));
    //    }
    //    else
    //    {
    //        _podczasTaranu = false;
    //    }



    //}























    //        if (_isMoving == false)
    //        {
    //            if (_taran == false && _breakTaran == false)
    //            {
    //                ColorChanger(Color.red);
    //                _taran = true;
    //                rb.velocity = Vector2.zero;
    //                StartCoroutine(Taran(2f));

    //            }
    //        }
    //        else if(_isMoving == true)
    //        {
    //            ColorChanger(Color.white);
    //            rb.velocity = Vector2.zero;
    //            Poruszanie();
    //        }


    //}
    //    private void OnTriggerStay2D(Collider2D collision)
    //    {

    //        Debug.Log(collision.tag);
    //        if ( (collision.CompareTag("HitboxMapElement") || collision.CompareTag("wall") ) && _podczasTaranu == true)
    //        {
    //            _breakTaran = true;
    //            _podczasTaranu = false;

    //            rb.velocity = Vector2.zero;
    //           

    //            StartCoroutine(StandUp(2f));

    //        }
    //    }

    //    IEnumerator StandUp(float _delay)
    //    {
    //        Debug.Log("wstawanie");
    //        yield return new WaitForSeconds(_delay);
    //        _taran = false;
    //        _breakTaran = false;
    //        _NavMeshAgent.isStopped = false;
    //    }

}
