AreaExport is a Revit add-in implementing an external command.


In order to generate the DLL, you download and compile the Visual Studio solution:

Download or clone the RoomAreaProperties GitHub repository.
Open the solution file RoomAreaProperty.sln in Visual Studio; to build it:
Add references to the Revit API assembly files RevitAPI.dll and RevitAPIUI.dll, located in your Revit installation directory.


If you wish to debug, set up the path to the Revit executable in the Debug tab, Start External Program; change the path to your system installation, e.g., C:\ProgramData\Autodesk\Revit\Addins\2021
Build and optionally debug into Revit.exe.

You may need to change the assembly path of the DLL file in the addin manifest file.

It is installed in the standard manner, i.e., by copying one file to the standard Revit Add-Ins folder:
AreasPropereties.addin

This will open the Revit installation and install the plugin.

You can then either start up Revit.exe manually or via the Visual Studio debugger.

