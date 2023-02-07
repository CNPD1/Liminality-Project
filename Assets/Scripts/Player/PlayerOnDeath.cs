using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnDeath : MonoBehaviour
{
    [SerializeField] EnemyBehaviour enemyBehaviour;

    void Start()
    {
        enemyBehaviour.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
