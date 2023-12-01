using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    // MAKE CLASS A SINGLETON
    
    public List<GameObject> hitList = new List<GameObject>();
    public DiscThrow throwB;
    [SerializeField] Transform rayOut;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] float maxDist;
    Coroutine cr;

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

            StartCoroutine(MoveToEnemyPositions());
            hitList.Clear();
        }
    }

    IEnumerator MoveToEnemyPositions()
    {
        for (int i = 0; i < hitList.Count; i++)
        {
            cr = StartCoroutine(GetEnemyPositions(i));
            yield return cr;
        }
    }

    IEnumerator GetEnemyPositions(int currentPos)
    {
        //throwB.newBall.transform.position
        yield return null;
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
