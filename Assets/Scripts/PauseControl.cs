using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
    - This script is used in the Pause Menu (In Stages 1 and 2)
    - It has three indexing systems that are used in different parts
      of the Pause Menu (the Main Pause section, Options section and Return to Main Menu section)
    - The current active button is defined based on those indexes
*/
public class PauseControl : MonoBehaviour
{

    // The boolean that checks if the game is paused
    public static bool isPaused;

    // The main canvas of the Pause Menu
    public GameObject pauseMenuCollective;

    // All the sub-menus of the Pause Menu canvas (Pause Menu, Options Menu, Return to Main Menu section)
    public GameObject pauseMenu, tutorial, optionsMenu, areYouSureMenu;

    // The collection of the game scene's audio sources
    private AudioSource[] audioSources;

    // All the buttons in the Pause Menu's first section (Continue, Options, Return to Main Menu)
    public GameObject[] pauseMenuButtons;

    // All the buttons in the Pause Menu's Options section (Options menu's starting point, Volume slider, Back)
    public GameObject[] optionsMenuButtons;

    // All the buttons in the Pause Menu's Return to Main Menu section (Return Confirmation, Yes, No)
    public GameObject[] returnConfirmationButtons;

    // Defines the current active button
    public GameObject currentSelectedButton;

    // Indexes of the three sections of the Pause Menu (the first section, Options section and Return to Main Menu section)
    private int pauseMenuIndex = 0;
    private int optionsMenuIndex = 0;
    private int returnMainMenuIndex = 0;


    void Start()
    {
        // In the beginning of the stage, the Pause Menu is disabled, and all the audio sources are stored
        pauseMenuCollective.GetComponent<Canvas>().enabled = false;
        audioSources = FindObjectsOfType<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        // The mouse cursor is locked and hidden (the navigation must be done with the keyboard)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 

        // If the Pause Menu is active, and the mouse buttons are pressed, the current active button will be the first button in the section
        // This is done in order to prevent the button highlights to disappear suddenly when clicking the mouse
        if (pauseMenuCollective.activeSelf)
        {

            if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                if (pauseMenu.activeSelf)
                {
                    pauseMenuIndex = 0;
                    EventSystem.current.SetSelectedGameObject(pauseMenuButtons[pauseMenuIndex]);
                }
                else if (areYouSureMenu.activeSelf)
                {
                    returnMainMenuIndex = 0;
                    EventSystem.current.SetSelectedGameObject(returnConfirmationButtons[returnMainMenuIndex]);
                }
            }

            // When Escape key is pressed, the game is paused and the Pause Menu will open
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                OpenPauseMenu();
            }


             /*
                - If the Pause Menu's first section is active, the Pause Menu's index will be increased/decreased
                  whenever the player navigates with W/S or Up/Down keys
                - The previous active button will be resetted, and then the new active button is defined based
                  on the new Pause Menu index
            */
            if (isPaused && pauseMenu.activeSelf)
            {
                if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (pauseMenuIndex <= 0)
                    {
                        pauseMenuIndex = 0;
                    }
                    else
                    {
                        pauseMenuIndex--;
                    }

                    currentSelectedButton.GetComponent<ButtonSound>().Reset();
                }
                else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (pauseMenuIndex >= pauseMenuButtons.Length - 1) 
                    {
                        pauseMenuIndex = pauseMenuButtons.Length - 1;
                    }
                    else
                    {
                        pauseMenuIndex++;
                    }

