using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DiscThrow : MonoBehaviour
{
    public GameObject discPrefab, newBall;
    public Transform discSpawn, playerPos, curvePoint;
    public TMP_Text cooldownText;
    private Rigidbody ballRB;
    
    public float speed, returnSpeed;
    public float returnCooldown = 3f;
    public bool throwReady, isReturning, isThrown;
    private float time = 0.0f;
    public Vector3 oldPos;
    public Camera cam;

    public GameManager gameManager;

    public bool ThrowReady
    {
        get { return throwReady; }
    }

    public void Start()
    {
        throwReady = true;
        isThrown = false;
        cam = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Update()
    {
        UpdateTimer();

        if (Input.GetKeyDown(KeyCode.Mouse0) && throwReady)
        {
            CreateAndThrowBall();
            gameManager.canChangeNextBallColour = true;
            returnCooldown = 3f;
            isThrown = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !throwReady && returnCooldown <= 0f)
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

        if (isThrown && returnCooldown >= 0)
        {
            returnCooldown -= Time.deltaTime;
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
    }

    //public void DrawTrajectory()
    //{
    //    RaycastHit hit;


    //}

    public void UpdateTimer()
    {
        cooldownText.text = "Cooldown: " + returnCooldown.ToString("F0");
    }
}