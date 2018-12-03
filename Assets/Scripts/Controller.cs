using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour {
    
    // The pawn connected to the controller
    public Pawn pawn;

    // The move function to be implemented across all controllers
    public abstract void move();
}
