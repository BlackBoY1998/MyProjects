using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace crudusingEntity.Models
{
    public class SessionEncDec
    {
        public string EncSession(string strSession)
        {
            try
            {
                System.Security.Cryptography.DESCryptoServiceProvider md5Obj = new System.Security.Cryptography.DESCryptoServiceProvider();
                byte[] key = new byte[] { 146, 43, 41, 160, 64, 185, 185, 121 };
                md5Obj.Key = key;
                md5Obj.IV = key;
                ICryptoTransform desencrypt = md5Obj.CreateEncryptor();
                byte[] byteToHash = System.Text.Encoding.ASCII.GetBytes(strSession);
                byte[] result = desencrypt.TransformFinalBlock(byteToHash, 0, byteToHash.Length);
                return BitConverter.ToString(result);
            }
            catch (Exception ex)
            {

            }
            return "";
        }
        public string EncSession(byte[] arrSession)
        {
            try
            {
                System.Security.Cryptography.DESCryptoServiceProvider md5Obj = new System.Security.Cryptography.DESCryptoServiceProvider();
                byte[] key = new byte[] { 146, 43, 41, 160, 64, 185, 185, 121 };
                md5Obj.Key = key;
                md5Obj.IV = key;
                ICryptoTransform desencrypt = md5Obj.CreateEncryptor();
                // byte[] byteToHash = System.Text.Encoding.ASCII.GetBytes(strSession);
                byte[] result = desencrypt.TransformFinalBlock(arrSession, 0, arrSession.Length);
                return BitConverter.ToString(result);
            }
            catch (Exception ex)
            {

            }
            return "";
        }
        public byte[] DecSessionToArray(string strSession)
        {
            try
            {
                string[] arrayData = strSession.Split("-".ToCharArray());
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[arrayData.Length];
                for (int i = 0; i < arrayData.Length; i++)
                {
                    inputByteArray[i] = byte.Parse(arrayData[i], System.Globalization.NumberStyles.HexNumber);
                }

                des.Key = new byte[] { 146, 43, 41, 160, 64, 185, 185, 121 };
                des.IV = new byte[] { 146, 43, 41, 160, 64, 185, 185, 121 };
                ICryptoTransform desencrypt = des.CreateDecryptor();
                byte[] result = desencrypt.TransformFinalBlock(inputByteArray, 0, inputByteArray.Length);

                return result;
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        public string DecSession(string strSession)
        {
            try
            {
                string[] arrayData = strSession.Split("-".ToCharArray());
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[arrayData.Length];
                for (int i = 0; i < arrayData.Length; i++)
                {
                    inputByteArray[i] = byte.Parse(arrayData[i], System.Globalization.NumberStyles.HexNumber);
                }

                des.Key = new byte[] { 146, 43, 41, 160, 64, 185, 185, 121 };
                des.IV = new byte[] { 146, 43, 41, 160, 64, 185, 185, 121 };
                ICryptoTransform desencrypt = des.CreateDecryptor();
                byte[] result = desencrypt.TransformFinalBlock(inputByteArray, 0, inputByteArray.Length);

                return Encoding.UTF8.GetString(result);
            }
            catch (Exception ex)
            {

            }

            return "";
        }

    }
}