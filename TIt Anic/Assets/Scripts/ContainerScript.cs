using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
    public bool open;
    public bool contains;
    public bool containsEvidence;
    public string item;
    public string requiredItem;
    public bool playerHasRequiredItem;
    public Sprite image;

    private GameObject[] containers;

    public GameObject player;
    private float dresserDistance;
    private bool dresserNear;
    private GameObject closestDresser;

    private List<GameObject> dresserList = new List<GameObject>();

    private List<GameObject> nightStandList = new List<GameObject>();

    public Animator dressAnim;


    // Start is called before the first frame update
    void Start()
    {
        if(requiredItem == "")
            playerHasRequiredItem = true;
        else
            playerHasRequiredItem = false;


        dresserDistance = 1.5f;

        //generate dresser list based on tag

        containers = GameObject.FindGameObjectsWithTag("Container");

        foreach(GameObject container in containers)
        {
            if(container.GetComponent<Animator>().avatar.name == "DresserAvatar")
            {
                dresserList.Add(container);

            }

            if (container.GetComponent<Animator>().avatar.name == "DresserAvatar")
            {
                nightStandList.Add(container);

            }

        }



        //foreach (GameObject dresser in dressers)
        //{
        //    dresserList.Add(dresser);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        DresserCheck();
        NightStandCheck();

    }

    void DresserCheck()
    {
        int counter = 0;
        foreach (GameObject dresser in dresserList)
        {
            if (player.transform.position.x > dresser.transform.position.x - dresserDistance &&
                player.transform.position.x < dresser.transform.position.x + dresserDistance &&
                player.transform.position.z > dresser.transform.position.z - dresserDistance &&
                player.transform.position.z < dresser.transform.position.z + dresserDistance)
            {
                dresserNear = true;

                //set closest
                if (closestDresser == null)
                {
                    closestDresser = dresser;
                }
                else if (closestDresser != null || closestDresser != dresser)
                {
                    //if this item is closer
                    if (Vector3.Distance(player.transform.position, dresser.transform.position) < Vector3.Distance(player.transform.position, closestDresser.transform.position))
                    {
                        closestDresser = dresser;
                    }
                }
            }
            else
            {
                counter++;
            }
        }
        //if none of the items were near, don't bring up the option
        if (counter == dresserList.Count)
        {
            dresserNear = false;
        }

        //if the player is near an item, let them pick it up
        if (dresserNear)
        {
            //dressAnim = closest.GetComponent<Animator>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                //AnimationScript animScript = closestDresser.GetComponent<AnimationScript>();
                dressAnim = GetComponent<Animator>();

                //if (animScript.open)
                //{
                //    animScript.open = false;
                //}
                //else if (!animScript.open)
                //{
                //    animScript.open = true;
                //}
                dressAnim.Play("DresserOpen");

            }
        }
    }

    void NightStandCheck()
    {
        int counter = 0;
        foreach (GameObject dresser in dresserList)
        {
            if (player.transform.position.x > dresser.transform.position.x - dresserDistance &&
                player.transform.position.x < dresser.transform.position.x + dresserDistance &&
                player.transform.position.z > dresser.transform.position.z - dresserDistance &&
                player.transform.position.z < dresser.transform.position.z + dresserDistance)
            {
                dresserNear = true;

                //set closest
                if (closestDresser == null)
                {
                    closestDresser = dresser;
                }
                else if (closestDresser != null || closestDresser != dresser)
                {
                    //if this item is closer
                    if (Vector3.Distance(player.transform.position, dresser.transform.position) < Vector3.Distance(player.transform.position, closestDresser.transform.position))
                    {
                        closestDresser = dresser;
                    }
                }
            }
            else
            {
                counter++;
            }
        }
        //if none of the items were near, don't bring up the option
        if (counter == dresserList.Count)
        {
            dresserNear = false;
        }

        //if the player is near an item, let them pick it up
        if (dresserNear)
        {
            //dressAnim = closest.GetComponent<Animator>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                //AnimationScript animScript = closestDresser.GetComponent<AnimationScript>();
                dressAnim = GetComponent<Animator>();

                //if (animScript.open)
                //{
                //    animScript.open = false;
                //}
                //else if (!animScript.open)
                //{
                //    animScript.open = true;
                //}
                dressAnim.Play("DresserOpen");

            }
        }
    }
}
