using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlanter : Player
{
    public Sprite[] interactSprites;

    public Image nowInteractImage;

    private int _nowInteractIndex;

    private static List<Bawerry> AllPlantPlaces => FindObjectsOfType<Bawerry>().ToList();

    public static int NowCountPlants => AllPlantPlaces.Count;
    
    private int NowInteractIndex
    {
        get => _nowInteractIndex;
        set
        {
            if (value > interactSprites.Length - 1)
                value = 0;
            
            _nowInteractIndex = value;

            nowInteractImage.sprite = interactSprites[value];
        }
    }

    public void SwitchInteract()
    {
        NowInteractIndex++;
    }

    public void OnPressInteract()
    {
        if (AllPlantPlaces.Any(x => Vector2.Distance(Position, x.transform.position) < 1))
        {
            var mostNear = AllPlantPlaces.Find(x =>
                Vector2.Distance(Position, x.transform.position) <=
                AllPlantPlaces.Select(y => Vector2.Distance(Position, y.transform.position)).Min());
            
            if (NowInteractIndex == 0)
                mostNear.TryToTakePlant();
            else
                mostNear.TryToPlant();
        }
    }
}
