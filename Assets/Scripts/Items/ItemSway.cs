using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemSway : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float smooth;
    [SerializeField] float swayMult;

    void Update()
    {
        Vector2 mouseSway = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * swayMult;

        Quaternion rotX = Quaternion.AngleAxis(-mouseSway.y, Vector3.right);
        Quaternion rotY = Quaternion.AngleAxis(-mouseSway.x, Vector3.up);

        Quaternion targetRot = rotX * rotY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, smooth * Time.deltaTime);
    }
}
