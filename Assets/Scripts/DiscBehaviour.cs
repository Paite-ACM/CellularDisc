using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// TODO: Remove most of the stuff in this and the panel change script as it is no longer in use
public class DiscBehaviour : MonoBehaviour
{
    public Color32[] discColors;
    public bool canChangeColor;
    public Material discMaterial;
    public GameObject discPrefab;

    private float colourChangeTimer;

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        discMaterial = GetComponent<MeshRenderer>().material;
        discColors = new Color32[4];
        canChangeColor = true;
        SetColours();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetColours()
    {
        discColors[0] = new Color32(255, 101, 80, 199);
        discColors[1] = new Color32(0, 180, 80, 199);
        discColors[2] = new Color32(136, 0, 255, 199);
        discColors[3] = new Color32(255, 255, 0, 199);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeDiscColor();
        //Debug.Log(GetComponent<MeshRenderer>().material.color);
    }

    private void Update()
    {
       // colourChangeTimer += Time.deltaTime;
    }

    public void ChangeDiscColor()
    {
        /*
        if (canChangeColor == true)
        {
            discMaterial.color = discColors[Random.Range(0, discColors.Length)];
            StartCoroutine("ColourTimer");
        } */

        /*if (colourChangeTimer > 5)
        {
            Debug.Log("Timer is above 5");
            discMaterial.color = discColors[Random.Range(0, discColors.Length)];
            colourChangeTimer = 0;
        } */
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision test");
        switch (collision.gameObject.tag)
        {
            case "Panel":
                // colour check
                if (collision.gameObject.GetComponent<MeshRenderer>().material.color == GetComponent<MeshRenderer>().material.color)
                {
                    // colours match
                    Debug.Log("Colour match!");
                    gameManager.IncreaseCombo();
                    gameManager.IncreaseScore();
                }
                else
                {
                    // colours don't match
                    Debug.Log("Colour does not match");
                    gameManager.combo = 0f;
                    collision.gameObject.GetComponent<Panel>().panelHealth -= 1f;
                    Debug.Log(gameManager.combo);
                }
                break;
        }
    }
}
