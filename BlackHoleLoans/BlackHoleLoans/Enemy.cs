using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackHoleLoans
{
  class Enemy : Entity
  {
    protected int[] path // The Enemy will move in the directions indicated by the ints in order and loop back.
    {                    // Closed loops are suggested, but the program does not enforce this!
      get;
      set;
    }

    protected int path_index = 0;

    public Enemy(OverWorld ow, Tile t, int[] newpath)
      : base(ow, t)
    {
      path_index = 0;
      path = newpath;
    }

    public Enemy(OverWorld ow, Tile t, int f, int[] newpath)
      : base(ow, t, f)
    {
      path_index = 0;
      path = newpath;
    }

    public override void OnCollision()
    {
      //run combat encounter
      Console.Write("Entered combat with entity by collision!\n");
    }

    public override void OnInteract()
    {
      //run combat encounter
      Console.Write("Entered combat with entity by interaction!\n");
    }

    public override void OnUpdate()
    {
      if (MoveAdjacent(path[path_index]))
      {
        path_index++;
        if (path_index >= path.Length)
        {
          path_index = 0;
        }
      }
    }

  }
}
