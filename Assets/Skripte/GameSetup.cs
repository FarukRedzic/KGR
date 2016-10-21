using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {

    public Camera mainCam;
    public BoxCollider2D topWall;
    public BoxCollider2D bottomWall;
    public BoxCollider2D leftWall; 
    public BoxCollider2D rightWall;

    public GameObject playerLeft;
    public GameObject playerRight;

    public static bool isMultiplayer = false;

    // Use this for initialization
    void Start() {
        var p2 = (PlayerControls)playerRight.GetComponent(typeof(PlayerControls));
        if (isMultiplayer)  
            p2.isEnemy = false;
        else 
            p2.isEnemy = true;
	}
}
