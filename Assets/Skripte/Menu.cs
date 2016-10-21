using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject btnSinglePlayer;
    public GameObject btnMultiPlayer;
    public GameObject btnSoundOnOff;
    public GameObject btnExit;
    
    private AudioSource audioSource;

    public static bool Sound = true;

    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.loop = true;

        btnSinglePlayer.GetComponent<Button>().onClick.AddListener(()=>{
            GameSetup.isMultiplayer = false;
            SceneManager.LoadScene(1);
        });

        btnMultiPlayer.GetComponent<Button>().onClick.AddListener(() => {
            GameSetup.isMultiplayer = true;
            SceneManager.LoadScene(1);
        });

        btnSoundOnOff.GetComponent<Button>().onClick.AddListener(() => {
            if (Sound)
                DisableSound();
            else
                EnableSound();

            Sound = !Sound;
        });
        btnExit.GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
        });
    }

    void DisableSound() {
        audioSource.Stop();
    }

    void EnableSound() {
        audioSource.Play();
    }

}
