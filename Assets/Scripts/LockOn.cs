using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    // MAKE CLASS A SINGLETON
    
    public List<GameObject> hitList = new List<GameObject>();
    public DiscThrow throwB;
    int currentHit = 0;
    bool bailiffActive = false;
    [SerializeField] Transform rayOut;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] float maxDist;

    private void Start()
    {
        throwB = FindObjectOfType<DiscThrow>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            FireRay();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            foreach (GameObject go in hitList)
            {
                go.GetComponent<LockedOnVisual>().VisualMarker(false);
                
            }
            bailiffActive = true;
            StartCoroutine(BailiffMove());
            //hitList.Clear();

            Debug.Log(hitList.Count);
        }
    }

    IEnumerator BailiffMove()
    {
        while (bailiffActive)
        {
            if (hitList.Count != 0)
            {
                throwB.isReturning = false;
                throwB.throwReady = false;
                if (throwB.newBall = null)
                {
                    throwB.newBall = Instantiate(throwB.discPrefab, throwB.discSpawn.position, throwB.cam.transform.localRotation);

                    if (Vector3.Distance(throwB.newBall.transform.position, hitList[currentHit].transform.position) < 0.8f)
                    {
                        if (currentHit < hitList.Count - 1)
                        {
                            currentHit++;
                        }
                        else
                        {
                            throwB.RetrieveDisc();
                            hitList.Clear();
                            bailiffActive = false;
                            break;
                        }
                        float t = 10 * Time.deltaTime;
                        throwB.newBall.transform.position = Vector3.MoveTowards(throwB.newBall.transform.position, hitList[currentHit].transform.position, t);
                    }
                    yield return null;
                }
                yield return null;
            }
            yield return null;
        }
    }

    void FireRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(rayOut.transform.position, rayOut.transform.forward, out hit, maxDist, hitLayer))
        {
            if (!hitList.Contains(hit.transform.gameObject))
            {
                hitList.Add(hit.transform.gameObject);

                hit.transform.gameObject.GetComponent<LockedOnVisual>().VisualMarker(true);
            }

        }
    }
}
