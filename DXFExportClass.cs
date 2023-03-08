using Autodesk.Revit.DB;
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
        public void ExportToDXF(Document doc, IList<Element> elements)
        {
            
            List<ElementId> selectids = new List<ElementId>();

            foreach (var element in elements)
            {
                View v = element as View;
                if (v.CanBePrinted && v.ViewType == ViewType.AreaPlan)

                    selectids.Add(v.Id);
            }




            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Export");
                DXFExportOptions options = new DXFExportOptions();
                ExportDWGSettings dwgSettings = ExportDWGSettings.Create(doc, "filexport");
                
                options = dwgSettings.GetDXFExportOptions();
                options.Colors = ExportColorMode.TrueColorPerView;
                options.FileVersion = ACADVersion.R2013;
                
                doc.Export("G:\\FreeLancing\\Fievr", "", selectids, options);
                ElementId dwgsetid = dwgSettings.Id;
                doc.Delete(dwgsetid);

                tx.Commit();
            }



        }
    }
}
