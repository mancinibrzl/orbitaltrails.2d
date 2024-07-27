using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    //public Animator animator;

    public AudioSource somDoPulo;

    private float _currentSpeed;
    private int _playerDirection = 1;

    private Animator _currentPlayer;

    [Header("Jump Colission Check")]

    public Collider2D collider2D;
    public float disToGround;
    public float spaceToGround = .1f;
    public ParticleSystem jumpVFX;


    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.playerAnimation, transform);
        _currentPlayer.transform.localPosition = Vector3.zero;
        var destroyHelper = _currentPlayer.GetComponentInChildren<PlayerDestroyHelper>();
        destroyHelper.player = this;
        var gunBase = _currentPlayer.GetComponentInChildren<GunBase>();
        gunBase.playerSideReference = transform;

        if (collider2D != null)
        {
            disToGround = collider2D.bounds.extents.y;
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, disToGround + spaceToGround);
        var hit2D = Physics2D.Raycast(transform.position, -Vector2.up, disToGround + spaceToGround);
        return hit2D;
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }


    private void Update()
    {
        IsGrounded();
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 2;

        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 2;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position - velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            _playerDirection = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position + velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            _playerDirection = 1;

        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }



        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity += soPlayerSetup.friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity -= soPlayerSetup.friction;
        }



    }

    private Tweener tweener = null;

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
           myRigidBody.velocity = Vector2.up * soPlayerSetup.forceJump;
           myRigidBody.transform.localScale = Vector2.one;
            somDoPulo.Play();

            DOTween.Kill(myRigidBody.transform);
            if (tweener != null)
            {
                tweener.Kill();
            }

           HandleScaleJump();
           PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        //VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
        if(jumpVFX != null) jumpVFX.Play();
    }

    private void HandleScaleJump()
    {
        myRigidBody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidBody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);

        tweener = DOTween.To(ScaleXGetter, ScaleXSetter, soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);


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

    public void DestroyMe()
    {
        Destroy(gameObject);
    }




}
