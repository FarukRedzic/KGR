using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BallControl : MonoBehaviour {

    public Rigidbody2D rb;
    public GameObject BodoviLijevoText;
    public GameObject BodoviDesnoText;
    public GameObject BodoviSinglePlayerText;
    int LijeviBodovi = 0;
    int DesniBodovi = 0;
    int SinglePlayerBodovi = 0;
    int HighScore = 0;

    public GameObject PanelButtonsEnd;
    public GameObject PanelInstructionsRightPlayers;
    public GameObject PanelHighScore;
    public Text HighScoreText;
    public GameObject LijeviText;
    public GameObject DesniText;
    public GameObject SinglePlayerText;
    public GameObject PauseImage;

    private AudioSource audioSourceKolizija;
    private AudioSource audioSourceGameOver;
    private AudioSource audioSourcePoint;
    private AudioSource BackgroundMusic;

    float tempTime = 1;
    bool isPaused = false;
    bool GameOver = false;


    IEnumerator Wait(float Time) {
        yield return new WaitForSeconds(Time);
        GoBall();
    }

    void ResetBall() {
        rb.velocity = Vector2.zero;
        Time.timeScale = 1;
        Vector2 position = new Vector2(0, 0);
        rb.transform.position = position;
        StartCoroutine(Wait(0.5f));
    }

    void GoBall() {
        float random = UnityEngine.Random.Range(0, 2);
        if (random <= 0.5)
            rb.AddForce(new Vector2(70, 20));
        else rb.AddForce(new Vector2(-70, -20));

    }

    void Start() {


        HighScore = PlayerPrefs.GetInt("HighScore");

        Text tmpHighScore = HighScoreText.GetComponent<Text>();
        tmpHighScore.text += HighScore.ToString();

        if (!GameSetup.isMultiplayer) {
            BodoviDesnoText.GetComponent<Text>().enabled = false;
            BodoviLijevoText.GetComponent<Text>().enabled = false;
            PanelInstructionsRightPlayers.SetActive(false);
        }

        else {
            BodoviSinglePlayerText.GetComponent<Text>().enabled = false;
            SinglePlayerText = GameObject.FindGameObjectWithTag("SinglePlayerText");
            PanelHighScore.SetActive(false);
        }

        StartCoroutine(Wait(2));
        GameOver = false;

        var nizZvukova = GetComponents<AudioSource>();
        audioSourceKolizija = nizZvukova[0];
        audioSourceGameOver = nizZvukova[1];
        BackgroundMusic = nizZvukova[2];
        audioSourcePoint = nizZvukova[3];
        BackgroundMusic.Play();
        BackgroundMusic.loop = true;


        if (!Menu.Sound)
            BackgroundMusic.Stop();

        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();

    }

    void OnCollisionEnter2D(Collision2D colInfo) {


        if (Time.timeScale < 4) {
            Time.timeScale *= 1.02f;
            tempTime *= 1.02f;
        }

        if (!GameSetup.isMultiplayer) {

            if (colInfo.collider.tag == "RightWall") {
                PanelButtonsEnd.SetActive(true);
                SinglePlayerText.SetActive(true);
                SinglePlayerText.GetComponent<Text>().enabled = true;
                SinglePlayerText.GetComponent<Text>().text += " " + SinglePlayerBodovi.ToString();

                Time.timeScale = 0;
                GameOver = true;
                if (Menu.Sound)
                    audioSourceGameOver.Play();

                if (SinglePlayerBodovi > HighScore)
                    PlayerPrefs.SetInt("HighScore", SinglePlayerBodovi);
            }

            if (colInfo.collider.tag == "LeftWall") {
                PanelButtonsEnd.SetActive(true);
                SinglePlayerText.SetActive(true);
                SinglePlayerText.GetComponent<Text>().text += " " + SinglePlayerBodovi.ToString();

                Time.timeScale = 0;
                GameOver = true;

                if (Menu.Sound)
                    audioSourceGameOver.Play();

                if (SinglePlayerBodovi > HighScore)
                    PlayerPrefs.SetInt("HighScore", SinglePlayerBodovi);
            }

            if (colInfo.transform.tag == "PlayerLeft") {
                Text temp = BodoviSinglePlayerText.GetComponent<Text>();
                SinglePlayerBodovi++;
                temp.text = SinglePlayerBodovi.ToString();

                audioSourceKolizija.pitch = UnityEngine.Random.Range(1.0f, 1.3f);
                if (Menu.Sound)
                    audioSourceKolizija.Play();
            }

            if (colInfo.transform.tag == "PlayerRight") {

                audioSourceKolizija.pitch = UnityEngine.Random.Range(1.0f, 1.3f);
                if (Menu.Sound)
                    audioSourceKolizija.Play();
            }

        }
        //
        else {
            if (colInfo.collider.tag == "RightWall") {
                Text temp = BodoviLijevoText.GetComponent<Text>();
                LijeviBodovi++;
                temp.text = LijeviBodovi.ToString();

                if (Menu.Sound)
                    audioSourcePoint.Play();

                if (LijeviBodovi == 5) {
                    PanelButtonsEnd.SetActive(true);
                    DesniText.SetActive(true);
                    Time.timeScale = 0;
                    GameOver = true;

                    if (Menu.Sound)
                        audioSourceGameOver.Play();
                }
                else ResetBall();
            }
            if (colInfo.collider.tag == "LeftWall") {
                Text temp = BodoviDesnoText.GetComponent<Text>();
                DesniBodovi++;
                temp.text = DesniBodovi.ToString();

                if (Menu.Sound)
                    audioSourcePoint.Play();

                if (DesniBodovi == 5) {
                    PanelButtonsEnd.SetActive(true);
                    LijeviText.SetActive(true);
                    Time.timeScale = 0;
                    GameOver = true;

                    if (Menu.Sound)
                        audioSourceGameOver.Play();

                }
                else ResetBall();
            }

            if (colInfo.transform.tag == "PlayerLeft") {

                audioSourceKolizija.pitch = UnityEngine.Random.Range(1.0f, 1.3f);
                if (Menu.Sound)
                    audioSourceKolizija.Play();
            }

            if (colInfo.transform.tag == "PlayerRight") {
                ;
                audioSourceKolizija.pitch = UnityEngine.Random.Range(1.0f, 1.3f);
                if (Menu.Sound)
                    audioSourceKolizija.Play();

            }


        }

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    private void Pause() {
        if (GameOver)
            return;
        if (!isPaused) {
            Time.timeScale = 0;
            isPaused = true;
            PauseImage.SetActive(true);
            PanelButtonsEnd.SetActive(true);

        }
        else {
            Time.timeScale = tempTime;
            isPaused = false;
            PauseImage.SetActive(false);
            PanelButtonsEnd.SetActive(false);
        }
    }
}
