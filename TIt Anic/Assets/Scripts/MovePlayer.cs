using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    Rigidbody body;

    public GameObject titanicTop;

    public GameObject marker1;
    public GameObject marker3;
    public GameObject clockHand;



    [SerializeField]
    float magnitude;
    float x;
    float z;

    float timerMax = 60;
    float timerMaxJury = 35;
    float timer;
    string thisScene;
    string trialScene;
    string masterScene;

    float titanicTopWidth1;
    float titanicTopLength1;
    float titanicTopWidth2;
    float titanicTopLength2;

    private GameObject[] tops;
    private List<GameObject> topList = new List<GameObject>();

    [HideInInspector]
    public bool canMove = true;

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


        tops = GameObject.FindGameObjectsWithTag("Top");
        foreach (GameObject top in tops)
        {
            topList.Add(top);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            timer += timerMax;
        }

        if(canMove)
        {
            body.velocity = magnitude * new Vector3(Input.GetAxisRaw("Horizontal") * x, body.velocity.y, Input.GetAxisRaw("Vertical") * z);
            x = 1;
            z = 1;
        }

        timer += Time.deltaTime;
       
        if (SceneManager.GetActiveScene().name.Equals("Scene_Jury"))
        {
            clockHand.transform.rotation = Quaternion.Euler(0, 0, 360 * (-timer / timerMaxJury));
        }
        else
        {
            clockHand.transform.rotation = Quaternion.Euler(0, 0, 360 * (-timer / timerMax));
        }

        if(timer >= timerMaxJury && SceneManager.GetActiveScene().name.Equals("Scene_Jury"))
        {
            SceneManager.LoadScene(masterScene);
            timer = 0;
        }
        else if(timer >= timerMax)
        {
            SceneManager.LoadScene(trialScene);
            timer = 0;
        }

        if(titanicTop && marker1 && marker3)
        {
            titanicTopWidth1 = titanicTop.transform.position.x - marker1.transform.position.x;
            titanicTopLength1 = titanicTop.transform.position.z - marker1.transform.position.z;

            titanicTopWidth2 = marker3.transform.position.x - titanicTop.transform.position.x;
            titanicTopLength2 = marker3.transform.position.z - titanicTop.transform.position.z;

            titanicTopLength2 /= 5.0f;


            if (this.transform.position.x < titanicTop.transform.position.x + titanicTopWidth2 &&
                this.transform.position.x > titanicTop.transform.position.x - titanicTopWidth1 &&
                this.transform.position.z < titanicTop.transform.position.z + titanicTopLength2 &&
                this.transform.position.z > titanicTop.transform.position.z - titanicTopLength1)
            {
                //titanicTop.SetActive(false);
                foreach (GameObject top in topList)
                {
                    top.GetComponent<MeshRenderer>().rendererPriority = 0;

                    Material[] materials = top.GetComponent<MeshRenderer>().materials;

                    if (materials.Length > 1)
                    {
                        foreach (Material material in materials)
                        {
                            Color color = material.color;
                            color.a -= Time.deltaTime * 2f;
                            if (color.a <= 0.0f)
                            {
                                color.a = 0.0f;
                            }
                            material.color = color;
                        }
                    }

                    else
                    {
                        Color color = materials[0].color;
                        color.a -= Time.deltaTime * 2f;
                        if (color.a <= 0.0f)
                        {
                            color.a = 0.0f;
                        }
                        materials[0].color = color;

                    }
                }


            }

            else
            {
                //titanicTop.SetActive(true);
                foreach (GameObject top in topList)
                {
                    Material[] materials = top.GetComponent<MeshRenderer>().materials;

                    if (materials.Length > 1)
                    {
                        foreach (Material material in materials)
                        {
                            Color color = material.color;
                            color.a += Time.deltaTime * 2f;
                            if (color.a >= 1.0f)
                            {
                                color.a = 1.0f;
                            }
                            material.color = color;
                        }
                    }

                    else
                    {
                        Color color = materials[0].color;
                        color.a += Time.deltaTime * 2f;
                        if (color.a >= 1.0f)
                        {
                            color.a = 1.0f;
                        }
                        materials[0].color = color;

                    }
                }

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
