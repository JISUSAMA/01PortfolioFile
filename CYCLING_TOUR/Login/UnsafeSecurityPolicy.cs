using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;




public class UnsafeSecurityPolicy : MonoBehaviour
{
    public static bool Validator (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
    {
        //Debug.Log("Validation successful!");
        return true;
    }

    public static void Instate()
    {
        ServicePointManager.ServerCertificateValidationCallback = Validator;
    }
}
