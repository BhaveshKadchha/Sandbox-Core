using System.Text;
using UnityEngine;
using System.Security.Cryptography;

namespace Sandbox.Helper
{
    // Encrypted PlayerPrefs
    // Written by Sven Magnus
    // MD5 code by Matthew Wegner (from [url]http://www.unifycommunity.com/wiki/index.php?title=MD5[/url])
    // Added SetBool and GetBool by Bhavesh Kadchha.

    public class EncryptedPlayerPrefs
    {
        private static string privateKey = "9ETrEsWaFRach3gexaDr";
        private static string[] keys = new string[] { "23Wrudre", "SP9DupHa", "frA5rAS3", "tHat2epr", "jaw3eDAs" };

        public static string Md5(string strToEncrypt)
        {
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);

            MD5 md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(bytes);

            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }
        public static void SaveEncryption(string key, string type, string value)
        {
            int keyIndex = (int)Mathf.Floor(Random.Range(0f, 1f) * keys.Length);
            string secretKey = keys[keyIndex];
            string check = Md5($"{key}_{type}_{privateKey}_{secretKey}_{value}");
            PlayerPrefs.SetString($"{key}_encryption_check", check);
            PlayerPrefs.SetInt($"{key}_used_key", keyIndex);
        }
        public static bool CheckEncryption(string key, string type, string value)
        {
            int keyIndex = PlayerPrefs.GetInt(key + "_used_key");
            string secretKey = keys[keyIndex];
            string check = Md5($"{key}_{type}_{privateKey}_{secretKey}_{value}");
            if (!PlayerPrefs.HasKey($"{key}_encryption_check")) return false;
            string storedCheck = PlayerPrefs.GetString($"{key}_encryption_check");
            return storedCheck == check;
        }

        public static void ModifyPrivateKey(string newPrivateKey)
        {
            if (string.IsNullOrWhiteSpace(newPrivateKey))
                Debug.LogError("Empty Private Key. Default Private Key will be used.");
            else
                privateKey = newPrivateKey;
        }
        public static void ModifySecretKey(string[] newSecretKey)
        {
            if (newSecretKey.Length == 0)
                Debug.LogError("Empty Secret Key Array. Default Secret Key will be used.");
            else
                keys = newSecretKey;
        }
        public static void ModifySecretKey(string newSecretKey)
        {
            if (newSecretKey.Length == 0)
                Debug.LogError("Empty Secret Key. Default Secret Key will be used.");
            else
                keys = new string[] { newSecretKey };
        }


        public static void SetBool(string key, bool value)
        {
            int val = value ? 1 : 0;
            PlayerPrefs.SetInt(key, val);
            SaveEncryption(key, "bool", value.ToString());
        }
        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            SaveEncryption(key, "int", value.ToString());
        }
        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            SaveEncryption(key, "float", Mathf.Floor(value * 1000).ToString());
        }
        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            SaveEncryption(key, "string", value);
        }


        public static bool GetBool(string key) => GetBool(key, false);
        public static int GetInt(string key) => GetInt(key, 0);
        public static float GetFloat(string key) => GetFloat(key, 0f);
        public static string GetString(string key) => GetString(key, "");

        public static bool GetBool(string key, bool defaultValue)
        {
            bool value = PlayerPrefs.GetInt(key) == 0 ? false : true;
            if (!CheckEncryption(key, "bool", value.ToString())) return defaultValue;
            return value;
        }
        public static int GetInt(string key, int defaultValue)
        {
            int value = PlayerPrefs.GetInt(key);
            if (!CheckEncryption(key, "int", value.ToString())) return defaultValue;
            return value;
        }
        public static float GetFloat(string key, float defaultValue)
        {
            float value = PlayerPrefs.GetFloat(key);
            if (!CheckEncryption(key, "float", Mathf.Floor(value * 1000).ToString())) return defaultValue;
            return value;
        }
        public static string GetString(string key, string defaultValue)
        {
            string value = PlayerPrefs.GetString(key);
            if (!CheckEncryption(key, "string", value)) return defaultValue;
            return value;
        }


        public static bool HasKey(string key) => PlayerPrefs.HasKey(key);
        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.DeleteKey($"{key}_encryption_check");
            PlayerPrefs.DeleteKey($"{key}_used_key");
        }
    }
}