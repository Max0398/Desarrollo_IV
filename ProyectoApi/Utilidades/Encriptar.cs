using ProyectoApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoApi.Utilidades
{
    public class Encriptar
    {
        public static string EncriptarPassword(string password)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data=md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb=   new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();   
        }

        public static bool Check(string input, string hash)
        {
            if (EncriptarPassword(input).Equals(hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidarContrasenia(string password)
        {
            if (password.Any(char.IsDigit) == false || password.Any(char.IsLower) == false
                || password.Any(char.IsUpper) == false || password.Length < 8)
            {
                return false;
            }
            else
            {
                return true;
            }
              
            
        }

    }
}
