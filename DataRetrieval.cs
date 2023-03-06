using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAreaProperty
{
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
                    AreaObject areaObject = new AreaObject();
                    Options _options = new Options();
                    Area _area = _element as Area;
                    SpatialElementBoundaryOptions spOPT = new SpatialElementBoundaryOptions();
                    areaObject.ID = _area.Id;
                    areaObject.areaValue = _area.Area;
                    areaObject.areaFloorLevel = _area.Level;
                    //The forloop is created to retrieve the boundry lines start and end points.
                    foreach (var boundarySegments in _area.GetBoundarySegments(spOPT))
                    {

                        foreach (var item in boundarySegments)
                        {
                            Curve curv = item.GetCurve();

                            areaObject.areaBoundingLines.Add(curv);

                            AreaObject.Line line = new AreaObject.Line();
                            line.startPoint = curv.GetEndPoint(0);
                            line.endpoint = curv.GetEndPoint(1);
                            //All properties were retrieved from element and areas except
                            // the start and endpoint of curves

                        }
                    }
                    AreaObjectsList.Add(areaObject);
                } 
            }
            return AreaObjectsList;
        }
    }
}