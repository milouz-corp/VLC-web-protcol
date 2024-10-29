using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace VLC_web_protocol
{
    class Program
    {
        static void Main(string[] args)
        {
            string lines = "";

            try
            {
                if (Environment.GetCommandLineArgs().Length > 1)
                {
                    var vlcArgs = Environment.GetCommandLineArgs()[1].Substring(6);
                    vlcArgs = vlcArgs.Replace("https//", "https://");
                    vlcArgs = vlcArgs.Replace("%20", " ");
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + "vlc.exe", vlcArgs);
                }
                else
                {

                    WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

                    if (!pricipal.IsInRole(WindowsBuiltInRole.Administrator))
                    {
                        RunElevated(Application.ExecutablePath);
                        return;
                    }

                    try
                    {
                        lines += "Installing\n";
                        var paths = new List<string>();
                        paths.Add(Environment.ExpandEnvironmentVariables("%ProgramW6432%"));
                        paths.Add(Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"));
                        paths.Add(Environment.ExpandEnvironmentVariables("%ProgramFiles%"));

                        var exeName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
                        var vlcFound = false;
                        foreach (var path in paths)
                        {
                            var vlcPath = path + @"\VideoLAN\VLC\";

                            if (File.Exists(vlcPath + "vlc.exe"))
                            {
                                vlcFound = true;
                                if (!Equals(AppDomain.CurrentDomain.BaseDirectory, vlcPath))
                                {
                                    lines += "Copying to VLC Path : " + vlcPath + "\n";
                                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + exeName, vlcPath + exeName, true);

                                    lines += "Registering Protocol\n";
                                    URISchema.Register("vlc", vlcPath + exeName, "%Host%", Microsoft.Win32.Registry.LocalMachine);

                                    lines += "Installation Successfull\n";
                                }
                                else
                                {
                                    lines += "This is already installed\n";
                                }
                                break;
                            }
                        }
                        if (!vlcFound)
                        {
                            lines += "VLC not found\n";
                        }

                        MessageBox.Show(lines);

                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show(lines + "ERROR : Restart with Administrator privileges to install");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(lines + ex.ToString());
            }

        }

        private static void RunElevated(string fileName)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Verb = "runas";

            processInfo.FileName = fileName;
            try
            {
                Process.Start(processInfo);
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.ToString());
#endif
                //Do nothing. Probably the user canceled the UAC window
            }
        }
    }
}
