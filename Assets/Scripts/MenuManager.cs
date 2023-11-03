using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject MenuUI;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        MenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryButton()
    {

    }

    public void UpgradesButton()
    {

    }

    public void ExitButton()
    {

    }
}
