using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedOnVisual : MonoBehaviour
{
    [SerializeField] ParticleSystem marker;
    [SerializeField] Transform spawn;

    public void VisualMarker(bool active)
    {
        if (active)
        {
            Instantiate(marker, spawn.position, Quaternion.identity, gameObject.transform);
            marker.Play();
        }
        else
        {
            Destroy(marker);
        }
    }
}
