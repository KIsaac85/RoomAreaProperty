using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.IO;
using Autodesk.Revit.Attributes;
using Newtonsoft.Json;

namespace RoomAreaProperty
{
    [TransactionAttribute(TransactionMode.ReadOnly)]
    public class AreasExport : IExternalCommand
    {

        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            string myjs ;


            //To convert from internal units to the format units
            FormatOptions areaFormatOptions = doc.GetUnits().GetFormatOptions(SpecTypeId.Area);
            ForgeTypeId areaUnit = areaFormatOptions.GetUnitTypeId();

            Filters flt = new Filters(doc);
            IList<Element> AreaElements = Filters.elementsReferecne();

            //String builder to save data
            StringBuilder sb = new StringBuilder();
            if (AreaElements.Count!=0)
            {
                
                foreach (Element _element in AreaElements)
                {
                    Options _options = new Options();
                    Area _area = _element as Area;
                    SpatialElementBoundaryOptions spOPT = new SpatialElementBoundaryOptions();

                    //The forloop is created to retrieve the boundry lines start and end points.
                    foreach (var boundarySegments in _area.GetBoundarySegments(spOPT))
                    {
                        foreach (var item in boundarySegments)
                        {
                            Curve curv = item.GetCurve();
                            //All properties were retrieved from element and areas except
                            // the start and endpoint of curves
                            sb.AppendLine("Element ID: " + _element.Id.ToString() +
                                "Area (m2): " + Math.Round(UnitUtils.ConvertFromInternalUnits(_area.Area, areaUnit), 2).ToString() +
                              "  Floor #:" + _area.Level.Name +
                              "  Type: " + _area.Name +
                              "  Coordinates: ( " + curv.GetEndPoint(0).ToString() + "," + curv.GetEndPoint(1).ToString() + ")");
                        }
                    }
                }
            }


            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.NullValueHandling = NullValueHandling.Ignore;

            Formatting formatting = RoomAreaProperty
                .UserSettings.JsonIndented ? Formatting.Indented: Formatting.None;

            myjs = JsonConvert.SerializeObject(sb, formatting, settings);

            File.WriteAllText(@"D:\Areas Properties Original.js", myjs);

            TaskDialog.Show("Areas", sb.ToString());
        
            return Result.Succeeded;
        }
    }
}
