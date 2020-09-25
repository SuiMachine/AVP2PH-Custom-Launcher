Aliens vs. Predator 2: Primal Hunt - Custom Launcher
============
A custom launcher for Aliens vs. Predator 2: Primal Hunt, written in C# (with elements of C++ and ASM)

Features
--------
  * Contains all of the main features of original launcher (except a host server option).
  * Allows to easily enable and disable Windowed Mode.
  * Supports custom screen resolutions.
  * Built in aspect ratio hack for easy access to widescreen resolutions.
  * Built in FOV changer, tied directly to aspect ratio hack (calculated as the most common Horizontal+ FOV).
  
Requirements
-------
 * Aliens vs. Predator 2: Primal Hunt
 * Windows Vista / 7 / 8 / 10
 * [Microsoft .NET Framework 4.5](https://www.microsoft.com/en-US/download/details.aspx?id=30653)
 * [Visual C++ Redistributable for Visual Studio 2017](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) 32-bit
 * Administrator rights on the system (due to writting to memory of other program / injecting DLL libraries etc.)
  
Installation
-------
Patch the game if you haven't already. Download the program from [releases page](https://github.com/SuiMachine/AVP2PH-Custom-Launcher/releases). Copy it to AVP2 directory. To get rid of annoying question from Windows about download files, right click on **AVP_CustomLauncher.exe**, and choose Properties. In the General tab, click Unlock. 

**Note**: For the program to work properly, Administrator rights may be required (especially for Widescreen hack). You can set it in Compatibility Options.

**Note 2**: For running the game on resolutions wider than 2048px you'll need "special" D3DIM700.DLL by jackfuste. It can be found in ***Over2048pxFix*** directory. Just move it to main directory and hope it works (cause sometimes it seems it doesn't).

**Note 3**: While it may be surprising some some people, I have not tested the launcher with multiplayer. With the master servers being dead for a long time and projects to restore them having suspecious files (says the one, who literally injects DLLs into memory), for me, it's dead.

Credits
-------
* [SuicideMachine](http://www.twitch.tv/suicidemachine/)
* evolution536 - who wrote the DLL injector class
* Cless - who wrote the trainer class