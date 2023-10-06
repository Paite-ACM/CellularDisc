using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChanges : MonoBehaviour
{
    public GameObject Panel;

   

    [SerializeField]
    private Material PanelMaterial;

    

    public Color[] PanelColours;

    public bool CanChangeColour;
    // Start is called before the first frame update
    void Start()
    {

        CanChangeColour = true;

    }

    // Update is called once per frame
    void Update()
    {
        ChangePanelColour();

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
}
