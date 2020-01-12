using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace VLC_web_protocol
{
    class URISchema
    {

        public static bool Register(string protocol, string application, string arguments, RegistryKey registry)
        {

            RegistryKey cl = Registry.ClassesRoot.OpenSubKey(protocol);

            try
            {
                RegistryKey r;
                r = registry.OpenSubKey("SOFTWARE\\Classes\\" + protocol, true);
                if (r == null)
                    r = registry.CreateSubKey("SOFTWARE\\Classes\\" + protocol);
                r.SetValue("", "URL: Protocol handled by VLC");
                r.SetValue("URL Protocol", "");
                r.SetValue("CustomUrlApplication", application);
                r.SetValue("CustomUrlArguments", arguments);

                r = registry.OpenSubKey("SOFTWARE\\Classes\\" + protocol + "\\DefaultIcon", true);
                if (r == null)
                    r = registry.CreateSubKey("SOFTWARE\\Classes\\" + protocol + "\\DefaultIcon");
                r.SetValue("", application);

                r = registry.OpenSubKey("SOFTWARE\\Classes\\" + protocol + "\\shell\\open\\command", true);
                if (r == null)
                    r = registry.CreateSubKey("SOFTWARE\\Classes\\" + protocol + "\\shell\\open\\command");

                r.SetValue("", application + " \"%1\"");


                // If 64-bit OS, also register in the 32-bit registry area. 
                if (registry.OpenSubKey("SOFTWARE\\Wow6432Node\\Classes") != null)
                {
                    r = registry.OpenSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol, true);
                    if (r == null)
                        r = registry.CreateSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol);
                    r.SetValue("", "URL: Protocol handled by CustomURL");
                    r.SetValue("URL Protocol", "");
                    r.SetValue("CustomUrlApplication", application);
                    r.SetValue("CustomUrlArguments", arguments);

                    r = registry.OpenSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol + "\\DefaultIcon", true);
                    if (r == null)
                        r = registry.CreateSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol + "\\DefaultIcon");
                    r.SetValue("", application);

                    r = registry.OpenSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol + "\\shell\\open\\command", true);
                    if (r == null)
                        r = registry.CreateSubKey("SOFTWARE\\Wow6432Node\\Classes\\" + protocol + "\\shell\\open\\command");

                    r.SetValue("", application + " \"%1\"");

                }

                //whitelist scheme to prevent chrome confirmation
                r = registry.OpenSubKey("SOFTWARE\\Policies\\Google\\Chrome\\URLWhitelist", true);
                if (r == null)
                    r = registry.CreateSubKey("SOFTWARE\\Policies\\Google\\Chrome\\URLWhitelist" );
                r.SetValue("42", protocol + "://*");

            }
            catch
            {
                return false;
            }
            return true;

        }
    }
}
