using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundManage : MonoBehaviour
{
    private AudioSource SourceSound => GetComponent<AudioSource>();

    private AudioSource SourceMusic => transform.GetChild(0).GetComponent<AudioSource>();

    public Button[] buttons;
    
    public AudioClip coin;

    public AudioClip jump;

    public AudioClip gameOver;

    public AudioClip win;
    
    public AudioClip click;

    public AudioClip takePlant;

    public AudioClip plant;

    public static SoundManage Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Coin()
    {
        SourceSound.PlayOneShot(coin);
    }

    public void Jump()
    {
        SourceSound.PlayOneShot(jump);
    }

    public void GameOver(bool isWin)
    {
        SourceSound.PlayOneShot(isWin ? win : gameOver);
    }

    public void Click()
    {
        SourceSound.PlayOneShot(click);
    }

    public void PlantDo(bool isTake)
    {
        SourceSound.PlayOneShot(isTake ? takePlant : plant);
    }
    

    public Image[] interactsSound;
    
    public Sprite activeSound;
    
    public Sprite inActiveSound;
    
    

    public Slider[] slidersMusic;


    private void InteractSound()
    {
        VolumeSound = VolumeSound == 0 ? 1 : 0;
    }

    private void ChangeVolumeMusic(float value)
    {
        VolumeMusic = value;
    }
    
    private int VolumeSound
    {
        get => PlayerPrefs.GetInt("sound");

        set
        {
            if (!PlayerPrefs.HasKey("sound"))
                value = 1;
            
            PlayerPrefs.SetInt("sound", value);

            SourceSound.volume = value;

            foreach (var i in interactsSound)
            {
                i.sprite = value == 1 ? activeSound : inActiveSound;
            }
        }
    }

    private float VolumeMusic
    {
        get => PlayerPrefs.GetFloat("music");

        set
        {
            if (!PlayerPrefs.HasKey("music"))
                value = 1;
            
            PlayerPrefs.SetFloat("music", value);

            SourceMusic.volume = value;

            foreach (var s in slidersMusic)
            {
                s.value = value;
            }
        }
    }

    private void Start()
    {
        foreach (var i in interactsSound)
        {
            i.GetComponent<Button>().onClick.AddListener(InteractSound);
        }

        buttons.ToList().ForEach(x => x.onClick.AddListener(Click));
        
        foreach (var s in slidersMusic)
        {
            s.onValueChanged.AddListener(ChangeVolumeMusic);
        }

        VolumeMusic = VolumeMusic;

        VolumeSound = VolumeSound;
    }
}
