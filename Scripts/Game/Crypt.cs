using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;
using System.Threading;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
public class Crypt : MonoBehaviour
{
    private static byte[] iv = new byte[16] { 0xAF, 0x5C, 0x61, 0x49, 0xB2, 0xF, 0xFF, 0x33, 0x12, 0x0, 0x45, 0x12, 0x79, 0x82, 0x6D, 0x1E };
    private static string password = "53829EHY#%U*(fnAJLKS>F#(Q108";
    public static string AESEncrypt(string message)
    {
        System.Security.Cryptography.SHA256 mySHA256 = System.Security.Cryptography.SHA256Managed.Create();
        byte[] key = mySHA256.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));
        return EncryptString(message, key, iv);
    }
    public static string AESDecrypt(string encrypted)
    {
        System.Security.Cryptography.SHA256 mySHA256 = System.Security.Cryptography.SHA256Managed.Create();
        byte[] key = mySHA256.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));
        return DecryptString(encrypted, key, iv);
    }
    private static string EncryptString(string plainText, byte[] key, byte[] iv)
    {
        Aes encryptor = Aes.Create();
        encryptor.Mode = CipherMode.CBC;
        encryptor.Key = key;
        encryptor.IV = iv;
        MemoryStream memoryStream = new MemoryStream();
        ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);
        byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
        return cipherText;
    }
    private static string DecryptString(string cipherText, byte[] key, byte[] iv)
    {
        Aes encryptor = Aes.Create();
        encryptor.Mode = CipherMode.CBC;
        encryptor.Key = key;
        encryptor.IV = iv;
        MemoryStream memoryStream = new MemoryStream();
        ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);
        string plainText = String.Empty;
        try
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] plainBytes = memoryStream.ToArray();
            plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
        }
        finally
        {
            memoryStream.Close();
            cryptoStream.Close();
        }
        return plainText;
    }

}
