using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Player
{
    public float moveSpeed = 3;

    public float jumpForce = 7;

    public ContactFilter2D groundFilter;
    
    private int _indexHorizontal = -1;
    
    private Vector2 _lastPos;

    private bool IsMove => _indexHorizontal != -1;

    private bool IsLeftMove => _indexHorizontal == 0;

    private bool IsGrounded => Rb.IsTouching(groundFilter);
    
    private Coroutine _checking;
    
    public void SetIndex(int index)
    {
        _indexHorizontal = index;

        if (!IsMove)
            return;
        
        Sr.flipX = IsLeftMove;

        if (_checking == null)
            _checking = StartCoroutine(CheckLastPost());
    }

    public void Jump()
    {
        if (!IsGrounded)
            return;
        
        Rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        
        SoundManage.Instance.Jump();
    }

    public void GoToLastPos()
    {
        Rb.position = _lastPos;
        
        _lastPos = Rb.position;
        
        StopCoroutine(_checking);
    }
    
    private void FixedUpdate()
    {
        CheckMove();
    }

    private void CheckMove()
    {
        Animator.SetBool("isMove", IsMove);
    
        Animator.SetBool("isGrounded", IsGrounded);
        
        if (!IsMove)
            return;

        var direction = IsLeftMove ? Vector2.left : Vector2.right;

        direction *= moveSpeed * Time.deltaTime;

        Rb.position += direction;
    }

    private IEnumerator CheckLastPost()
    {
        while (true)
        {
            _lastPos = Rb.position;
            
            yield return new WaitForSeconds(3);
        }
    }
}
