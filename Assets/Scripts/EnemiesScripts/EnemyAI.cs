using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour {
    public float searchRange = 50f;
    public float pathUpdateSeconds = 0.5f;

    public float enemyMoveSpeed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float enemyJumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    // Check isGrounded :
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;

    private Transform target;
    private Path path;
    private int currentWaypoint = 0;
    private Animator _animator;
    private Seeker seeker;
    private Rigidbody2D _rigidbody;

    public void Start() {
        target = GameObject.FindWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    void Update() {
        // Run animation :
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if (distanceToPlayer < searchRange) {
            _animator.SetBool("isRun", true);
        } else {
            _animator.SetBool("isRun", false);
        }
    }

    private void FixedUpdate() {
        if (TargetInDistance() && followEnabled) {
            PathFollow();
        }
    }

    private void UpdatePath() {
        if (followEnabled && TargetInDistance() && seeker.IsDone()) {
            seeker.StartPath(_rigidbody.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow() {
        if (path == null) {
            return;
        }

        // Reached end of path :
        if (currentWaypoint >= path.vectorPath.Count) {
            return;
        }

        // See if colliding with anything :
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        
        // Direction Calculation :
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - _rigidbody.position).normalized;
        Vector2 force = direction * enemyMoveSpeed * Time.deltaTime;

        // Jump :
        if (jumpEnabled && isGrounded) {
            if (direction.y > jumpNodeHeightRequirement) {
                _rigidbody.AddForce(Vector2.up * enemyMoveSpeed * enemyJumpModifier);
            }
        }

        // Movement :
        _rigidbody.velocity = new Vector2(direction.x * enemyMoveSpeed * Time.deltaTime, _rigidbody.velocity.y);

        // Next Waypoint :
        float distance = Vector2.Distance(_rigidbody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }

        if (directionLookEnabled) {
            if (_rigidbody.velocity.x > 0.01f) {
                transform.eulerAngles = new Vector3(0, 0, 0);
            } else if (_rigidbody.velocity.x < -0.01f) {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
        }
    }

    private bool TargetInDistance() {
        return Vector2.Distance(transform.position, target.transform.position) < searchRange;
    }

    private void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }
}