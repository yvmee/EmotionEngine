using System;
using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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

    private bool _interacting;
    public UITextController uiTextController;

    public GameObject pauseMenu;

    private EmotionModel _emotionModel;
    public DiscreteEmotion e;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

        _emotionModel = FindObjectOfType<EmotionModel>();
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
        if (_interacting) return;
        _animator.SetTrigger(SwordAttackAn);
    }

    void OnSendEmotionEvent()
    {
        Debug.Log("Pressed E");
        //emotionModel.RaiseSoftEmotionEvent(e);
        EmotionModel.EmotionPulseSend.Invoke(e, false);
    }

    void OnInteract()
    {
        if (_interacting)
        {
            uiTextController.NextNode();
            return;
        }
        Debug.Log("Raycasting");
        
        var layerMask = LayerMask.GetMask("Interaction");

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.up, 0.5f,
            layerMask);

        if (hit.collider != null)
        {
            Debug.Log("Hit something: " + hit.collider.gameObject.name);
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null) interactable.Interact(gameObject);
        }
        else
        {
            var sideDirection = Vector2.right;
            if (_renderer.flipX) sideDirection = Vector2.left;
            
            hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), sideDirection, 0.5f,
                layerMask);
            if (hit.collider != null)
            {
                Debug.Log("Hit something: " + hit.collider.gameObject.name);
                var interactable = hit.collider.GetComponentInParent<IInteractable>();
                if (interactable != null) interactable.Interact(gameObject);
            }
        }
    }

    public void OnPause()
    {
        if (pauseMenu == null) return;
        if (Menu.Paused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Menu.Paused = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Menu.Paused = true;
        }
    }

    public void SwordAttack()
    {

        LockMovement();

        if (_renderer.flipX)
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

    private void LockMovement()
    {
        Debug.Log("Locking Movement");
        _canMove = false;
    }

    private void UnlockMovement()
    {
        Debug.Log("Unlocking Movement");
        _canMove = true;
    }

    public void LockForInteraction()
    {
        _interacting = true;
        LockMovement();
    }

    public void UnlockForInteraction()
    {
        _interacting = false;
        UnlockMovement();
    }

}
