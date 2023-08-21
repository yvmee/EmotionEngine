using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Animal : MonoBehaviour
{
    public enum AnimalState
    {
        Normal,
        Running,
        Excited
    }
    [SerializeField] private AnimalState currentState = AnimalState.Normal;

    private DialogueInteraction _dialogueInteraction;
    
    // Animations
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private static readonly int Walking = Animator.StringToHash("walking");
    private static readonly int Running = Animator.StringToHash("running");
    
    // Physics Movement
    [Header("Physics Settings")]
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    
    private List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    private const float Offset = 0.05f;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _col;
    private GameObject _player;
    
    // Walking Parameters
    [Header("Walking Settings")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float walkingSpeed = 1f;
    [SerializeField] private float waitTime = 5f;
    
    private int _waypointIndex;
    private bool _moving = true;
    
    // Running Parameters
    [Header("Running Settings")]
    [SerializeField] private float runningSpeed = 0.2f;
    [SerializeField] private float runningDistance = 3.5f;
    [SerializeField] private float maxRunningTime = 6f;
    
    private Vector2 _runAwayWaypoint;
    private bool _playerNear = true;
    private bool _running;


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _dialogueInteraction = GetComponent<DialogueInteraction>();

        _runAwayWaypoint = gameObject.transform.position;
        _player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        if (_dialogueInteraction.inDialogue)
        {
            _animator.SetBool(Walking, false);
            _animator.SetBool(Running, false);
            return;
        }
        
        switch (currentState)
        {
            case AnimalState.Normal:
                WalkAround();
                break;
            case AnimalState.Running:
                if (_running) RunAway();
                else WalkAround();
                break;
            case AnimalState.Excited:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    private void Move(Vector2 dir, float speed)
    {
        if (dir.x > 0)
        {
            _spriteRenderer.flipX = false;
        } else if (dir.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        int count = _col.Cast(
            dir,
            movementFilter,
            _castCollisions, 
            speed * Time.fixedDeltaTime + collisionOffset);

        if (count != 0) return;
        _rigidbody2D.MovePosition(_rigidbody2D.position + dir * (speed * Time.fixedDeltaTime));
    }

    private void WalkAround()
    {
        if (!_moving) return;
        _animator.SetBool(Walking, true);
        
        Vector2 waypoint = waypoints[_waypointIndex].position;
        Vector2 direction = waypoint - (Vector2)transform.position;
        direction.Normalize();
        Move(direction, walkingSpeed);

        if (Vector3.Distance(waypoint, transform.position) < Offset)
        {
            _moving = false;
            _animator.SetBool(Walking, false);
            _waypointIndex = (_waypointIndex + 1) % waypoints.Length;
            StartCoroutine(Stay());
        }
    }

    private IEnumerator Stay()
    {
        _animator.SetBool(Walking, false);
        _animator.SetBool(Running, false);
        yield return new WaitForSeconds(waitTime);
        _moving = true;
    }

    private void RunAway()
    {
        _animator.SetBool(Running, true);
        var direction = _runAwayWaypoint - (Vector2) transform.position;
        direction.Normalize();
        Move(direction, runningSpeed);

        if (Vector3.Distance(_runAwayWaypoint, transform.position) < Offset)
        {
            if (_playerNear)
            {
                SetRunAwayWaypoint(_player.transform.position);
                StopAllCoroutines();
                StartCoroutine(RunningThreshold());
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(Stay());
                _running = false;
            }
        }
    }
    
    private IEnumerator RunningThreshold()
    {
        yield return new WaitForSeconds(maxRunningTime);
        _animator.SetBool(Running, false);
        _running = false;
    }

    public void PlayerEnter()
    {
        _playerNear = true;
        _running = true;
        SetRunAwayWaypoint(_player.transform.position);
        StopCoroutine(RunningThreshold());
        StartCoroutine(RunningThreshold());
    }

    public void PlayerExit()
    {
        _playerNear = false;
    }

    private void SetRunAwayWaypoint(Vector3 playerPos)
    {
        var position = gameObject.transform.position;
        Vector2 dir = position - playerPos;
        dir.Normalize();
        _runAwayWaypoint = (Vector2) position + dir * runningDistance;
    }

    public void SetAnimalState(AnimalState state)
    {
        currentState = state;
    }
}
