using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void MainMenu()
    {
        // Added by C-Hurrell

        GameObject.Destroy(GameObject.FindGameObjectWithTag("GameManager"));
        SceneManager.LoadScene("MainMenu");
    }
}
