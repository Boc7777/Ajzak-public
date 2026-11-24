using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpaceBar : MonoBehaviour
{
    [SerializeField] private Sprite _contyner1,_contyner2, _contyner3, _contyner4, _contyner5, _contyner6, _fill1, _fill2, _fill3, _fill4, _fill5, _fill6;
    [SerializeField] private Image _spaceBarImage,_spaceBarChargingImage;
    public Image SpaceBarCharging;
    private float _fill;
    public int ChargesAmountMax;
    public int ChargesAmountCurrently;
    private PlayerStats _playerStats;
    private float BookOfBelialDmg;
    public bool ItemActiveOnRoom;

    public GameObject MonsterParent;
    private PlayerHealth _playerHealth;

    public bool TheBibleActive;

    

    void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        BookOfBelialDmg = 0.2f;
        _playerHealth = GameObject.Find("HitBox").GetComponent<PlayerHealth>();

    }

    void Update()
    {
        if (Input.GetKey("space")&& _spaceBarImage.sprite.name!="Empty" && ChargesAmountCurrently>=ChargesAmountMax)
        {
           SpaceBarCharging.fillAmount = 0f;
            ChargesAmountCurrently = 0;
            Items();
        } 
    }
    public void SetSpaceBar(int ChargingLevel,Sprite ItemSprite)
    {
        _spaceBarImage.sprite = ItemSprite;

        if (ChargingLevel == 1)
        {
            _spaceBarChargingImage.sprite = _contyner1;
            SpaceBarCharging.sprite = _fill1;
            _fill = 1f;
            ChargesAmountMax = 1;
            ChargesAmountCurrently = 1;
        }
        if (ChargingLevel == 2)
        {
            _spaceBarChargingImage.sprite = _contyner2;
            SpaceBarCharging.sprite = _fill2;
            _fill = 0.5f;
            ChargesAmountMax = 2;
            ChargesAmountCurrently = 2;
        }
        if (ChargingLevel == 3)
        {
            _spaceBarChargingImage.sprite = _contyner3;
            SpaceBarCharging.sprite = _fill3;
            _fill = 0.33f;
            ChargesAmountMax = 3;
            ChargesAmountCurrently = 3;


        }
        if (ChargingLevel == 4)
        {
            _spaceBarChargingImage.sprite = _contyner4;
            SpaceBarCharging.sprite = _fill4;
            _fill = 0.25f;
            ChargesAmountMax = 4;
            ChargesAmountCurrently = 4;


        }
        if (ChargingLevel == 5)
        {
            _spaceBarChargingImage.sprite = _contyner5;
            SpaceBarCharging.sprite = _fill5;
            _fill = 0.20f;
            ChargesAmountMax = 5;
            ChargesAmountCurrently = 5;


        }
        if (ChargingLevel == 6)
        {
            _spaceBarChargingImage.sprite = _contyner6;
            SpaceBarCharging.sprite = _fill6;
            _fill = 0.16f;
            ChargesAmountMax = 6;
            ChargesAmountCurrently = 6;


        }

    }

    public void Charging()
    {
        SpaceBarCharging.fillAmount += _fill;
        ChargesAmountCurrently++;
    }

    private void Items()
    {
        if(_spaceBarImage.sprite.name == "The Book of Belial")
        {
            ItemActiveOnRoom = true;
            _playerStats.Dmg += BookOfBelialDmg;
            _playerStats.NewStats();

        }
        if (_spaceBarImage.sprite.name == "TheBible")
        {
            TheBibleActive = true;
            ItemActiveOnRoom = true;


        }
        if (_spaceBarImage.sprite.name== "TheNecronomicon")
        {
            for (int childIndex = 0; childIndex < MonsterParent.transform.childCount; childIndex++)
            {
                Transform child = MonsterParent.transform.GetChild(childIndex);
                child.GetComponent<MonsterTakeDmg>().MonsterHealth -=4f;


            }
        }
        if(_spaceBarImage.sprite.name== "Book of Shadows")
        {
           _playerHealth.Immortal = true;
            StartCoroutine(Immortal(4f));
        }

        if(_spaceBarImage.sprite.name == "Guppy's_Head")
        {

            GameObject.Find("PokeDex").GetComponent<CreateFriendlyMonster>().CreateBlueFly();
        }

    }

    public void ClearSpaceBarItems()
    {

        if (_spaceBarImage.sprite.name == "The Book of Belial")
        {
            ItemActiveOnRoom = false;
            _playerStats.Dmg -= BookOfBelialDmg;
            _playerStats.NewStats();


        }
        if (_spaceBarImage.sprite.name == "TheBible")
        {
            TheBibleActive = false;

        }
    }
    
    private IEnumerator Immortal(float delay)
    {
        yield return new WaitForSeconds(delay);
        _playerHealth.Immortal = false;

    }
}
