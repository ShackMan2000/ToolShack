using System.Collections.Generic;
using UnityEngine;

public static class ExtensionsGeometry
{
    
   
     public static Vector3 AdjustBy(this Vector3 vec, float? x = null, float? y = null, float? z = null)
     {
         float newX = x == null ? vec.x : vec.x + (float)x;
         float newY = y == null ? vec.y : vec.y + (float)y;
         float newZ = z == null ? vec.z : vec.z + (float)z;

         return new Vector3(newX, newY, newZ);
     }

     public static Vector3 Set(this Vector3 vec, float? x = null, float? y = null, float? z = null)
     {
         float newX = x ?? vec.x;
         float newY = y ?? vec.y;
         float newZ = z ?? vec.z;

         return new Vector3(newX, newY, newZ);
     }


     public static Vector3 DirectionTo(this Vector3 origin, Vector3 target)
     {
         return Vector3.Normalize(target - origin);
     }

     public static Vector3 DirectionTo(this Transform origin, Transform target)
     {
         return origin.position.DirectionTo(target.position);
     }



     public static Vector3 Random(this Vector3 original, float min, float max)
     {

         return new Vector3(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));
     }


     public static Object GetByChance(this Dictionary<Object, int> dic)
     {
         List<Object> list = new List<Object>();

         foreach (var item in dic)
         {
             for (int i = 0; i < item.Value; i++)
             {
                 list.Add(item.Key);
             }
         }

         if (list.Count > 0)
         {
             return list[UnityEngine.Random.Range(0, list.Count)];
         }
         else
         {
             Debug.Log("Trying to pick from a dictionary that doesn't have any entries");
             return null;
         }
     }




     public static float GetHueDifference(this float hueA, float hueB)
     {
         float dif = Mathf.Abs(hueA - hueB);
         if (dif > 0.5f)
             dif = 1f - dif;

         return dif * 2f;
     }


   
}
