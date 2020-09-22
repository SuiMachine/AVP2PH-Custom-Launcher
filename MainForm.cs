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
        Config.CustomConfig customConfig;
        Config.LithTechConfig lithTechConfig;


        GameHack _gamehack = new GameHack();
        int _posX = 0;
        int _posY = 0;
        private bool skipLauncher = false;
        string originalParams = "";

        public mainform(string[] originalParams)
        {
            if (originalParams.Contains("-skiplauncher", StringComparer.InvariantCultureIgnoreCase))
            {
                originalParams = originalParams.Where(x => x.ToLower() != "-skiplauncher").ToArray();
                skipLauncher = true;
            }
            this.originalParams = string.Join(" ", originalParams);
            LogHandler.WriteLine("LogFile created.");
            InitializeComponent();
        }

        #region Functions
        private void CheckForRequiredGameFiles()
        {
            string[] files = { "lithtech.exe", "AVP2X.REZ", "AVP2DLL.REZ", "AVP2L.REZ", "DIALOGUE.REZ", "AVP2P1.REZ", "AVP2P.REZ", "binkw32.dll", "MULTI.REZ", "server.dll", "ltmsg.dll", "binkw32.dll" };

            for (int i = 0; i < files.Length; i++)
            {
                if (!File.Exists(files[i]))
                {
                    MessageBox.Show("No " + files[i] + " found. Please place the custom launcher in the directory with the game!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }

            if (!File.Exists("widescreenfix.dll"))
            {
                MessageBox.Show("Widescreenfix.dll has not been found. This file is required for Widescreen support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateGenericAVP2Cmds()
        {
            string basePath = "";
            object key = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Monolith Productions\\Aliens vs. Predator 2\\1.0", "InstallDir", "");

            if (key != null)
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

                if (result == DialogResult.OK)
                {
                    basePath = fld.SelectedPath;
                }
                else
                {
                    Close();
                }
            }

            string output = String.Format("-windowtitle \"Aliens versus Predator 2: Primal Hunt\" -rez \"{0}AVP2.rez\" -rez \"{0}sounds.rez\" -rez \"{0}Alien.rez\" -rez \"{0}Marine.rez\" -rez \"{0}Predator.rez\" -rez \"{0}Multi.rez\" -rez multi.rez -rez \"AVP2dll.rez\" -rez \"AVP2l.rez\" -rez dialogue.rez -rez avp2p.rez -rez avp2p1.rez -rez avp2x.rez -rez custom", basePath + "\\");
            File.WriteAllText("avp2cmds.txt", output);
        }

        private void SetPositionFromConfig()
        {
            if (checkIfPosIsCorrect(customConfig.PositionX, customConfig.PositionY))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.SetDesktopLocation((int)customConfig.PositionX, (int)customConfig.PositionY);
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }
        #endregion

        #region EventHandlers
        private void mainform_Load(object sender, EventArgs e)
        {
            CheckForRequiredGameFiles();

            if (!File.Exists("autoexec.cfg"))
            {
                ConfigChoice _ConfigChoice = new ConfigChoice();
                _ConfigChoice.ShowDialog();
            }


            if (!File.Exists("avp2cmds.txt"))
            {
                MessageBox.Show("No avp2cmds.txt found. The launcher will try to create it based on files in your current directory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CreateGenericAVP2Cmds();
            }

            customConfig = Config.CustomConfig.Load();
            lithTechConfig = Config.LithTechConfig.Load();

            if (!File.Exists("avp2cmds.txt"))
            {
                MessageBox.Show("No avp2cmds.txt found. The launcher will try to create it based on files in your current directory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CreateGenericAVP2Cmds();
            }
            SetPositionFromConfig();

            if (skipLauncher)
            {
                this.WindowState = FormWindowState.Minimized;
                StartGame();
            }
        }

        private void B_StartGame_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            string cmdlineparamters = File.ReadLines("avp2cmds.txt").FirstOrDefault();
            cmdlineparamters = stripResolutionParameters(cmdlineparamters);

            cmdlineparamters += " ++RenderDll d3d.ren ++CardDesc display";

            if(customConfig.Windowed)
                cmdlineparamters = cmdlineparamters + " +windowed 1";
            else
                cmdlineparamters = cmdlineparamters + " +windowed 0";

            if(customConfig.DisableSound)
                cmdlineparamters = cmdlineparamters + " +DisableSound 1";
            else
                cmdlineparamters = cmdlineparamters + " +DisableSound 0";

            if(customConfig.DisableMusic)
                cmdlineparamters = cmdlineparamters + " +DisableMusic 1";
            else
                cmdlineparamters = cmdlineparamters + " +DisableMusic 0";

            if(customConfig.DisableLogos)
                cmdlineparamters = cmdlineparamters + " +DisableMovies 1";
            else
                cmdlineparamters = cmdlineparamters + " +DisableMovies 0";

            if(customConfig.DisableJoystick)
                cmdlineparamters = cmdlineparamters + " +DisableJoystick 1";
            else
                cmdlineparamters = cmdlineparamters + " +DisableJoystick 0";

            if(customConfig.DisableTrippleBuffering)
                cmdlineparamters = cmdlineparamters + " +EnableTripBuf 0";
            else
                cmdlineparamters = cmdlineparamters + " +EnableTripBuf 1";

            if(customConfig.DisableHardwareCursor)
                cmdlineparamters = cmdlineparamters + " +DisableHardwareCursor 1";
            else
                cmdlineparamters = cmdlineparamters + " +DisableHardwareCursor 0";

            cmdlineparamters += $" {originalParams} {customConfig.CVARS}";

            Thread GameHackThread = new Thread(_gamehack.DoWork);
            if(customConfig.AspectRatioFix)
            {
                _gamehack.SendValues(customConfig.FOV, (int)lithTechConfig.GameScreenWidth, (int)lithTechConfig.GameScreenHeight, customConfig.LithFixEnabled);
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
                LogHandler.WriteLine("Exception in mainform (gameprocess):" + ex.ToString());
                return;
            }
        }

        private string stripResolutionParameters(string cmdlineparamters)
        {
            string[] words = cmdlineparamters.Split(' ');
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (words[i].ToLower() == "++gamescreenwidth" || words[i].ToLower() == "++screenwidth")
                {
                    if (uint.TryParse(words[i + 1], out _))
                    {
                        words[i + 1] = lithTechConfig.GameScreenWidth.ToString();
                        i++;
                    }
                }
                else if (words[i].ToLower() == "++gamescreenheight" || words[i].ToLower() == "++screenheight")
                {
                    if (uint.TryParse(words[i + 1], out _))
                    {
                        words[i + 1] = lithTechConfig.GameScreenHeight.ToString();
                        i++;
                    }
                }
                else if (words[i].ToLower() == "++gamebitdepth" || words[i].ToLower() == "++bitdepth")
                {
                    if (uint.TryParse(words[i + 1], out _))
                    {
                        words[i + 1] = lithTechConfig.GameBitDepth.ToString();

                        i++;
                    }
                }
            }
            return string.Join(" ", words);
        }

        private void B_DisplaySettings_Click(object sender, EventArgs e)
        {
            using (GameSettings _GraphicsSettings = new GameSettings(new Config.CustomConfig(customConfig), new Config.LithTechConfig(lithTechConfig)))
            {
                _GraphicsSettings.StartPosition = FormStartPosition.Manual;
                _GraphicsSettings.SetDesktopLocation(this.DesktopLocation.X + 20, this.DesktopLocation.Y + 20);
                if (_GraphicsSettings.ShowDialog() == DialogResult.OK)
                {
                    lithTechConfig = _GraphicsSettings.lithTechConfig;
                    customConfig = _GraphicsSettings.customConfig;
                }
            }
        }

        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            customConfig.PositionX = (uint)_posX;
            customConfig.PositionY = (uint)_posY;
            customConfig.Save();
            LogHandler.Close();
        }

        private void mainform_LocationChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal && this.DesktopLocation.X > 0 && this.DesktopLocation.Y > 0 && this.DesktopLocation.X < 14000)
            {
                _posX = this.DesktopLocation.X;
                _posY = this.DesktopLocation.Y;
            }
        }

        private void B_Exit_Click(object sender, EventArgs e)
        {
            _gamehack.RequestStop();
            this.Close();
        }
        #endregion

        #region ParseFunctions
        private uint parsePosition(string line)
        {
            uint outVal = 0;
            string valText = line.Split(':')[1];
            if (valText != String.Empty)
            {
                if (uint.TryParse(valText, out outVal))
                {
                    return outVal;
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        private bool checkIfPosIsCorrect(uint PosX, uint PosY)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                Rectangle scrRect = screen.WorkingArea;
                if (PosX > scrRect.X && PosX < scrRect.X + scrRect.Width &&
                    PosY > scrRect.Y && PosY < scrRect.Y + scrRect.Height)
                    return true;
            }
            return false;
        }
        #endregion

        private void projectPageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/SuiMachine/AVP2PH-Custom-Launcher");
        }

        private void donatePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.gamingforgood.net/s/suicidemachine/widget");
        }

        private void pcgwLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://pcgamingwiki.com/w/index.php?title=Aliens_versus_Predator_2");

        }

        private void WSGFLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.wsgf.org/dr/aliens-versus-predator-2-gold-edition");
        }
    }
}
