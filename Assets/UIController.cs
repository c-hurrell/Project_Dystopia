using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    
    [Header("On start of scene Dialogue")]
    #region Start Dialogue
    [SerializeField] private string[] startDialogue = {"ME: This can't be real. I must be in a simulation. I need to find a way out.",
        "ME: If this is a simulation, then there must be an exit point somewhere. I have to find it.",
        "ME: I don't belong here. I need to break free from this simulation and find my way back to reality."};
    #endregion
    
    
    
    
    // Start is called before the first frame update
    private void Start()
    {
        Dialog.DialogManager.ShowDialog(startDialogue);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    
    // Pause Menu Functions
    #region Pause Menu Functions
    private void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void Quit()
    {
        //SceneManager.LoadScene("MainMenu");
    }
    #endregion
    
}
