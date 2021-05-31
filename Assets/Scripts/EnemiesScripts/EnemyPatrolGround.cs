using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolGround : MonoBehaviour {
    // Variables :
    public float enemyPatrolSpeed;
    public float enemyPatrolDistance;
    public Transform groundDetection;
    public LayerMask layerMask;

    private bool enemyPatrolMovingRight = true;
    
    void Update() {
        transform.Translate(Vector2.right * enemyPatrolSpeed * Time.deltaTime);
        RaycastHit2D groundInformation = Physics2D.Raycast(groundDetection.position, Vector2.down, enemyPatrolDistance);
        RaycastHit2D forwardInformation = Physics2D.Raycast(groundDetection.position, transform.TransformDirection(Vector3.forward), enemyPatrolDistance, layerMask);

        // Turn the enemy :
        if (groundInformation.collider == false || forwardInformation.collider == true) {
            if (enemyPatrolMovingRight) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                enemyPatrolMovingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                enemyPatrolMovingRight = true;
            }
        }
    }
}
