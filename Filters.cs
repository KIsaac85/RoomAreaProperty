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
        private static FilteredElementCollector AreasCollector { get; set; }
        private static FilteredElementCollector ViewsCollector { get; set; }
        private static ElementCategoryFilter AreaFilter { get; set; }
        private static ElementCategoryFilter ViewsFilter { get; set; }
        private static IList<Element> AreaElements { get; set; }
        private static IList<Element> ViewsElements { get; set; }
        public Filters(Document doc)
        {
            AreasCollector = new FilteredElementCollector(doc);
            ViewsCollector = new FilteredElementCollector(doc);
            AreaFilter = new ElementCategoryFilter(BuiltInCategory.OST_Areas);
            ViewsFilter = new ElementCategoryFilter(BuiltInCategory.OST_Views);
            AreaElements = new List<Element>();
            ViewsElements = new List<Element>();

        }
        public  IList<Element> elementsAreaReferecne()
        {
            AreaElements = AreasCollector.WherePasses(AreaFilter).WhereElementIsNotElementType().ToElements();

            return AreaElements;
        }
        public  IList<Element> elementsViewReferecne()
        {
            
            ViewsElements = ViewsCollector.WherePasses(ViewsFilter).WhereElementIsNotElementType().ToElements();

            return ViewsElements;
        }
    }
}
