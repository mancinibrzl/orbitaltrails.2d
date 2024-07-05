using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;


    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;


    [Header("Animation Player")]
    public string boolRun = "Run";
    public Animator animator;
    public float playerSwipeDuration = .1f;



    private float _currentSpeed;

    private int _playerDirection = 1;
    
    
    private void Update()
    {
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speedRun;
            animator.speed = 2;

        }
        else
        {
            _currentSpeed = speed;
            animator.speed = 2;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position - velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, playerSwipeDuration);
            }
            animator.SetBool(boolRun, true);
            _playerDirection = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position + velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, playerSwipeDuration);
            }
            animator.SetBool(boolRun, true);
            _playerDirection = 1;

        }
        else
        {
            animator.SetBool(boolRun, false);
        }



        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity += friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity -= friction;
        }



    }

    private Tweener tweener = null;

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           myRigidBody.velocity = Vector2.up * forceJump;
           myRigidBody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidBody.transform);
            if (tweener != null)
            {
                tweener.Kill();
            }

           HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        myRigidBody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        //myRigidBody.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);

        tweener = DOTween.To(ScaleXGetter, ScaleXSetter, jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);


        float ScaleXGetter()
        {
            return myRigidBody.transform.localScale.x;
        }

        void ScaleXSetter(float value)
        {
            var localScale = myRigidBody.transform.localScale;
            localScale.x = value * _playerDirection;
            myRigidBody.transform.localScale = localScale;
        }

    }






}
