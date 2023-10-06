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

    public bool ThrowReady
    {
        get { return throwReady; }
    }

    public void Start()
    {
        throwReady = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && throwReady)
        {
            ThrowDisc();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !throwReady)
        {
            RetrieveDisc();
        }
    }

    public void ThrowDisc()
    {
        throwReady = false;
        var instance = Instantiate(discPrefab, discSpawn.transform.position, Quaternion.identity);
        instance.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public void DiscReady()
    {
        throwReady = true;
    }

    public void RetrieveDisc()
    {
        Destroy(GameObject.Find("Disc(Clone)"));
        throwReady = true;
    }
}
