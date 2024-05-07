using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FurnApp_API.Helper
{
    public static class Validation
    {
        public static bool IsValidEmail(string email)
        {
            // E-posta adresi formatını kontrol eden bir Regex deseni
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // E-posta adresinin formatı doğru mu kontrol et
            if (!Regex.IsMatch(email, pattern))
            {
                return false;
            }

            // Geriye doğru bir e-posta adresi olduğunu belirt
            return true;
        }

    }
}
