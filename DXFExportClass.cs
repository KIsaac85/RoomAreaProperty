using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAreaProperty
{
    /// <summary>
    /// A class to create dxf drawings 
    /// from area plans
    /// </summary>
    class DXFExportClass
    {
        private DXFExportOptions options { get; set; }
        
        private ExportDWGSettings dwgSettings { get; set; }
        public View v { get; set; }
        private ElementId dwgsetid { get; set; }
        public void ExportToDXF(Document doc, IList<Element> elements, string filepath)
        {
            
            List<ElementId> selectids = new List<ElementId>();

            foreach (var element in elements)
            {
                v = element as View;
                if (v.CanBePrinted && v.ViewType == ViewType.AreaPlan)

                    selectids.Add(v.Id);
            }




            if (selectids.Any())
            {
                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Export");
                    options = new DXFExportOptions();
                    dwgSettings = ExportDWGSettings.Create(doc, "filexport");

                    options = dwgSettings.GetDXFExportOptions();
                    options.Colors = ExportColorMode.TrueColorPerView;
                    options.FileVersion = ACADVersion.R2013;

                    doc.Export($"{filepath}", "", selectids, options);
                    dwgsetid = dwgSettings.Id;
                    doc.Delete(dwgsetid);

                    tx.Commit();
                } 
            }
            else
                TaskDialog.Show("No Data", "There are no area views to export.");


        }
    }
}
