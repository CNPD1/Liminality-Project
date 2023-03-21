using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnemyBehaviour;

public class DisplayMonsterState : MonoBehaviour
{
    EnemyBehaviour monsterBehaviour;
    Text text;

    bool debounce = false;

    void Awake()
    {
        monsterBehaviour = FindObjectOfType<EnemyBehaviour>();
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        monsterBehaviour.OnDeath += StopUpdating;
    }

    void Update()
    {
        if(debounce == true)
        {
            text.text = "-------";
            return;
        }

        switch (monsterBehaviour.state)
        {
            case EnemyState.Hidden:
                text.text = "It is hidden";
                break;
            case EnemyState.Hiding:
                text.text = "It is hiding";
                break;
            case EnemyState.Follow:
                text.text = "It is following";
                break;
            case EnemyState.Chase:
                text.text = "It is chasing";
                break;
        }
    }

    void StopUpdating()
    {
        debounce = true;
    }
}
