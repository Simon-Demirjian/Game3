using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
    public bool open;
    public bool contains;
    public bool containsEvidence;
    public string item;
    public string[] requiredItem;
    public bool playerHasRequiredItem;
    public Sprite image;
    public bool visited;

    private GameObject[] containers;

    public GameObject player;

    private float dresserDistance;
    private bool dresserNear;
    private GameObject closestDresser;

    private float standDistance;
    private bool standNear;
    private GameObject closestStand;

    private List<GameObject> dresserList;
    private List<GameObject> nightStandList;

    public Animator animator;
    private bool dresser;
    private bool stand;



    // Start is called before the first frame update
    void Start()
    {
        dresserList = new List<GameObject>();
        nightStandList = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");

        visited = false;
        foreach (string x in requiredItem)
        {
            if (x == "")
                playerHasRequiredItem = true;
            else
                playerHasRequiredItem = false;
        }

        dresserDistance = 1.5f;
        standDistance = 2.0f;

        animator = GetComponent<Animator>();

        if(animator.avatar && animator.avatar.name == "DresserAvatar")
        {
            dresser = true;
            stand = false;
        }
        else if(animator.avatar)
        {
            dresser = false;
            stand = true;
        }
        //generate dresser list based on tag

            /*

            containers = GameObject.FindGameObjectsWithTag("Container");

            foreach(GameObject container in containers)
            {
                if(container.GetComponent<Animator>().avatar.name == "DresserAvatar")
                {
                    dresserList.Add(container);

                }

                if (container.GetComponent<Animator>().avatar.name == "Night_StandAvatar")
                {
                    nightStandList.Add(container);

                }

            }
            */



            //foreach (GameObject dresser in dressers)
            //{
            //    dresserList.Add(dresser);
            //}
    }

    // Update is called once per frame
    void Update()
    {
        if(dresser)
        {
            DresserCheck();
        }
        else if(stand)
        {
            NightStandCheck();
        }

    }

    void DresserCheck()
    {
        int counter = 0;
        /*
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
        */


        //if the player is near an item, let them pick it up
        if (player.transform.position.x > transform.position.x - dresserDistance &&
                player.transform.position.x < transform.position.x + dresserDistance &&
                player.transform.position.z > transform.position.z - dresserDistance &&
                player.transform.position.z < transform.position.z + dresserDistance)
        {
            //dressAnim = closest.GetComponent<Animator>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                //AnimationScript animScript = closestDresser.GetComponent<AnimationScript>();
                animator = GetComponent<Animator>();

                //if (animScript.open)
                //{
                //    animScript.open = false;
                //}
                //else if (!animScript.open)
                //{
                //    animScript.open = true;
                //}
                animator.Play("DresserOpen");

            }
        }
    }

    void NightStandCheck()
    {
        /*
        int counter = 0;
        foreach (GameObject stand in nightStandList)
        {
            if (player.transform.position.x > stand.transform.position.x - standDistance &&
                player.transform.position.x < stand.transform.position.x + standDistance &&
                player.transform.position.z > stand.transform.position.z - standDistance &&
                player.transform.position.z < stand.transform.position.z + standDistance)
            {
                standNear = true;

                //set closest
                if (closestStand == null)
                {
                    closestStand = stand;
                }
                else if (closestStand != null || closestStand != stand)
                {
                    //if this item is closer
                    if (Vector3.Distance(player.transform.position, stand.transform.position) < Vector3.Distance(player.transform.position, closestStand.transform.position))
                    {
                        closestStand = stand;
                    }
                }
            }
            else
            {
                counter++;
            }
        }
        //if none of the items were near, don't bring up the option
        if (counter == nightStandList.Count)
        {
            standNear = false;
        }
        */

        //if the player is near an item, let them pick it up
        if (player.transform.position.x > transform.position.x - standDistance &&
                player.transform.position.x < transform.position.x + standDistance &&
                player.transform.position.z > transform.position.z - standDistance &&
                player.transform.position.z < transform.position.z + standDistance)
        {
            //dressAnim = closest.GetComponent<Animator>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                //AnimationScript animScript = closestDresser.GetComponent<AnimationScript>();
                animator = GetComponent<Animator>();

                //if (animScript.open)
                //{
                //    animScript.open = false;
                //}
                //else if (!animScript.open)
                //{
                //    animScript.open = true;
                //}
                animator.Play("NightStandOpen");

            }
        }
    }
}
