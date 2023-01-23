using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{   
    public float offset;
    public float time;
    public float maxTime;       
    public float force;
    private float rotationLimitDownY = -2.9f;
    private float rotationLimitUpY = 13f;
    private bool isLoaded;
    public float load;
    public float loadMax;
    public Vector2 turn;    

    public GameObject cannonBall;
    public Transform SpawnPoint;
    public ParticleSystem shotEffectMuzzle;
    public GameObject reloadCore;
    public AudioSource audioSource;
    public AudioClip shootingAudioClip;

    private void Start()
    {
        reloadCore = GameObject.FindGameObjectWithTag("ReloadCore");
    }

    void Update()
    {
        MoveCannon();        
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && isLoaded)
        {
            load = 0f;
            Invoke("Shot",0.3f);                         
        }

        Reload();
    }
       
    private void Shot()
    {
        audioSource.PlayOneShot(shootingAudioClip);
        shotEffectMuzzle.Play();
        GameObject bullet = Instantiate(cannonBall, SpawnPoint.position, SpawnPoint.rotation);        
        bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(force, 0f, 0f));               
        Destroy(bullet, 5f);
    }

    void Reload()
    {
        load += 1f;
        load = Mathf.Clamp(load, 0, loadMax);

        if (load == loadMax)
        {
            isLoaded = true;
            reloadCore.SetActive(true);
            
        }
        else if (load < loadMax)
        {
            isLoaded = false;
            reloadCore.SetActive(false);
        }
    }

    void MoveCannon()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        turn.y = Mathf.Clamp(turn.y, rotationLimitDownY, rotationLimitUpY);
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
