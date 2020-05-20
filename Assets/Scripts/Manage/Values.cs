using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Values : MonoBehaviour
{
    public Text timerText;
    
    public Text nowBarrieText;

    public Text nowCoinsText;

    public Text bestCoinsText;

    public GameObject[] hearts;
    
    public UnityEvent onGotAllStarBarrie;
    
    
    private static Values _instance;

    private static int _secondsLost;

    private static int _barrieNowCount;

    private static int _barrieMaxCount;

    private static int _coinsCount;
    
    private static int _healthCount;

    private static int SecondsLost
    {
        get => _secondsLost;
        set
        {
            _secondsLost = value;
            
            var minutes = Mathf.FloorToInt(_secondsLost / 60f);

            var seconds = _secondsLost - minutes * 60;

            var isAddSeconds = seconds > 9 ? "" : "0";
            var isAddMinutes = minutes > 9 ? "" : "0";

            _instance.timerText.text = isAddMinutes + minutes + ":" + isAddSeconds + seconds;
        }
    }

    private static int BarrieNowCount
    {
        get => _barrieNowCount;
        set
        {
            _barrieNowCount = value;

            _instance.nowBarrieText.text = $"{value}/{_barrieMaxCount}";
            
            if (value == _barrieMaxCount)
                _instance.onGotAllStarBarrie.Invoke();
        }
    }

    private static int CoinsCount
    {
        get => _coinsCount;
        set
        {
            _coinsCount = value;

            _instance.nowCoinsText.text = $"x{value}";

            if (value > BestCoinsCount)
                BestCoinsCount = value;
        }
    }

    private static int HealthCount
    {
        get => _healthCount;
        set
        {
            _healthCount = value;

            var hearts = _instance.hearts;
            
            for (var i = 0; i < hearts.Length; i++)
                hearts[i].SetActive(i < value);
            
            if (value <= 0)
                GameManager.Instance.EndGame(false);
        }
    }

    private static int BestCoinsCount
    {
        get => PlayerPrefs.GetInt("best");

        set
        {
            PlayerPrefs.SetInt("best", value);

            _instance.bestCoinsText.text = $"BEST COINS:\n{value}";
        }
    }
    
    public static int KeysCount { get; private set; }

    public static void StartTimer(int seconds)
    {
        SecondsLost = seconds;

        _instance.StartCoroutine(Timing());
    }

    public static void AddBarrie()
    {
        BarrieNowCount++;
    }

    public static void ResetBarrie(int maxCount)
    {
        _barrieMaxCount = maxCount;

        BarrieNowCount = 0;
    }

    public static void AddCoins(int value)
    {
        CoinsCount += value;
    }

    public static void AddKey()
    {
        KeysCount++;
    }
    
    public static void RemoveHealth()
    {
        HealthCount--;
    }

    [ContextMenu("Test")]
    private void Health()
    {
        RemoveHealth();
    }
    
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        CoinsCount = 0;

        HealthCount = 3;

        BestCoinsCount = BestCoinsCount;
    }

    private static IEnumerator Timing()
    {
        while (SecondsLost > 0)
        {
            yield return new WaitForSeconds(1);
            
            SecondsLost--;
        }
        
        GameManager.Instance.EndGame(false);
    }
}
