using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security;

namespace EDIVisualizer.Classes
{
    class Settings
    {

        //http://weblogs.asp.net/jongalloway/encrypting-passwords-in-a-net-app-config-file
        public String SPLogin { get; set; }
        public SecureString SPMdp { get; set; }
        byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Hutchinson Group");

        private string SettingsFilePath
        {
            get
            {
                string EnvironmentFolder = string.Format(@"{0}\{1}", System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData), Application.ProductName);
                if (!Directory.Exists(EnvironmentFolder))
                    Directory.CreateDirectory(EnvironmentFolder);
                return Path.Combine(EnvironmentFolder, "EDIVisualizer.Settings");
            }
        }

        public Settings()
        {
            if (File.Exists(SettingsFilePath))
                this.GetInfos();
        }

        private void GetInfos()
        {
            using (StreamReader file = new StreamReader(SettingsFilePath))
            { 
                SPLogin = DecryptString(file.ReadLine());
                SPMdp = DecryptSecureString(file.ReadLine());
            }
        }

        internal void Save(string lg, string ps)
        {
            string eLogin = EncryptString(ToSecureString(lg));
            string ePass = EncryptString(ToSecureString(ps));
            if (File.Exists(SettingsFilePath))
                File.Delete(SettingsFilePath);
            using (StreamWriter writer = new StreamWriter(SettingsFilePath))
            {
                writer.WriteLine(eLogin);
                writer.WriteLine(ePass);
            }
        }

        #region Security

        private string EncryptString(System.Security.SecureString input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        private SecureString DecryptSecureString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        private String DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return System.Text.Encoding.Unicode.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        private SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
                secure.AppendChar(c);
            secure.MakeReadOnly();
            return secure;
        }

        private static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        #endregion


        internal void Delete()
        {
            if (File.Exists(SettingsFilePath))
                File.Delete(SettingsFilePath);
        }
    }
}
