using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridDirections
{
   public static GridPosition[] Diaognal = new GridPosition[]
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
}
