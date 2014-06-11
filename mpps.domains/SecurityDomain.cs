using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Specialized;

namespace pts.domain
{
    public static class SecurityDomain
    {
        private const String SECRET_KEY = "e0507f1e2d1040f589502e2f323b60a63925e8acd1064e66815984ef85827b16f9f6a127eb6a473d9c31cd7aff192f6c0b1ed69fd65449fdaa819c5534432c199dce82f9dd704fbfa28a41c4a037f9d58b3e68841b3a4e8d8ea13106f01cf65d4b1567fced0a4eed9cb1d5e9773fc8201de08062e00244c7840494d4378ba784";

        public static String sign(IDictionary<string, string> paramsArray, String secretKey)
        {
            return sign(buildDataToSign(paramsArray), secretKey);
        }

        public static String sign(String data, String secretKey) {
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        private static String buildDataToSign(IDictionary<string,string> paramsArray) {
            String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

	        foreach (String signedFieldName in signedFieldNames)
	        {
	             dataToSign.Add(signedFieldName.Trim() + "=" + paramsArray[signedFieldName].Trim());
	        }

            return commaSeparate(dataToSign);
        }

        private static String commaSeparate(IList<string> dataToSign) {
            return String.Join(",", dataToSign);                         
        }
    }
}
