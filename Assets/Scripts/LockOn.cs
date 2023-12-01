using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    // MAKE CLASS A SINGLETON
    
    public List<GameObject> hitList = new List<GameObject>();
    [SerializeField] Transform rayOut;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] float maxDist;

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
                Destroy(go);
            }
            hitList.Clear();
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
