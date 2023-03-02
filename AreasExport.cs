﻿using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.IO;
using Autodesk.Revit.Attributes;

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
            //Save data in a text file
            File.WriteAllText(@"D:\Areas Properties.txt", sb.ToString());
            TaskDialog.Show("Areas", sb.ToString());
        
            return Result.Succeeded;
        }
    }
}
