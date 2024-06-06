using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

namespace Sandbox.Helper
{
    public static class Extension
    {
        static Color colorHolder = new Color();

        public static void Shuffle<T>(this T[] list)
        {
            System.Random rnd = new System.Random();
            int n = list.Length;
            while (n > 1)
            {
                int k = rnd.Next(n--);
                T temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }
        public static void Shuffle<T>(this List<T> list)
        {
            System.Random rnd = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                int k = rnd.Next(n--);
                T temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }

        public static string ToTime(this int timeInSecond)
        {
            int hours = timeInSecond / 3600;
            int minutes = (timeInSecond % 3600) / 60;
            int seconds = timeInSecond % 60;

            if (hours > 0)
                return $"{hours:00}h {minutes:00}m {seconds:00}s";
            else if (minutes > 0)
                return $"{minutes:00}m {seconds:00}s";
            else
                return $"{seconds:00}s";
        }

        public static Bounds OrthographicBounds(this Camera camera)
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = camera.orthographicSize * 2;
            Bounds bounds = new Bounds(camera.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
            return bounds;
        }

        public static Sprite ConvertTexToSprite(this Texture2D tex) => Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

        public static void SetAlphaValue(this MaskableGraphic uiItem, float alphaValue)
        {
            colorHolder = uiItem.color;
            colorHolder.a = alphaValue;
            uiItem.color = colorHolder;
        }
        public static void SetAlphaValueZero(this MaskableGraphic uiItem) => uiItem.SetAlphaValue(0);
        public static void SetAlphaValueOne(this MaskableGraphic uiItem) => uiItem.SetAlphaValue(1);
    }
}