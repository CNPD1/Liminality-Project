using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    public delegate void PlayerDied();
    public event PlayerDied OnDeath;

    public enum EnemyState { Hiding, Hidden, Follow, Chase };

    [Header("Player")]
    [SerializeField] GameObject player;
    [SerializeField] EnemyDetection enemyDetection;
    [SerializeField] CameraBehaviour cameraBehaviour;

    [Header("Textures")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite idleTexture;
    [SerializeField] Sprite chaseTexture;

    [Header("AI")]
    public EnemyState state;
    [SerializeField] CapsuleCollider detectionHitbox;
    [SerializeField] float chaseRange;
    [SerializeField] float baseSpeed;
    [SerializeField] float chaseSpeed;
    [SerializeField] float hidingTime;

    NavMeshAgent navAgent;

    float timeHidden = 0;

    [Header("Audio")]
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource plrDeathSound;
    [SerializeField] AudioSource triggeredSound;
    [SerializeField] AudioSource chaseSound;

    void Awake()
    {
        navAgent= GetComponent<NavMeshAgent>();
        state = EnemyState.Hidden;
        navAgent.speed = 0;
        detectionHitbox.radius = chaseRange * (1/transform.localScale.x);
        transform.position = player.transform.position + (Vector3.back * 300);

        navAgent.radius = 0.01f;

        cameraBehaviour = FindObjectOfType<CameraBehaviour>();

        cameraBehaviour.HitMonster += EnemyDeath;
    }

    void Update()
    {
        if(!player.activeSelf) { return; }

        StateHandler();

        LookAtPlayer();

        if(state != EnemyState.Hiding)
        {
            navAgent.destination = GetPlayerLocation();
        }

        if(enemyDetection.enemyDetected)
        {
            if(state != EnemyState.Chase &&
                state != EnemyState.Hiding)
            {
                BeginChase();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (state == EnemyState.Hiding ||
            state == EnemyState.Chase)
        { return; }

        if (other.CompareTag("Player"))
        {
            BeginChase();
        }
    }

    public void KillPlayer()
    {
        state = EnemyState.Hiding;
        navAgent.enabled = false;

        chaseSound.Stop();
        triggeredSound.Stop();
        plrDeathSound.Play();

        StartCoroutine("GoBackToMenu");

        OnDeath();
    }

    public void WinGame()
    {
        navAgent.enabled = false;

        chaseSound.Stop();
        triggeredSound.Stop();
        plrDeathSound.Play();

        StartCoroutine("Win");

        OnDeath();
    }    

    void EnemyDeath(GameObject enemy)
    {
        state = EnemyState.Hiding;
        spriteRenderer.enabled = false;
        triggeredSound.Stop();
        chaseSound.Stop();
        deathSound.Play();

        GetNewPosition();
    }

    void GetNewPosition()
    {
        Vector2 randVector2 = Random.insideUnitCircle.normalized;
        Vector3 dir = new Vector3(randVector2.x, 0, randVector2.y);
        float distance = Random.value * 100 + 100;

        Vector3 randPos = transform.position + (dir * distance);

        NavMeshHit navMeshHit;

        NavMesh.SamplePosition(randPos, out navMeshHit, 500f, NavMesh.AllAreas);

        navAgent.destination = navMeshHit.position;
        navAgent.speed = 20;
    }

    Vector3 GetPlayerLocation()
    {
        NavMeshHit navHit;

        NavMesh.SamplePosition(player.transform.position, out navHit, 10f, NavMesh.AllAreas);

        return navHit.position;
    }

    void StateHandler()
    {
        switch (state)
        {
            case EnemyState.Hidden:
                timeHidden += Time.deltaTime;

                if(timeHidden >= hidingTime)
                {
                    state = EnemyState.Follow;
                    timeHidden = 0;
                    navAgent.ResetPath();
                }

                break;
            case EnemyState.Hiding:
                if((transform.position - navAgent.destination).magnitude < 5)
                {
                    state = EnemyState.Hidden;
                    spriteRenderer.enabled = true;
                    spriteRenderer.sprite = idleTexture;
                    navAgent.speed = 0;
                }

                break;
            case EnemyState.Follow:
                navAgent.speed = baseSpeed;
                spriteRenderer.sprite = idleTexture;
                break;
            case EnemyState.Chase:
                navAgent.speed = chaseSpeed;
                spriteRenderer.sprite = chaseTexture;
                timeHidden = 0;
                break;
        }
    }

    void LookAtPlayer()
    {
        transform.LookAt(new Vector3(
            player.transform.position.x,
            transform.position.y,
            player.transform.position.z)
        );
    }

    void BeginChase()
    {
        state = EnemyState.Chase;
        StartCoroutine("PlayChase");
    }

    IEnumerator PlayChase()
    {
        triggeredSound.Play();
        yield return new WaitForSeconds(triggeredSound.clip.length-0.1f);

        if(state == EnemyState.Chase)
        {
            chaseSound.Play();
        }
    }
    IEnumerator GoBackToMenu()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(0);
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(2);
    }
}
