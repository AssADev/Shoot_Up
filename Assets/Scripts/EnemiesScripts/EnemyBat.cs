using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBat : MonoBehaviour {
    // Variables :
    public float searchRange = 7.5f;
    public LayerMask playerLayer;

    private IAstarAI astarAI;
    private AIDestinationSetter destinationSetter;
    private Transform target = null;
    
    void Start() {
        astarAI = GetComponent<IAstarAI>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void Update() {
        // FlipX :
        if (astarAI.velocity.x >= 0.01f) {
            transform.eulerAngles = new Vector3(0, -180, 0);
        } else if (astarAI.velocity.y <= -0.01f) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void UpdatePath() {
        if (target == null) {
            Collider2D[] collision = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), searchRange, playerLayer);
            if (collision.Length > 0) {
                target = collision[0].transform;
                destinationSetter.target = target;
                astarAI.isStopped = false;
            }            
        } else {
            float distance = (transform.position - target.position).magnitude;
            if (distance >= searchRange) {
                target = null;
                destinationSetter.target = null;
                astarAI.isStopped = true;
            }
        }
    }
}
