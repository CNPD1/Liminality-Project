using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CompassBehaviour : MonoBehaviour
{
    GameObject player;
    CameraBehaviour cameraBehaviour;
    EnemyBehaviour enemyBehaviour;

    [SerializeField] GameObject pointDir;
    [SerializeField] GameObject compassHand;

    List<GameObject> pillars;

    Transform pointLocation;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        cameraBehaviour = FindObjectOfType<CameraBehaviour>();
        enemyBehaviour = FindObjectOfType<EnemyBehaviour>();

        pointLocation = GameObject.Find("Thing").transform;

        pillars = GameObject.FindGameObjectsWithTag("Pillar").ToList();

        cameraBehaviour.HitPillar += RemovePillar;
    }

    void Update()
    {
        GameObject closestPillar = GetClosestPillar();

        if(closestPillar is null) { return; }

        pointLocation = closestPillar.transform;

        pointDir.transform.LookAt(pointLocation);

        Vector3 pointDirVector = new Vector3(pointDir.transform.forward.x, 0, pointDir.transform.forward.z);

        compassHand.transform.localRotation = Quaternion.FromToRotation(player.transform.forward, pointDirVector);
    }

    void RemovePillar(GameObject pillar)
    {
        pillars.Remove(pillar);

        Destroy(pillar);

        if(pillars.Count == 0)
        {
            enemyBehaviour.WinGame();
        }
    }

    GameObject GetClosestPillar()
    {
        GameObject closestPillar = null;

        foreach(GameObject pillar in pillars)
        {
            if(closestPillar is null)
            {
                closestPillar = pillar;
                continue;
            }

            if(
               (pillar.transform.position - player.transform.position).magnitude < (closestPillar.transform.position - player.transform.position).magnitude
            )
            {
                closestPillar = pillar;
            }
        }

        return closestPillar;
    }
}
