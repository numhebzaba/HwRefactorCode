using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckData 
{
    public Vector2 left;
    public Vector2 center;
    public Vector2 right;

    public bool grounded1;
    public bool grounded2;
    public bool grounded3;

    public GroundCheckData(Vector2 left, Vector2 center, Vector2 right, bool grounded1, bool grounded2, bool grounded3)
    {
        this.left = left;
        this.center = center;   
        this.right = right;
        this.grounded1 = grounded1; 
        this.grounded2 = grounded2;
        this.grounded3 = grounded3;
    }
}
