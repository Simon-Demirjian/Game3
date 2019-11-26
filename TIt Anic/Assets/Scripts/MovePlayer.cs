using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    Rigidbody body;

    [SerializeField]
    float magnitude;
    float x;
    float z;

    float timerMax = 180;
    float timer;
    string thisScene;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        x = 1;
        z = 1;

        timer = 0;
        thisScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = magnitude * new Vector3(Input.GetAxisRaw("Horizontal") * x, body.velocity.y, Input.GetAxisRaw("Vertical") * z);
        x = 1;
        z = 1;

        timer += Time.deltaTime;

        if(timer >= timerMax)
        {
            SceneManager.LoadScene(thisScene, LoadSceneMode.Single);
        }
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
