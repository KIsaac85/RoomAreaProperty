#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Reflection;
#endregion

namespace RoomAreas
{
  class App : IExternalApplication
  {
    /// <summary>
    /// Add buttons for our command
    /// to the ribbon panel.
    /// </summary>
    void PopulatePanel( RibbonPanel p )
    {
      string path = Assembly.GetExecutingAssembly()
        .Location;

      RibbonItemData i1 = new PushButtonData(
          "Room_Area_Property_Command", "Room Areas \r\n Properties",
          path, "RoomAreasCommand");

      i1.ToolTip = "Export Area Properties to JSON File";

      //p.AddStackedItems( i1, i2, i3 );

      p.AddItem( i1 );
    }

    public Result OnStartup( UIControlledApplication a )
    {
      PopulatePanel(
        a.CreateRibbonPanel(
          "Area Property Export" ) );

      return Result.Succeeded;
    }

    public Result OnShutdown( UIControlledApplication a )
    {
      return Result.Succeeded;
    }
  }
}
