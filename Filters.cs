using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAreaProperty
{
    public class Filters
    {
        private static FilteredElementCollector collector { get; set; }

        private static ElementCategoryFilter filter { get; set; }

        private static IList<Element> AreaElements { get; set; }
        public Filters(Document doc)
        {
            collector = new FilteredElementCollector(doc);
            filter = new ElementCategoryFilter(BuiltInCategory.OST_Areas);
            AreaElements = new List<Element>();
        }
        public static IList<Element> elementsReferecne()
        {
            AreaElements = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

            return AreaElements;
        }
    }
}
