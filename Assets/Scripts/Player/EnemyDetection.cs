using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] GameObject enemy;
    [SerializeField] LayerMask ignoreRaycastLayer;
    public bool enemyDetected;

    void Awake()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        Vector3 relativeNormalizedPos = (enemy.transform.position - cameraTransform.position).normalized;
        float enemyAngle = Vector3.Dot(relativeNormalizedPos, cameraTransform.transform.TransformDirection(Vector3.forward));

        Debug.DrawLine(transform.position, enemy.transform.position, Color.red);

        if (enemyAngle < 0.73 || (enemy.transform.position - transform.position).magnitude > 100)
        {
            enemyDetected = false;
            return;
        }

        RaycastHit info;

        if(!Physics.Linecast(transform.position, enemy.transform.position, out info, (1 << LayerMask.NameToLayer("Default")))) //If nothing is between the player and the enemy
        {
            enemyDetected = true;
        }
    }
}
