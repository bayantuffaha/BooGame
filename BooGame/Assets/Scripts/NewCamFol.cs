using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NewCameraFollow : MonoBehaviour
{

    public GameObject target;
    public float camSpeed = 4.0f;

    void FixedUpdate()
    {
        //Vector2 pos = Vector2.Lerp((Vector2)transform.position, (Vector2)target.transform.position, camSpeed * Time.fixedDeltaTime);
        //transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        transform.position = target.transform.position
    }

    void OnEnable()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
    }
}