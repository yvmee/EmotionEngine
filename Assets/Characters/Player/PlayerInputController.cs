using System;
using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    
    private Vector2 _movementInput;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();

    private bool _canMove = true;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int SwordAttackAn = Animator.StringToHash("swordAttack");

    private EmotionModel emotionModel;
    public DiscreteEmotion e;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

        emotionModel = FindObjectOfType<EmotionModel>();
    }

    private void FixedUpdate()
    {
        if (!_canMove)
            return;
        
        if (_movementInput != Vector2.zero)
        {
            bool success = TryMove(_movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(_movementInput.x, 0));
            }
            if (!success)
            {
                success = TryMove(new Vector2(0, _movementInput.y));
            }
            
            _animator.SetBool(IsMoving, success);
        }
        else
        {
            _animator.SetBool(IsMoving, false);
        }
        
        // Set flip direction
        if (_movementInput.x < 0)
        {
            _renderer.flipX = true;
        }
            
        else if (_movementInput.x > 0)
        {
            _renderer.flipX = false;
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return false;
        
        int count = _rb.Cast(
            _movementInput,
            movementFilter,
            _castCollisions, 
            moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count != 0) return false;
        
        _rb.MovePosition(_rb.position + _movementInput * (moveSpeed * Time.fixedDeltaTime));
        return true;
    }

    void OnMove(InputValue movementValue)
    {
        _movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        _animator.SetTrigger(SwordAttackAn);
    }

    void OnSendEmotionEvent()
    {
        Debug.Log("Pressed E");
        emotionModel.RaiseSoftEmotionEvent(e);
    }

    public void SwordAttack()
    {
        LockMovement();

        if (_renderer.flipX == true)
        {
            swordAttack.AttackLeft();
        } else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        _canMove = false;
    }

    public void UnlockMovement()
    {
        _canMove = true;
    }

}
