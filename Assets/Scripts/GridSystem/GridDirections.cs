using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridDirections
{
   public static readonly GridPosition[] Diaognal = new GridPosition[]
      {
            new GridPosition(1,1),
            new GridPosition(1,-1),
            new GridPosition(-1,1),
            new GridPosition(-1,-1)
      };

    public static readonly GridPosition[] Orthogonal =
 {
        new GridPosition(0,1),
        new GridPosition(1,0),
        new GridPosition(0,-1),
        new GridPosition(-1,0)
    };

    public static readonly GridPosition Left = new GridPosition(-1, 0);
    public static readonly GridPosition Right = new GridPosition(1, 0);
    public static readonly GridPosition Up = new GridPosition(0, 1);
    public static readonly GridPosition Down = new GridPosition(0, -1);
}
