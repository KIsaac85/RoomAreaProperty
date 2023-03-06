using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAreaProperty
{
    class AreaObject
    {
        public ElementId ID { get; set; }

        public double areaValue { get; set; }

        public Level areaFloorLevel { get; set; }

        public List<Line> areaBoundingLines { get; set; }

        public class Line
        {
            public XYZ startPoint { get; set; }
            public XYZ endpoint { get; set; }
        }

        public AreaObject(IList<Element> AreaElements)
        {

        }
        public void data()
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
}
