using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public Vector3 _rotation;
    public GameObject waypointsContainer;
    public List<GameObject> waypoints;   
    public Rigidbody rb;

    public float speed = 2;
    int index = 0;
    public bool isLoop = true;
    Vector3 newPos;        
    public float health = 3;
    private int damage;
    public bool isAlive;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        FillTheListOfWaypoints();

        damage = Random.Range(1, 4);        
    }

    private void Update()
    {
        if(isAlive)
        {
            MoveShipWithPoints();
        }
        else if (!isAlive)
        {
            ShipSink();
        }

        SpawnNewShip();
    }

    GameObject FindCorrectWaypoints()
    {        
        if (gameObject.tag == "Ship1")
        {
            GameObject waypoints = GameObject.FindGameObjectWithTag("Waypoints1");

            return waypoints;
        }
        else if (gameObject.tag == "Ship2")
        {
            GameObject waypoints = GameObject.FindGameObjectWithTag("Waypoints2");

            return waypoints;
        }
        else if (gameObject.tag == "Ship3")
        {
            GameObject waypoints = GameObject.FindGameObjectWithTag("Waypoints3");

            return waypoints;
        }

        return null;
    }

    void FillTheListOfWaypoints()
    {
        waypointsContainer = FindCorrectWaypoints();

        for (int i = 0; i < waypointsContainer.transform.childCount; i++)
        {
            waypoints.Add(waypointsContainer.transform.GetChild(i).gameObject);
        }
    }

    void ShipSink()
    {
        rb.useGravity = true;
        transform.Rotate(_rotation * Time.deltaTime);
    }

    void MoveShipWithPoints()
    {
        Vector3 destination = waypoints[index].transform.position;
        newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.LookAt(newPos);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);

        if (distance <= 0.05)
        {
            if (index < waypoints.Count - 1)
            {
                index++;
            }
            else
            {
                if (isLoop)
                {
                    index = 0;
                }
            }
        }
    }

    void IsAlive()
    {
        isAlive = false;

        Component[] array = gameObject.GetComponentsInChildren(typeof(Floating));

        foreach(Floating f in array)
        {
            f.enabled = false;
        }                     
    }

    void SpawnNewShip()
    {
        if (gameObject.tag == "Ship1" && health < 1 && health >= -6)
        {
            health = -10;
            GameObject spawner = GameObject.FindGameObjectWithTag("Spawner1");
            spawner.GetComponent<RandomSpawner>().canSpawn = true;
        }
        else if (gameObject.tag == "Ship2" && health < 1 && health >= -6)
        {
            health = -10;
            GameObject spawner = GameObject.FindGameObjectWithTag("Spawner2");
            spawner.GetComponent<RandomSpawner>().canSpawn = true;
        }
        else if (gameObject.tag == "Ship3" && health < 1 && health >= -6)
        {
            health = -10;
            GameObject spawner = GameObject.FindGameObjectWithTag("Spawner3");
            spawner.GetComponent<RandomSpawner>().canSpawn = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Core")
        {            
            Destroy(collision.gameObject);            
            health -= damage;
          
            if (health <= 0)
            {
                speed = 2f;

                Invoke("IsAlive", 1.5f);
                
                Destroy(gameObject,8f);                
            }
        }       
    }   
}
