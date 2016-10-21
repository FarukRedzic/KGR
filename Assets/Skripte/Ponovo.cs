using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ponovo : MonoBehaviour {

    public void Start() 
    {
        if (Menu.Sound)
            EnableSound();
        else DisableSound();
    }

    public void Rematch()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void DisableSound() {
        GetComponent<AudioListener>().enabled = false;
    }
    void EnableSound() {
        GetComponent<AudioListener>().enabled = true;
    }
}
