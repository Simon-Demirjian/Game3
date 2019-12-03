using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEditor.SceneManagement;
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
    string trialScene;
    string masterScene;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        x = 1;
        z = 1;

        timer = 0;
        thisScene = SceneManager.GetActiveScene().name;
        masterScene = "Master_Scene";
        trialScene = "Scene_Jury";
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
            if (thisScene.Equals("Scene_Jury"))
            {
                //SceneManager.LoadScene(thisScene, LoadSceneMode.Single);
                SceneManager.LoadScene(masterScene);

            }

            else
            {
                SceneManager.LoadScene(trialScene);

            }
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
