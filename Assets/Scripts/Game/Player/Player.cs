using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected SpriteRenderer Sr => GetComponent<SpriteRenderer>();

    protected Rigidbody2D Rb => GetComponent<Rigidbody2D>();

    protected Animator Animator => GetComponent<Animator>();

    private static Player _instance;

    public static Vector2 Position => _instance.Rb.position;

    public static PlayerMove Move => _instance.GetComponent<PlayerMove>();
    
    private void Awake()
    {
        _instance = this;
    }
}
