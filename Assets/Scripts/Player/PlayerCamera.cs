using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    public static float sensitivity = 2;

    [SerializeField] GameObject player;

    [SerializeField] EnemyBehaviour enemyBehaviour;
    [SerializeField] AnalogGlitch glitchFx;

    Vector2 camRotation;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = FindObjectOfType<PlayerMovement>().gameObject;
        glitchFx = GetComponent<AnalogGlitch>();

        if(enemyBehaviour != null)
        {
            enemyBehaviour.OnDeath += OnDeath;
        }
    }

    void Update()
    {
        if(Cursor.lockState != CursorLockMode.Locked) { return; }

        Vector2 mousePos = new Vector2(0, 0);

        mousePos.x = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensitivity * 100;
        mousePos.y = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensitivity * 100;

        camRotation.y += mousePos.x;
        camRotation.x -= mousePos.y;

        camRotation.x = Mathf.Clamp(camRotation.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);
        player.transform.rotation = Quaternion.Euler(0, camRotation.y, 0);
    }

    void OnDeath()
    {
        glitchFx.enabled = true;
        gameObject.GetComponent<PlayerCamera>().enabled = false;
    }
}
