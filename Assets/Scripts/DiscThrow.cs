using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DiscThrow : MonoBehaviour
{
    [SerializeField]
    private GameObject discPrefab, newBall;
    [SerializeField]
    private Transform discSpawn, playerPos, curvePoint;
    private Rigidbody ballRB;
    
    public float speed, returnSpeed;
    private bool throwReady, isReturning;
    private float time = 0.0f;
    public Vector3 oldPos;
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

        if (isReturning)
        {
            if (time < 1.0f)
            {
                ballRB.position = getBezierQuadCurvePoint(time, oldPos, curvePoint.position, playerPos.position);
                time += Time.deltaTime;
            }
            else
            {
                ResetBall();
            }
        }
    }

    Vector3 getBezierQuadCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }

    public void CreateAndThrowBall()
    {
        isReturning = false;
        throwReady = false;
        newBall = Instantiate(discPrefab, discSpawn.position, cam.transform.localRotation);
        ballRB = newBall.GetComponent<Rigidbody>();
        ThrowBall(newBall);
    }
    public void ThrowBall(GameObject goForward)
    {
        goForward.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse);
    }

    public void ResetBall()
    {
        isReturning = false;
        Destroy(newBall);
        throwReady = true;
    }

    public void RetrieveDisc()
    {
        time = 0;
        oldPos = ballRB.position;
        ballRB.velocity = Vector3.zero;
        isReturning = true;
        //ballRB.isKinematic = true;

        //Destroy(newBall);
        //throwReady = true;
    }


}