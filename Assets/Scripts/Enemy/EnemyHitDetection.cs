using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField] EnemyBehaviour enemyBehaviour;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && enemyBehaviour.state == EnemyBehaviour.EnemyState.Chase)
        {
            enemyBehaviour.KillPlayer();
        }
    }
}
