using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void Awake()
    {
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject.Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -0.03f, 0));
    }
}
