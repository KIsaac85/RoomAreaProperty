using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.IO;
using Autodesk.Revit.Attributes;
/*
namespace RoomAreaProperty
{
    [TransactionAttribute(TransactionMode.ReadOnly)]
    public class AreasExport : IExternalCommand
    {

        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            //Filters of Areas
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Areas);

            //To convert from internal units to the format units
            FormatOptions areaFormatOptions = doc.GetUnits().GetFormatOptions(SpecTypeId.Area);
            ForgeTypeId areaUnit = areaFormatOptions.GetUnitTypeId();
            IList<Element> AreaElements = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

            //String builder to save data
            StringBuilder sb = new StringBuilder();
            if (AreaElements.Count!=0)
            {
                
                
            }
            //Save data in a text file
            File.WriteAllText(@"D:\Areas Properties.txt", sb.ToString());
            TaskDialog.Show("Areas", sb.ToString());
        
            return Result.Succeeded;
        }
    }
}
*/
