using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundAndMove
{
    //bounds of the main camera
    public static Vector2 bounds = new Vector2(8f, 4f);

    //function used to create torus effect/wraping movement
    public static Vector2 WrapMovement(Vector2 pos) {

        if (Mathf.Abs(pos.x) > bounds.x)
        {
            pos.x *= -1f;

            //if I dont add this the player 
            //bounces between boarders
            pos.x *= .95f;
        }

        if (Mathf.Abs(pos.y) > bounds.y)
        {
            pos.y *= -1f;

            //if I dont add this the player 
            //bounces between boarders
            pos.y *= .95f;
        }

        return pos;
    }
 


}
