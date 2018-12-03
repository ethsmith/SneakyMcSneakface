using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISenses : MonoBehaviour {

    // The field of view for eye sight
    public float fieldOfView = 90f;
    // The bool for whether the player is in rage of sight
    public bool playerInRange;
    // The last known location of the enemy when it was heard
    public Vector3 lastKnownLoc;
    // The bool for whether or not the enemy is chasing the player
    public bool isChasing = false;
    // The hearing range of the enemy
    public float hearingRange = 10.0f;

    // How far the line of sight is
    private Transform lineOfSightEnd;
    // a reference to the player for raycasting
    private GameObject player;
    // The pawn for the enemy
    private AIPawn enemy;

    // Use this for initialization
    void Start () {
        playerInRange = false;
        player = GameManager.instance.player.gameObject;
        enemy = GameManager.instance.enemy;
        lineOfSightEnd = player.transform;
    }

    // Update is called once per frame
    void Update () {
        // Check every frame if the player is seen
        CanPlayerBeSeen();
	}

    // Check if the enemy ai can see the player
    bool CanPlayerBeSeen() {
        // Is the player in range of sight
        if (playerInRange) {
            // Is the player in the field of view
            if (PlayerInFieldOfView()) {
                // Is the player not hidden by obstacles such as trees
                if (!PlayerHiddenByObstacles()) {
                    // set chasing to true
                    isChasing = true;
                    // change the ai state to chase
                    GameManager.instance.enemy.ChangeState("chase");
                } else {
                    // set chasing to false
                    isChasing = false;
                    // set the ai state to search
                    GameManager.instance.enemy.ChangeState("search");
                }
                // return whether or not the player can be seen based on obstacles
                return !PlayerHiddenByObstacles();
            } else {
                // set chasing to false
                isChasing = false;
                // set the ai state to search
                GameManager.instance.enemy.ChangeState("search");
                return false;
            }
        } else {
            // set chasing to false
            isChasing = false;
            // set the ai state to search
            GameManager.instance.enemy.ChangeState("search");
            return false;
        }

    }

    // Check if something is within the enemy ai's eye collider
    void OnTriggerStay2D(Collider2D other) {
        // if it is the hero, the player is in range
        if (other.gameObject.name == "Hero") {
            playerInRange = true;
        }
    }

    // Check if something exits the enemy ai's eye collider
    void OnTriggerExit2D(Collider2D other) {
        // if it is the hero, the player is not in range anymore
        if (other.gameObject.name == "Hero") {
            playerInRange = false;
        }
    }

    bool PlayerInFieldOfView() {
        // check if the player is within the enemy's field of view
        // this is only checked if the player is within the enemy's sight range

        // find the angle between the enemy's 'forward' direction and the player's location and return true if it's within 65 degrees (for 130 degree field of view)

        Vector2 directionToPlayer = player.transform.position - transform.position; // represents the direction from the enemy to the player    
        Debug.DrawLine(transform.position, player.transform.position, Color.magenta); // a line drawn in the Scene window equivalent to directionToPlayer

        Vector2 lineOfSight = lineOfSightEnd.position - transform.position; // the centre of the enemy's field of view, the direction of looking directly ahead
        Debug.DrawLine(transform.position, lineOfSightEnd.position, Color.yellow); // a line drawn in the Scene window equivalent to the enemy's field of view centre

        // calculate the angle formed between the player's position and the centre of the enemy's line of sight
        float angle = Vector2.Angle(directionToPlayer, lineOfSight);

        // if the player is within 65 degrees (either direction) of the enemy's centre of vision (i.e. within a 130 degree cone whose centre is directly ahead of the enemy) return true
        if (angle < fieldOfView)
            return true;
        else
            return false;
    }

    bool PlayerHiddenByObstacles() {

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, player.transform.position - transform.position, distanceToPlayer);
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.blue); // draw line in the Scene window to show where the raycast is looking
        //List<float> distances = new List<float>();

        foreach (RaycastHit2D hit in hits) {

            // ignore the enemy's own colliders (and other enemies)
            if (hit.collider.gameObject.name == "Slender")
                continue;

            // if anything other than the player is hit then it must be between the player and the enemy's eyes (since the player can only see as far as the player)
            if (hit.collider.gameObject.name != "Hero") {
                return true;
            }
        }

        // if no objects were closer to the enemy than the player return false (player is not hidden by an object)
        return false;
    }

    public bool CanHear(PlayerPawn target) {
        // The game object of the player
        GameObject player = GameManager.instance.player.gameObject;

        // check if the distance between the player and the enemy is less than hearing range
        if (Mathf.Abs(Vector3.Distance(transform.position, player.transform.position)) < hearingRange) {
            // set last known location
            lastKnownLoc = player.transform.position;
            // start chasing the player
            isChasing = true;
            enemy.ChangeState("chase");
            return true;
        }
        // search for the player
        enemy.ChangeState("search");
        return false;
    }
}
