using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class StrUnitl
{

    public static string EncodeSHA1(string str)
    {
        var sha1 = new SHA1CryptoServiceProvider();
        byte[] str01 = Encoding.Default.GetBytes(str);
        byte[] str02 = sha1.ComputeHash(str01);
        var pass = BitConverter.ToString(str02).Replace("-", "");
        return pass;
    }

}
