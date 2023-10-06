using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject currentDisc;
    private DiscThrow throwState;
    [SerializeField] private List<GameObject> allPanels;

    private float changeColourTimer;
    [SerializeField] private float colourChangeTimerMax; // the value the timer reaches when a colour change happens

    // Start is called before the first frame update
    void Start()
    {
        throwState = FindObjectOfType<DiscThrow>();
        FindPanels();
    }

    // Update is called once per frame
    void Update()
    {
        changeColourTimer += Time.deltaTime;

        if (!throwState.ThrowReady)
        {
            if (currentDisc == null)
            {
                currentDisc = FindObjectOfType<DiscBehaviour>().gameObject;
            }
        }
        else
        {
            currentDisc = null;
        }

        if (changeColourTimer > colourChangeTimerMax)
        {
            int len = currentDisc.GetComponent<DiscBehaviour>().discColors.Length;
            
            if (currentDisc != null)
            {
                currentDisc.GetComponent<MeshRenderer>().material.color = currentDisc.GetComponent<DiscBehaviour>().discColors[Random.Range(0, len)];
            }
            
            // change all platform colours
            for (int i = 0; i < allPanels.Count; i++)
            {
                allPanels[i].GetComponent<MeshRenderer>().material.color = allPanels[i].GetComponent<PanelChanges>().PanelColours[Random.Range(0, len)];
            }

            changeColourTimer = 0;
        }
    }

    // will put every panel gameobject into the list
    private void FindPanels()
    {
        var panels = FindObjectsOfType<PanelChanges>();

        foreach (var current in panels)
        {
            allPanels.Add(current.gameObject);
        }
    }
}
