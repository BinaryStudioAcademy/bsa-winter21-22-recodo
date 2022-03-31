using Microsoft.Win32;

namespace Recodo.Desktop.Logic
{
    public class RegistryHelper
    {
        public static void SaveToken(string token)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Recodo");
            key.SetValue("token", token);
            key.Close();
        }
        
        public static void DeleteToken()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Recodo", true);
            if(key is not null)
            {
                key.DeleteValue("token");
            }
        }
    }
}
