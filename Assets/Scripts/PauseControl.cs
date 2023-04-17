using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{

    public static bool isPaused;

    public GameObject pauseMenuCollective;

    public GameObject pauseMenu, optionsMenu, areYouSureMenu;

    private AudioSource[] audioSources;

    public GameObject[] pauseMenuButtons;

    public GameObject[] optionsMenuButtons;

    public GameObject[] returnConfirmationButtons;

    public GameObject currentSelectedButton;

    private int pauseMenuIndex = 0;

    private int optionsMenuIndex = 0;

    private int returnMainMenuIndex = 0;


    void Start()
    {
        pauseMenuCollective.GetComponent<Canvas>().enabled = false;
        audioSources = FindObjectsOfType<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 

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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                OpenPauseMenu();
            }

            // Navigation in the first Pause Menu Canvas
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


            // Navigation in the Options section of the menu canvas
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

            // Navigation in the second Pause Menu Canvas ("Are you sure you want to return to main menu?")
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

            if (isPaused)
            {
                currentSelectedButton.GetComponent<ButtonSound>().ActivateSound();
            }
        }
    }

    void OpenPauseMenu()
    {
        if (pauseMenuCollective && isPaused)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.Pause();
            }

            Time.timeScale = 0f;
            pauseMenuCollective.GetComponent<Canvas>().enabled = true;
            pauseMenu.SetActive(true);
            
            pauseMenuIndex = 0;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseMenuButtons[pauseMenuIndex]);
            currentSelectedButton = pauseMenuButtons[pauseMenuIndex];
            currentSelectedButton.GetComponent<ButtonSound>().Reset();
        }
        else
        {
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

     public void ExitOptionsMenu()
    {
        areYouSureMenu.SetActive(false);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        
        pauseMenuIndex = 0;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuButtons[pauseMenuIndex]);
    }

    public void ContinuePlaying()
    {
        isPaused = false;
        OpenPauseMenu();
    }

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

    public void CancelReturnToMenu()
    {
        areYouSureMenu.SetActive(false);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        
        pauseMenuIndex = 0;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuButtons[pauseMenuIndex]);
    }

    public void AcceptReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}