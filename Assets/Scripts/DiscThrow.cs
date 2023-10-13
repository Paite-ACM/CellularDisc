using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscThrow : MonoBehaviour
{
    [SerializeField]
    private GameObject discPrefab, newBall;
    [SerializeField]
    private Transform discSpawn, playerPos;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    private float speed, returnSpeed;
    private bool throwReady;
    public Camera cam;

    public bool ThrowReady
    {
        get { return throwReady; }
    }

    public void Start()
    {
        throwReady = true;
        cam = Camera.main;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && throwReady)
        {
            CreateAndThrowBall();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !throwReady)
        {
            RetrieveDisc(newBall);
        }
    }

    public void CreateAndThrowBall()
    {
        throwReady = false;
        newBall = Instantiate(discPrefab, discSpawn.position, cam.transform.localRotation);
        ThrowBall(newBall);
    }
    
    public void ThrowBall(GameObject goForward)
    {
        goForward.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse);
    }

    public void DiscReady()
    {
        throwReady = true;
    }

    public void RetrieveDisc(GameObject goBack)
    {
        

        //Destroy(newBall);
        throwReady = true;
    }


}
