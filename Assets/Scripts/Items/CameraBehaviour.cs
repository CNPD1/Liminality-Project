using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CameraBehaviour : MonoBehaviour
{
    public delegate void Flashed(GameObject hit);
    public event Flashed HitMonster;
    public event Flashed HitPillar;

    [SerializeField] Light flash;

    [Header("Config")]
    float flashIntensity = 20;
    float flashDecayIntensity = 10f;
    float flashDecayIncrement = 0.1f;
    float flashDecayDuration = 0.5f;

    float flashRange = 20;
    float flashCooldown = 1f;

    AudioSource sfx;

    [Header("Player")]
    [SerializeField] GameObject plrCamera;

    [Header("Enemy")]
    [SerializeField] GameObject enemy;
    [SerializeField] EnemyDetection enemyDetection;

    [Header("States")]
    bool flashing = false;

    void Awake()
    {
        sfx = GetComponent<AudioSource>();

        plrCamera = FindObjectOfType<PlayerCamera>().gameObject;

        enemy = FindObjectOfType<EnemyBehaviour>().gameObject;
        enemyDetection = FindObjectOfType<EnemyDetection>();
    }

    void Update()
    {
        if(Input.GetButtonDown("UseCamera") && !flashing)
        {
            StartCoroutine(FlashCamera());
            PillarDetection();

            if (enemyDetection.enemyDetected && 
                (enemy.transform.position - transform.position).magnitude <= flashRange)
            {
                HitMonster(enemy);
            }
        }
    }

    void PillarDetection()
    {
        RaycastHit rayHit;
        Ray ray = new Ray(plrCamera.transform.position, plrCamera.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction, Color.cyan, 100f);

        if (Physics.Raycast(ray, out rayHit, 100f, (1 << LayerMask.NameToLayer("Default"))))
        {
            if(rayHit.collider.gameObject.CompareTag("Pillar"))
            {
                HitPillar(rayHit.collider.gameObject);
            }
        }
    }

    IEnumerator FlashCamera()
    {
        sfx.Play();
        flashing = true;
        flash.intensity = flashIntensity;

        yield return new WaitForSeconds(0.1f);

        flash.intensity = flashDecayIntensity;

        while(flash.intensity > 0)
        {
            flash.intensity = Mathf.Clamp(flash.intensity - flashDecayIncrement, 0, flashIntensity);
            yield return new WaitForSeconds(flashDecayDuration/(flashDecayIntensity/flashDecayIncrement));
        }

        yield return new WaitForSeconds(flashCooldown);

        flashing = false;
    }
}
