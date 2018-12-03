using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller {

    public float speed;

    // Transform component of the pawn
    private Transform pawnTransform;
    // The AISenses component for access to hearing
    private AISenses senses;

    // Use this for initialization
    void Start() {
        pawnTransform = pawn.transform;
        senses = GameObject.Find("Eyes").GetComponent<AISenses>();
    }

    // Update is called once per frame
    void Update() {
        move();
        //enemy.CanSee(gameObject);
    }

    /*
     * Handle Movement
     */
    public override void move() {
        PlayerPawn playerPawn = (PlayerPawn)pawn;
        // Pressing W will move up
        if (Input.GetKey(KeyCode.W)) {
            if (!senses.isChasing && senses.playerInRange) {
                senses.CanHear(playerPawn);
            }
            pawnTransform.Translate(0, Time.deltaTime * speed, 0);
            // Pressing A will move left
        } else if (Input.GetKey(KeyCode.A)) {
            if (!senses.isChasing && senses.playerInRange) {
                senses.CanHear(playerPawn);
            }
            pawnTransform.Translate(Time.deltaTime * -speed, 0, 0);
            // Pressing S will move down
        } else if (Input.GetKey(KeyCode.S)) {
            if (!senses.isChasing && senses.playerInRange) {
                senses.CanHear(playerPawn);
            }
            pawnTransform.Translate(0, Time.deltaTime * -speed, 0);
            // Pressing D will move right
        } else if (Input.GetKey(KeyCode.D)) {
            if (!senses.isChasing && senses.playerInRange) {
                senses.CanHear(playerPawn);
            }
            pawnTransform.Translate(Time.deltaTime * speed, 0, 0);
        }
    }
}