                    currentSelectedButton.GetComponent<ButtonSound>().Reset();
                }

                EventSystem.current.SetSelectedGameObject(pauseMenuButtons[pauseMenuIndex]);
                currentSelectedButton = pauseMenuButtons[pauseMenuIndex];
            }


            /*
                - If the Options Menu's section is active, the Option Menu's index will be increased/decreased
                  whenever the player navigates with W/S or Up/Down keys
                - The previous active button will be resetted, and then the new active button is defined based
                  on the new Option Menu's index
            */
            if (isPaused && optionsMenu.activeSelf)
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

            /*
                - If the "Return to Main Menu" section is active, the "Return to Main Menu" section's index 
                  will be increased/decreased whenever the player navigates with W/S or Up/Down keys
                - The previous active button will be resetted, and then the new active button is defined based
                  on the new "Return to Main Menu" section's index
            */
            if (isPaused && areYouSureMenu.activeSelf)
            {
                if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (returnMainMenuIndex <= 0)
                    {
                        returnMainMenuIndex = 0;
                    }
                    else
                    {
                        returnMainMenuIndex--;
                    }

                    currentSelectedButton.GetComponent<ButtonSound>().Reset();
                }
                else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (returnMainMenuIndex >= returnConfirmationButtons.Length - 1)
                    {
                        returnMainMenuIndex = returnConfirmationButtons.Length - 1;
                    }
                    else
                    {
                        returnMainMenuIndex++;
                    }

                    currentSelectedButton.GetComponent<ButtonSound>().Reset();
                }
                EventSystem.current.SetSelectedGameObject(returnConfirmationButtons[returnMainMenuIndex]);
                currentSelectedButton = returnConfirmationButtons[returnMainMenuIndex];
            }

            /* 
                This section is used to stop the tutorial audio every time 
                the player navigates away from the Tutorial button 
                (otherwise the tutorial audio would continue playing even though the other buttons are selected)
            */
            if (currentSelectedButton != tutorial)
            {
                tutorial.transform.GetChild(1).GetComponent<AudioSource>().Stop();
            }
            
            // When the game is paused, the introduction audio will play
            if (isPaused)
            {
                currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();
            }
        }
    }

    // This opens the Pause Menu when the Escape key is pressed
    void OpenPauseMenu()
    {
        // All the audio sources in the stage will be paused
        // (the Pause Menu's audio sources are activated later)
        // NOTE: the pause menu has its own audio listener 
        //       --> the game scenes own audio listener is disabled when the Pause Menu is activated
        if (pauseMenuCollective && isPaused)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.Pause();
            }

            // The scene's time scale will be set to 0,
            // and the Pause Menu canvas will be activated
            Time.timeScale = 0f;
            pauseMenuCollective.GetComponent<Canvas>().enabled = true;
            pauseMenu.SetActive(true);
            
            // Pause Menu's index will be set 0, and the active button will be defined by it
            pauseMenuIndex = 0;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseMenuButtons[pauseMenuIndex]);
            currentSelectedButton = pauseMenuButtons[pauseMenuIndex];
            currentSelectedButton.GetComponent<ButtonSound>().Reset();
        }
        else
        {
            // If the game is not paused, the time scale will become 1,
            // the game scene's audio listener continues playing,
            // all the paused audio sources are set to continue playing,
            // and all the Pause Menu's sections (and canvas itself) will be disabled
            Time.timeScale = 1;
            AudioListener.pause = false;
            pauseMenuCollective.GetComponent<Canvas>().enabled = false;
            pauseMenu.SetActive(false);
            areYouSureMenu.SetActive(false);

            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.UnPause();
            }
        }
    }


    public void Tutorial()
    {
        tutorial.transform.GetChild(1).GetComponent<AudioSource>().Play(0);
    }


    /* 
       - This opens the Options Menu when the "Options" button is pressed
       - Other parts of the Pause Menu are disabled
       - Also, the Option Menu's index is set to 0, and the current button is defined by it
    */
    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
        areYouSureMenu.SetActive(false);
        optionsMenu.SetActive(true);

        optionsMenuIndex = 0;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsMenuButtons[optionsMenuIndex]);
        currentSelectedButton = optionsMenuButtons[optionsMenuIndex];

        currentSelectedButton.GetComponent<ButtonSound>().Reset();
        currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();
    }

    /* 
       - This disables the Options Menu and returns the Pause Menu's first section
       - Other parts of the Pause Menu are disabled
       - Also, the Pause Menu's index is set to 0, and the current button is defined by it
    */
     public void ExitOptionsMenu()
    {
        areYouSureMenu.SetActive(false);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        
        pauseMenuIndex = 0;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuButtons[pauseMenuIndex]);
    }

    // This disables the Pause Menu (it calls the OpenPauseMenu function but with a false boolean as a parameter)
    public void ContinuePlaying()
    {
        isPaused = false;
        OpenPauseMenu();
    }

    
    
    /* 
       - This opens the "Return to Main Menu" confirmation section when the "Return to Main Menu" button is pressed
       - Other parts of the Pause Menu are disabled
       - Also, the "Return to Main Menu" section's index is set to 0, and the current button is defined by it
    */
    public void ReturnToMenuConfirmation()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        areYouSureMenu.SetActive(true);

        returnMainMenuIndex = 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(returnConfirmationButtons[returnMainMenuIndex]);

        currentSelectedButton = returnConfirmationButtons[returnMainMenuIndex];
        currentSelectedButton.GetComponent<ButtonSound>().Reset();
        currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();
    }

    /* 
        - If the player has pressed the "No" button in the "Return to Main Menu" confirmation section,
          the Pause Menu's first section is activated, and other parts are disabled
        - Also, the Pause Menu's index is set to 0, and the current button is defined by it
    */
    public void CancelReturnToMenu()
    {
        areYouSureMenu.SetActive(false);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        
        pauseMenuIndex = 0;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuButtons[pauseMenuIndex]);
    }

    /* 
        If the player has pressed the "Yes" button in the "Return to Main Menu" confirmation section,
        the time scale is set to 1, and the Main Menu scene is loaded, thus ending the game stage
    */
    public void AcceptReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}