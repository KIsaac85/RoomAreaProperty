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
    /// A class to create drawings 
    /// from area plans
    /// </summary>
    class DWGExportClass 
    {
        public void ExportToDwg(Document doc, IList<Element> elements)
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
                DWGExportOptions options = new DWGExportOptions();
                ExportDWGSettings dwgSettings = ExportDWGSettings.Create(doc, "filexport");
                options = dwgSettings.GetDWGExportOptions();
                options.Colors = ExportColorMode.TrueColorPerView;
                options.FileVersion = ACADVersion.R2013;
                options.MergedViews = true;
                doc.Export("G:\\FreeLancing\\Fievr", "", selectids, options);
                ElementId dwgsetid = dwgSettings.Id;
                doc.Delete(dwgsetid);
                    
                tx.Commit();
                }
				
			
            
		}
    }
}
