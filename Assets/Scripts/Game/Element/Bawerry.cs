using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bawerry : MonoBehaviour
{
    public Sprite strawberrySprite;

    public Sprite plantedSprite;
    
    private SpriteRenderer Sr => GetComponent<SpriteRenderer>();
    
    private bool _isTakenToPlant;

    private bool _isPlanted;

    public bool IsTakenToPlant
    {
        get => _isTakenToPlant;
        set
        {
            _isTakenToPlant = value;

            Sr.sprite = value ? null : strawberrySprite;
            
            if (value)
                Values.AddBarrie();
        }
    }

    public bool IsPlanted
    {
        get => _isPlanted;

        set
        {
            _isPlanted = value;

            Sr.sprite = value ? plantedSprite : Sr.sprite;
            
           
        }
    }

    public void TryToPlant()
    {
        if (!IsTakenToPlant || IsPlanted)
            return;
        
        IsPlanted = true;
        
        SoundManage.Instance.PlantDo(false);
    }

    public void TryToTakePlant()
    {
        if (IsPlanted || IsTakenToPlant)
            return;

        SoundManage.Instance.PlantDo(true);
        
        IsTakenToPlant = true;
        
        StartCoroutine(WaitNotPlant());
    }
    
    private void Start()
    {
        IsTakenToPlant = false;

        IsPlanted = false;
    }

    private IEnumerator WaitNotPlant()
    {
        yield return new WaitForSeconds(5);
        
        if (!IsPlanted)
            GameManager.Instance.EndGame(false);
    }
}
