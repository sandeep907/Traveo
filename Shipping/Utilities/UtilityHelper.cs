using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using static Shipping.Utilities.Enums;

namespace Shipping.Utilities
{
    public class UtilityHelper
    {
        public string EncrpytPassword(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public int ConvertStringToInteger(string pString)
        {
            int returnInteger = 0;
            int.TryParse(pString, out returnInteger);
            return returnInteger;
        }
        public string RemoveWhiteSpace(string pString)
        {
            if (string.IsNullOrEmpty(pString))
            {
                return string.Empty;
            }
            else
            {
                return pString.Replace(" ", string.Empty);
            }
        }
        public decimal ConvertStringToDecimal(string pString)
        {
            decimal returnInteger = default(decimal);
            decimal.TryParse(pString, out returnInteger);
            return returnInteger;
        }
        public double ConvertStringToDouble(string pString)
        {
            double returnInteger = default(double);
            double.TryParse(pString, out returnInteger);
            return returnInteger;
        }
        public string GenerateMessage(string message, MessageType messagetype)
        {
            switch (messagetype)
            {
                case MessageType.Error:
                    return "<div class='alert alert-danger alert-dismissable' style='padding:4px'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" + message + "</div>";

                case MessageType.Success:
                    return "<div class='alert alert-success text-center alert-dismissable' style='padding:4px'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" + message + "</div>";

                default:
                    return "<div class='alert alert-danger alert-dismissable'  style='padding:4px'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" + message + "</div>";
            }
        }
        public int GetRandomNumber()
        {
            Random r = new Random();
            return r.Next(1000, 5000);
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static string EncodeEmailToBase64(string email)
        {
            try
            {
                byte[] encData_byte = new byte[email.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(email);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string DecodeFrom64(string email)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(email);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
