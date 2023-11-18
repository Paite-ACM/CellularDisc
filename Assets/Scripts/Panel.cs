using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject panel;

   

    [SerializeField]
    private Material PanelMaterial;

    

    public Color[] PanelColours;

    public bool CanChangeColour;

    public float panelHealth = 2f;

    // Start is called before the first frame update
    void Start()
    {

        CanChangeColour = true;

    }

    // Update is called once per frame
    void Update()
    {
        ChangePanelColour();
        DestroyPanel();
    }

    public void ChangePanelColour()
    {
        /*if (CanChangeColour == true)
        {                         
            PanelMaterial.color = PanelColours[Random.Range(0, PanelColours.Length)];               
            StartCoroutine("ColourTimer");
        } */
    }

    /*IEnumerator ColourTimer()
    {
        CanChangeColour = false;
        yield return new WaitForSeconds(5f);
        CanChangeColour = true;
    } */

    // Will destroy panels that have 0 health
    public void DestroyPanel()
    {
        if (panelHealth < 0f)
        {
            Destroy(gameObject);
        }
    }
}
