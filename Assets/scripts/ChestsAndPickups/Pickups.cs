using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickups : MonoBehaviour
{
    //Texty do edycji
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI BombsText;
    public TextMeshProUGUI KeysText;
    //Realne zmienne
    public int CoinsAmount;
    public int BombsAmount;
    public int KeysAmount;
    //zmieniona skrzynka
    [SerializeField] private Sprite _openedChest;
    

    private SpaceBar _spacebar;

    private void Start()
    {
        CoinsAmount = 0;
        BombsAmount = 0;
        KeysAmount = 0;
        _spacebar = GameObject.Find("Player").GetComponent<SpaceBar>();
    }
    private void Update()
    {
        KeysText.text = KeysAmount.ToString();
        CoinsText.text = CoinsAmount.ToString();
        BombsText.text = BombsAmount.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Key"))
        {
            if(KeysAmount <99)
                KeysAmount++;

            
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Coin"))
        {
            if(CoinsAmount < 99)
                CoinsAmount++;

            
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Bombs"))
        {
            if(BombsAmount < 99)
                BombsAmount++;

            
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("LockedChest") && KeysAmount >= 1)  
        {
            KeysAmount--;
            
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _openedChest;
            collision.gameObject.transform.tag = "OpenedChest";
            collision.gameObject.GetComponent<ChestDrop>().ChestDroprate();
        }

        if (collision.CompareTag("LockedMiniChest") && KeysAmount >= 1)
        {
            KeysAmount--;
            
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _openedChest;
            collision.gameObject.transform.tag = "OpenedChest";
            collision.gameObject.GetComponent<ChestDrop>().ChestDroprate();
        }



        if (collision.CompareTag("Battery")&&_spacebar.ChargesAmountCurrently<_spacebar.ChargesAmountMax)
        {
            _spacebar.ChargesAmountCurrently = _spacebar.ChargesAmountMax;
            _spacebar.SpaceBarCharging.fillAmount = 1f;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("5Coins"))
        {
            CoinsAmount += 5;
            if(CoinsAmount > 99)
            {
                CoinsAmount = 99;
            }
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("10Coins"))
        {
            CoinsAmount += 10;
            if (CoinsAmount > 99)
            {
                CoinsAmount = 99;
            }
            Destroy(collision.gameObject);
        }

    }
    
}
