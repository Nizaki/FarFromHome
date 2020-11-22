using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


static class Extension
{
  public static Vector2Int ToInt2(this Vector2 v)
  {
    return new Vector2Int((int)v.x, (int)v.y);
  }
  public static Vector3Int ToInt3(this Vector2 v)
  {
    return new Vector3Int((int)v.x, (int)v.y,0);
  }
  public static Vector3Int ToInt(this Vector3 v)
  {
    return new Vector3Int((int)v.x, (int)v.y, (int)v.z);
  }
}

