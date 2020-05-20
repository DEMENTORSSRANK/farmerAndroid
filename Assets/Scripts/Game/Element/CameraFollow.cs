using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 offset;

    public float modiferSpeed = 2;
    
    private void LateUpdate()
    {
        var playerPos = (Vector3) Player.Position;

        var myPos = transform.position;

        playerPos += (Vector3) offset;
        
        playerPos.z = myPos.z;

        var distance = Vector2.Distance(myPos, playerPos);

        var speed = distance * Time.deltaTime * modiferSpeed;

        var toMovePos = Vector3.MoveTowards(myPos, playerPos, speed);

        transform.position = toMovePos;
    }
}
