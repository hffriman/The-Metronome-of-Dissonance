using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// This script is used in the Main Menu //
public class MainMenuController : MonoBehaviour
{

    // The black image that is used for fade-ins and fade-outs
    public Image blackScreen;

    // The whole canvas that contains all the buttons in the Main Menu (including the sub-menus in Options and Quit Game)
    public GameObject mainMenuCollective;

    // These are the buttons and submenus that belong to the mainMenuCollective
    public GameObject mainMenu, optionsMenu, credits, quitMenu;

    // This contains of the buttons that are shown first in the Main Menu (Main Menu's starting point, Start Demo, Options, Credits, Quit Game)
    public GameObject[] mainMenuButtons;

    // This contains the buttons in the Options sub-menu (Options sub-menu's starting point, Volume Slider, Back)
    public GameObject[] optionsMenuButtons;

    // This contains all the buttons that appear when "Quit Game" is selected (QuitConfirm, Yes, No)
    public GameObject[] quitMenuButtons;

    // This defines the current selected button
    public GameObject currentSelectedButton;

    // This stores the index of the buttons in the Main Menu (Main Menu's starting point, Start Demo, Options, Credits, Quit Game)
    private int mainMenuIndex = 0;

    // This stores the index of the buttons in the Options sub-menu (Options sub-menu's starting point, Volume Slider, Back)
    private int optionsMenuIndex = 0;

    // This stores the index of the buttons that appear when "Quit Game" is selected (QuitConfirm, Yes, No)
    private int quitMenuIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        // When the Main Menu starts, the blackscreen fades away by this function
        MenuFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        // The mouse will be locked and hidden: only W/S keys or Up/Down keys are used
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 

        /* 
            - If the Main Menu's main section is active, the main menu index increases or decreases
              every time the player navigates with W/S keys or Up/Down keys
            - The current active button is defined based on this index
            - Before the new current button is defined, the old active button
              is resetted, meaning that it stops its audio source whenever
              the player continues navigating.
        */
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

            // - This section defines the new active button based on the increased/decreased Main Menu index
            // - Also, the new active button's audio source is activated
            EventSystem.current.SetSelectedGameObject(mainMenuButtons[mainMenuIndex]);
            currentSelectedButton = mainMenuButtons[mainMenuIndex];
            currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();
        }


        /* 
            - If the Option Menu's main section is active, the option menu index increases or decreases
              every time the player navigates with W/S keys or Up/Down keys
            - The old active button is resetted, and the new current button is defined
              based on the Options Menu's index
        */
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
            
            // - This section defines the new active button based on the increased/decreased Option Menu index
            // - Also, the new active button's audio source is activated
            EventSystem.current.SetSelectedGameObject(optionsMenuButtons[optionsMenuIndex]);
            currentSelectedButton = optionsMenuButtons[optionsMenuIndex];
            currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();
        }

        /* 
            - If the Quit Game section is active, the Quit Game section's index increases or decreases
              every time the player navigates with W/s keys or Up/Down keys
            - The old active button is resetted, and the new current button is defined
              based on the Quit Game section's index
        */
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

            // - This section defines the new active button based on the increased/decreased Quit Menu index
            // - Also, the new active button's audio source is activated
            EventSystem.current.SetSelectedGameObject(quitMenuButtons[quitMenuIndex]);
            currentSelectedButton = quitMenuButtons[quitMenuIndex];      
            currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();      
        }

        /* 
            This section is used to stop the Credits audio every time 
            the player navigates away from the credits button 
            (otherwise the credits audio would continue playing even though the other buttons are selected)
        */
        if (currentSelectedButton != credits)
        {
            credits.transform.GetChild(1).GetComponent<AudioSource>().Stop();
        }
    }

    // This function fades the black screen away, showing the Main Menu
    public void MenuFadeIn()
    {
        blackScreen.CrossFadeAlpha(0, .5f, false);
    }

    // When the Start Game button is pressed, the black screen appears
    // and the game starts after preparations (defined in PrepareForStart function)
    public void StartGame()
    {
        blackScreen.CrossFadeAlpha(1, .5f, false);
        StartCoroutine(PrepareForStart());
    }

    // After some seconds, the new scene is loaded
    IEnumerator PrepareForStart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /* 
       - This opens the Options Menu part of the Main Menu canvas
       - Other parts of the canvas (Main Menu's main section and Quit Game section) are disabled
       - The Options Menu's index is resetted to 0, and the new active button is defined by it
       - Also, the current button's audio source is activated
    */
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

    // This plays the credits audio whenever Credits button is pressed
    public void GameCredits()
    {
        credits.transform.GetChild(1).GetComponent<AudioSource>().Play(0);
    }


    /*
       - This opens the Quit Game section of the Main Menu canvas
       - Other parts of the canvas (Main Menu's main section and Options section) are disabled
       - The Quit Game section's index is resetted to 0, and the new active button is defined by it
       - Also, the current button's audio source is activated
    */
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

    // When the player has pressed "Yes" button in the Quit Game section, the game ends
    public void Quit() 
    {
        Application.Quit();
    }

    /*
       - When the player has pressed "Back" or "No" buttons, the Main Menu's main section is activated
       - Other parts of the canvas (Main Menu's main section and Options section) are disabled
       - The Main Menu's main index is resetted to 0, and the new active button is defined by it
       - Also, the current button's audio source is activated
    */
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
