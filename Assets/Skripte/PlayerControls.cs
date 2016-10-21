using UnityEngine;
using System.Collections;


public class PlayerControls : MonoBehaviour {

    public Rigidbody2D rb = new Rigidbody2D();
    public bool isEnemy = false;
    public Transform ball;
    public KeyCode moveUp = new KeyCode();
    public KeyCode moveDown = new KeyCode();

    float speed = 10;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (isEnemy == false) {
            if (Input.GetKey(moveUp))
                rb.velocity = new Vector3(0, speed, 0);

            else if (Input.GetKey(moveDown))
                rb.velocity = new Vector3(0, speed * -1, 0);

            else
                rb.velocity = new Vector3(0, 0, 0);
        }
        else this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x,ball.position.y),Time.deltaTime * 5);
    }
}