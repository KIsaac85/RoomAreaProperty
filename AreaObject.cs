using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace RoomAreaProperty
{
    /// <summary>
    /// Area Object
    /// Each area object has an ID, area value, 
    /// a level and a list of lines
    /// a line is an object which has a start and end points
    /// </summary>
    [DataContract]
    public class AreaObject
    {
        [DataMember]
        public ElementId ID { get; set; }
        [DataMember]
        public double areaValue { get; set; }
        [DataMember]
        public Level areaFloorLevel { get; set; }
        [DataMember]
        public List<Curve> areaBoundingLines { get; set; }

        
        public class Line
        {
            [DataMember]
            public XYZ startPoint { get; set; }
            [DataMember]
            public XYZ endpoint { get; set; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public AreaObject()
        {

            areaBoundingLines = new List<Curve>();
        }

 

    }
}
