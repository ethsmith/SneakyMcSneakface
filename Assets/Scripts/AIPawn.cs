using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPawn : Pawn {

    public string aiState;

    // Use this for initialization
    void Start() {
        aiState = "search";
    }

    public void ChangeState(string newState) {
        // Change the ai state
        aiState = newState;
    }









































    /*
    public bool CanSee(GameObject target) {
        // We use the location of our target in a number of calculations - store it in a variable for easy access.
        Transform targetTf = target.GetComponent<Transform>();
        Vector3 targetPosition = targetTf.position;

        // Find the vector from the agent to the target
        // We do this by subtracting "destination minus origin", so that "origin plus vector equals destination."
        Vector3 agentToTargetVector = targetPosition - transform.position;

        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToTarget = Vector3.Angle(agentToTargetVector, transform.forward);

        // if that angle is less than our field of view
        if (angleToTarget < fieldOfView) {
            // Raycast
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, agentToTargetVector);

            if (hitInfo.collider != null) {
                // If the first object we hit is our target 
                if (hitInfo.collider.gameObject == target) {
                    lastKnownLoc = targetTf.position;
                    canSee = true;
                    isChasing = true;
                    Debug.Log("Saw player!");
                    ChangeState("chase");
                    return true;
                } else {
                    canSee = false;
                    isChasing = false;
                    ChangeState("search");
                    Debug.Log("Saw " + target.name + " instead...");
                }
            }
        }
        return false;
    }
    */
}
