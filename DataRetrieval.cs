using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAreaProperty
{
    /// <summary>
    /// A class is created to receive data from revit and add 
    /// it to each object
    /// then each object is added to the list
    /// the list which is supposed to be added to JSON
    /// </summary>
    class DataRetrieval
    {
        private IList<Element> AreaElements { get; set; }
        public List<AreaObject> AreaObjectsList { get; set; }


        public DataRetrieval(IList<Element> ElementsList)
        {
            AreaElements = new List<Element>();
            AreaElements = ElementsList;
            AreaObjectsList = new List<AreaObject>();
           
        }
        public List<AreaObject> data()
        {
            if (AreaElements.Count != 0)
            {
                foreach (Element _element in AreaElements)
                {
                    AreaObject arObject = new AreaObject();
                    Options _options = new Options();
                    Area _area = _element as Area;
                    SpatialElementBoundaryOptions spOPT = new SpatialElementBoundaryOptions();
                    arObject.ID = _area.Id;
                    arObject.areaValue = _area.Area;
                    arObject.areaFloorLevel = _area.Level.Name;
                    //The forloop is created to retrieve the boundry lines start and end points.
                    foreach (var boundarySegments in _area.GetBoundarySegments(spOPT))
                    {
                        
                        foreach (var item in boundarySegments)
                        {
                            Curve curv = item.GetCurve();
                            AreaObject.BoundaryLine  line = new AreaObject.BoundaryLine();
                            line.startPoint = curv.GetEndPoint(0);
                            line.endpoint = curv.GetEndPoint(1);
                            arObject.BoundaryLinesPoints.Add(line);

                        }
                        
                    }
                    AreaObjectsList.Add(arObject);
                } 
            }
            return AreaObjectsList;
        }
    }
}