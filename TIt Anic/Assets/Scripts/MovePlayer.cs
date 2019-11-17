using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Rigidbody body;

    [SerializeField]
    float magnitude;
    float x;
    float z;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        x = 1;
        z = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = magnitude * new Vector3(Input.GetAxisRaw("Horizontal") * x, body.velocity.y, Input.GetAxisRaw("Vertical") * z);
        x = 1;
        z = 1;
    }

    private void OnCollisionStay(Collision collision)
    {
        body.constraints = body.constraints | RigidbodyConstraints.FreezePositionY;
        Vector3 temp = new Vector3(Mathf.Abs(collision.contacts[0].normal.x), Mathf.Abs(collision.contacts[0].normal.y), Mathf.Abs(collision.contacts[0].normal.z));
        if ((temp.x > 0) && (temp.x > temp.y))
        {
            z = 1.5f;
        }
        if ((temp.z > 0) && (temp.z > temp.y))
        {
            x = 1.5f;
        }
    }
}
