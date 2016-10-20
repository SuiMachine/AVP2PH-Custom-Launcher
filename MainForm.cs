using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Win32;

namespace AVP_CustomLauncher
{
    public partial class mainform : Form
    {
        GameSettings _GraphicsSettings;
        ConfigChoice _ConfigChoice;
        GameHack _gamehack = new GameHack();

        public mainform()
        {
            InitializeComponent();
        }

        private void mainform_Load(object sender, EventArgs e)
        {
            CheckForRequiredGameFiles();

            if(!File.Exists(@"autoexec.cfg"))
            {
                _ConfigChoice = new ConfigChoice();
                _ConfigChoice.ShowDialog();
            }

            if (!File.Exists(@"avp2cmds.txt"))
            {
                MessageBox.Show("No avp2cmds.txt found. The launcher will try to create it based on files in your current directory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CreateGenericAVP2Cmds();
            }

            _GraphicsSettings = new GameSettings(this);
        }

        private void CheckForRequiredGameFiles()
        {
            string[] files = { "lithtech.exe", "AVP2X.REZ", "AVP2DLL.REZ", "AVP2L.REZ", "DIALOGUE.REZ", "AVP2P1.REZ", "AVP2P.REZ", "binkw32.dll", "MULTI.REZ", "server.dll", "ltmsg.dll", "binkw32.dll"};

            for (int i=0; i<files.Length; i++)
            {
                if (!File.Exists(@files[i]))
                {
                    MessageBox.Show("No " +files[i] + " found. Please place the custom launcher in the directory with the game!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }

            /*
            if (!File.Exists(@"widescreenfix.dll"))
            {
                MessageBox.Show("Widescreenfix.dll has not been found. This file is required for Widescreen support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void B_StartGame_Click(object sender, EventArgs e)
        {
            StreamReader SR = new StreamReader(@"avp2cmds.txt");
            string cmdlineparamters = "";
            cmdlineparamters = SR.ReadToEnd();

            cmdlineparamters += " ++RenderDll d3d.ren ++CardDesc display";

            if (_GraphicsSettings.windowed)
                cmdlineparamters = cmdlineparamters + " +windowed 1";
            else
                cmdlineparamters = cmdlineparamters + " +windowed 0";  

            if (_GraphicsSettings.disablesound)
                cmdlineparamters = cmdlineparamters + " +DisableSound 1";
            else
                cmdlineparamters = cmdlineparamters + " +DisableSound 0";

            if (_GraphicsSettings.disablemusic)
                cmdlineparamters = cmdlineparamters + " +DisableMusic 1";
            else
                cmdlineparamters = cmdlineparamters + " +DisableMusic 0";

            if (_GraphicsSettings.disablelogos)
                cmdlineparamters = cmdlineparamters + " +DisableMovies 1";
            else
                cmdlineparamters = cmdlineparamters + " +DisableMovies 0";

            if (_GraphicsSettings.disablejoystick)
                cmdlineparamters = cmdlineparamters + " +DisableJoystick 1";
            else
                cmdlineparamters = cmdlineparamters + " +DisableJoystick 0";

            if (_GraphicsSettings.disabletripplebuffering)
                cmdlineparamters = cmdlineparamters + " +EnableTripBuf 0";
            else
                cmdlineparamters = cmdlineparamters + " +EnableTripBuf 1";

            if (_GraphicsSettings.disablehardwarecursor)
                cmdlineparamters = cmdlineparamters + " +DisableHardwareCursor 1";
            else
                cmdlineparamters = cmdlineparamters + " +DisableHardwareCursor 0";

            cmdlineparamters = cmdlineparamters + " " + _GraphicsSettings.T_CommandLine.Text;
                



            Thread GameHackThread = new Thread(_gamehack.DoWork);
            if (_GraphicsSettings.aspectratiohack)
            {
                _gamehack.SendValues(_GraphicsSettings.fov, _GraphicsSettings.ResolutionX, _GraphicsSettings.ResolutionY);
                GameHackThread.Start();
            }

            try
            {                
                Process gameProcess = new Process();
                gameProcess.StartInfo.FileName = "Lithtech.exe";
                gameProcess.StartInfo.Arguments = cmdlineparamters;
                gameProcess.EnableRaisingEvents = true;
                this.WindowState = FormWindowState.Minimized;
                
                gameProcess.Start();
                gameProcess.WaitForExit();
                _gamehack.RequestStop();
                this.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occurred!: " + ex.Message);
                return;
            }
        }

        private void B_DisplaySettings_Click(object sender, EventArgs e)
        {
            _GraphicsSettings.SetDesktopLocation(this.DesktopLocation.X + 10, this.DesktopLocation.Y + 10);
            _GraphicsSettings.ShowDialog();
        }

        private void B_Exit_Click(object sender, EventArgs e)
        {
            _gamehack.RequestStop();
            this.Close();
        }

        private void CreateGenericAVP2Cmds()
        {
            string basePath = "";
            object key = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Monolith Productions\\Aliens vs. Predator 2\\1.0", "InstallDir", "");

            if(key != null)
            {
                basePath = key.ToString();
            }
            else
            {
                MessageBox.Show("Launcher was not able to find the location of the base game. Please specify it manually.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FolderBrowserDialog fld = new FolderBrowserDialog();
                fld.ShowNewFolderButton = false;
                fld.Description = "Select a folder where the base game is located.";
                DialogResult result = fld.ShowDialog();

                if(result == DialogResult.OK)
                {
                    basePath = fld.SelectedPath;
                }
                else
                {
                    Close();
                }
            }
            
            string output = String.Format("-windowtitle \"Aliens versus Predator 2: Primal Hunt\" -rez \"{0}AVP2.rez\" -rez \"{0}sounds.rez\" -rez \"{0}Alien.rez\" -rez \"{0}Marine.rez\" -rez \"{0}Predator.rez\" -rez \"{0}Multi.rez\" -rez multi.rez -rez \"AVP2dll.rez\" -rez \"AVP2l.rez\" -rez dialogue.rez -rez avp2p.rez -rez avp2p1.rez -rez avp2x.rez -rez custom", basePath + "\\");
            
            StreamWriter SW = new StreamWriter(@"avp2cmds.txt");
            SW.WriteLine(output);
            SW.Close();
        }
    }
}
