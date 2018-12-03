using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller {

    // Move speed of the ai
    public float moveSpeed = 1f;
    // Rotation speed of the ai
    public float rotationSpeed = 1f;

    // The enemy pawn
    private AIPawn enemyPawn;
    // The player pawn
    private PlayerPawn playerPawn;
    // The transform of the enemy ai
    private Transform aiTransform;
    // The transform of the player
    private Transform target;

    // Use this for initialization
    void Start () {
		enemyPawn = (AIPawn)pawn;
        playerPawn = (PlayerPawn)GetComponent<PlayerController>().pawn;

        aiTransform = enemyPawn.GetComponent<Transform>();
        target = playerPawn.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyPawn.aiState == "search") {
            search();
        } else if (enemyPawn.aiState == "chase") {
            move();
        }
	}

    /*
     * Handle Movement
    */
    public override void move() {
        Vector3 vectorToTarget = target.position - aiTransform.position;
        aiTransform.position += vectorToTarget.normalized * moveSpeed * Time.deltaTime;
    }

    public void search() {
        if (aiTransform.rotation.y == -180) {
            aiTransform.Rotate(0f, 180f * Time.deltaTime * rotationSpeed, 0f);
        } else {
            aiTransform.Rotate(0f, -180f * Time.deltaTime * rotationSpeed, 0f);
        }
    }
}
