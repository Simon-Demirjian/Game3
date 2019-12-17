using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuryScenePlayerManager : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(12.0f, 8.0f, -40.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < 0 || player.transform.position.x > 24)
            player.transform.position = new Vector3(12, 8, -40);

    }
}
