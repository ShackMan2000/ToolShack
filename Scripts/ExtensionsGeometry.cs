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
}
