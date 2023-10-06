using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscThrow : MonoBehaviour
{
    [SerializeField]
    private GameObject discPrefab;
    [SerializeField]
    private Transform discSpawn;
    [SerializeField]
    private float speed;
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
            RetrieveDisc();
        }
    }

    public void CreateAndThrowBall()
    {
        throwReady = false;
        GameObject instance = Instantiate(discPrefab, discSpawn.position, cam.transform.localRotation);
        ThrowBall(instance);
    }
    
    public void ThrowBall(GameObject go)
    {
        go.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse);
    }

    public void DiscReady()
    {
        throwReady = true;
    }

    public void RetrieveDisc()
    {
        Destroy(GameObject.Find("Ball(Clone)"));
        throwReady = true;
    }
}
