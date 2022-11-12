using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField]
    private Text coinTxt;

    private int collectedCoins;

    public static GameManager Instance
    {
        get
        {
            if(instance==null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
            
    }

    public GameObject CoinPrefab
    {
        get
        {
            return coinPrefab;
        }
        
    }

    public int CollectedCoins
    {
        get
        {
            return collectedCoins;
        }
        set
        {
            coinTxt.text = value.ToString();
            this.collectedCoins = value;
        }
    }
    

    //public Text CoinTxt
    //{
    //    get
    //    {
    //        return coinTxt;
    //    }
    //    set
    //    {
    //        this.coinTxt = value;
    //    }
    //}
}
