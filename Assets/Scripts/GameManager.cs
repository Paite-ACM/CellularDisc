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

    [SerializeField] private Color32[] allColours;

    public float score;
    // Start is called before the first frame update
    void Start()
    {
        allColours = new Color32[4];
        SetColours();
        throwState = FindObjectOfType<DiscThrow>();
        FindPanels();
        score = 0f;
    }

    public void SetColours()
    {
        allColours[0] = new Color32(255, 101, 80, 255);
        allColours[1] = new Color32(0, 180, 80, 255);
        allColours[2] = new Color32(136, 0, 255, 255);
        allColours[3] = new Color32(255, 255, 0, 255);
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
            if (currentDisc != null)
            {
                currentDisc.GetComponent<MeshRenderer>().material.color = allColours[Random.Range(0, allColours.Length)];
            }
            
            // change all platform colours
            for (int i = 0; i < allPanels.Count; i++)
            {
                allPanels[i].GetComponent<MeshRenderer>().material.color = allColours[Random.Range(0, allColours.Length)];
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
    /// <summary>
    /// Will increase score variable
    /// </summary>
    public void IncreaseScore()
    {
        score++;
        Debug.Log(score);
    }
}
