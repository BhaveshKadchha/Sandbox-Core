using UnityEngine;

namespace Sandbox.Helper
{
    public sealed class Bezier
    {
        public static Vector3 GetPoint(Vector3 origin, Vector3 target, Vector3 control, float t)
        {
            Vector3 p1 = Vector3.Lerp(origin, control, t);
            Vector3 p2 = Vector3.Lerp(control, target, t);
            return Vector3.Lerp(p1, p2, t);
        }

        public static Vector3 GetPoint(Vector3 origin, Vector3 target, Vector3 controlOne, Vector3 controlTwo, float t)
        {
            Vector3 p1 = GetPoint(origin, controlTwo, controlOne, t);
            Vector3 p2 = GetPoint(controlOne, target, controlTwo, t);
            return Vector3.Lerp(p1, p2, t);
        }

        public static float Length(Vector2 origin, Vector2 target, Vector2 control)
        {
            Vector2 v = 2 * (target - origin);
            Vector2 w = control - 2 * target + origin;

            float uu = 4 * (w.x * w.x + w.y * w.y);

            if (uu < 0.00001)
                return (float)Mathf.Sqrt((control.x - origin.x) * (control.x - origin.x)
                    + (control.y - origin.y) * (control.y - origin.y));

            float ww = v.x * v.x + v.y * v.y;
            float vv = 4 * (v.x * w.x + v.y * w.y);

            float t1 = (float)(2 * Mathf.Sqrt(uu * (uu + vv + ww)));
            float t2 = 2 * uu + vv;
            float t3 = vv * vv - 4 * uu * ww;
            float t4 = (float)(2 * Mathf.Sqrt(uu * ww));

            return (float)((t1 * t2 - t3 * Mathf.Log(t2 + t1) - (vv * t4 - t3 * Mathf.Log(vv + t4))) / (8 * Mathf.Pow(uu, 1.5f)));
        }

        public static float Length(Vector3 origin, Vector3 target, Vector3 control)
        {
            Vector3 v = 2 * (target - origin);
            Vector3 w = control - 2 * target + origin;

            float uu = 4 * (w.x * w.x + w.y * w.y + w.z * w.z);

            if (uu < 0.00001)
                return (float)Mathf.Sqrt((control.x - origin.x) * (control.x - origin.x)
                    + (control.y - origin.y) * (control.y - origin.y)
                    + (control.z - origin.z) * (control.z - origin.z));

            float ww = (v.x * v.x) + (v.y * v.y) + (v.z * v.z);
            float vv = 4 * (v.x * w.x + v.y * w.y + v.z * w.z);

            float t1 = (float)(2 * Mathf.Sqrt(uu * (uu + vv + ww)));
            float t2 = 2 * uu + vv;
            float t3 = vv * vv - 4 * uu * ww;
            float t4 = (float)(2 * Mathf.Sqrt(uu * ww));

            return (float)((t1 * t2 - t3 * Mathf.Log(t2 + t1) - (vv * t4 - t3 * Mathf.Log(vv + t4))) / (8 * Mathf.Pow(uu, 1.5f)));
        }
    }
}