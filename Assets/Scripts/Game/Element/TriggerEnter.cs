using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TriggerEnter : MonoBehaviour
{
    public TypeEnter typeEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        switch (typeEnter)
        {
            case TypeEnter.Coin:
                Values.AddCoins(Random.Range(1, 4));
                
                SoundManage.Instance.Coin();
                
                Destroy(gameObject);
                break;
            case TypeEnter.Death:
                Values.RemoveHealth();
                
                Player.Move.GoToLastPos();
                break;
            
            case TypeEnter.Key:
                SoundManage.Instance.Coin();
                
                Values.AddKey();
                
                Destroy(gameObject);
                break;
            case TypeEnter.Door:
                if (Values.KeysCount == 3)
                    GameManager.Instance.EndGame(true);
                break;
        }
    }

    public enum TypeEnter
    {
        Coin,
        Death,
        Key,
        Door
    }
}
