using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public Image blackScreen;

    public GameObject mainMenuCollective;

    public GameObject mainMenu, optionsMenu, credits, quitMenu;

    public GameObject[] mainMenuButtons;

    public GameObject[] optionsMenuButtons;

    public GameObject[] quitMenuButtons;

    public GameObject currentSelectedButton;

    private int mainMenuIndex = 0;

    private int optionsMenuIndex = 0;

    private int quitMenuIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        MenuFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 

        // Navigation in the first Pause Menu Canvas
        if (mainMenu.activeSelf)
        {
            if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (mainMenuIndex <= 0)
                {
                    mainMenuIndex = 0;
                }
                else
                {
                    mainMenuIndex--;
                }

                currentSelectedButton.GetComponent<ButtonSound>().Reset();
            }
            else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (mainMenuIndex >= mainMenuButtons.Length - 1) 
                {
                    mainMenuIndex = mainMenuButtons.Length - 1;
                }
                else
                {
                    mainMenuIndex++;
                }

                currentSelectedButton.GetComponent<ButtonSound>().Reset();
            }

            EventSystem.current.SetSelectedGameObject(mainMenuButtons[mainMenuIndex]);
            currentSelectedButton = mainMenuButtons[mainMenuIndex];

            currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();
        }


        // Navigation in the first Pause Menu Canvas
        if (optionsMenu.activeSelf)
        {
            if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (optionsMenuIndex <= 0)
                {
                    optionsMenuIndex = 0;
                }
                else
                {
                    optionsMenuIndex--;
                }

                currentSelectedButton.GetComponent<ButtonSound>().Reset();
            }
            else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (optionsMenuIndex >= optionsMenuButtons.Length - 1) 
                {
                    optionsMenuIndex = optionsMenuButtons.Length - 1;
                }
                else
                {
                    optionsMenuIndex++;
                }
            
                currentSelectedButton.GetComponent<ButtonSound>().Reset();
            }

            EventSystem.current.SetSelectedGameObject(optionsMenuButtons[optionsMenuIndex]);
            currentSelectedButton = optionsMenuButtons[optionsMenuIndex];

            if (currentSelectedButton.GetComponent<ButtonSound>())
            {
                currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();
            }
        }


        if (quitMenu.activeSelf)
        {
            if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (quitMenuIndex <= 0)
                {
                    quitMenuIndex = 0;
                }
                else
                {
                    quitMenuIndex--;
                }

                currentSelectedButton.GetComponent<ButtonSound>().Reset();
            }
            else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (quitMenuIndex >= quitMenuButtons.Length - 1) 
                {
                    quitMenuIndex = quitMenuButtons.Length - 1;
                }
                else
                {
                    quitMenuIndex++;
                }

                currentSelectedButton.GetComponent<ButtonSound>().Reset();
            }

            EventSystem.current.SetSelectedGameObject(quitMenuButtons[quitMenuIndex]);
            currentSelectedButton = quitMenuButtons[quitMenuIndex];      

            currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();      
        }


        if (currentSelectedButton != credits)
        {
            credits.transform.GetChild(1).GetComponent<AudioSource>().Stop();
        }
    }


    public void MenuFadeIn()
    {
        blackScreen.CrossFadeAlpha(0, .5f, false);
    }

    public void StartGame()
    {
        blackScreen.CrossFadeAlpha(1, .5f, false);
        StartCoroutine(PrepareForStart());
    }

    IEnumerator PrepareForStart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        quitMenu.SetActive(false);
        optionsMenu.SetActive(true);

        optionsMenuIndex = 0;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsMenuButtons[optionsMenuIndex]);
        currentSelectedButton = optionsMenuButtons[optionsMenuIndex];

        currentSelectedButton.GetComponent<ButtonSound>().Reset();
        currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();


    }

    public void GameCredits()
    {
        credits.transform.GetChild(1).GetComponent<AudioSource>().Play(0);
    }

    public void ConfirmGameQuit()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        quitMenu.SetActive(true);

        quitMenuIndex = 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitMenuButtons[quitMenuIndex]);
        currentSelectedButton = quitMenuButtons[quitMenuIndex];

        currentSelectedButton.GetComponent<ButtonSound>().Reset();
        currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();
    }

    public void Quit() 
    {
        Application.Quit();
    }

    public void GoBackToMainMenu()
    {
        quitMenu.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);

        mainMenuIndex = 0;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuButtons[mainMenuIndex]);
        currentSelectedButton = mainMenuButtons[mainMenuIndex];

        currentSelectedButton = mainMenuButtons[mainMenuIndex];
        currentSelectedButton.GetComponent<ButtonSound>().Reset();
        currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();

    }

}
