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
        private List<ElementId> selectids { get; set; }
        private View v { get; set; }
        private DWGExportOptions options { get; set; }
        
        private ExportDWGSettings dwgSettings { get; set; }
        
        private ElementId dwgsetid { get; set; }
        public DWGExportClass()
        {
            selectids = new List<ElementId>();
        }
        public void ExportToDwg(Document doc, IList<Element> elements, string filepath)
        {
            
			 
           
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
                    options = new DWGExportOptions();
                    dwgSettings = ExportDWGSettings.Create(doc, "filexport");
                    options = dwgSettings.GetDWGExportOptions();
                    options.Colors = ExportColorMode.TrueColorPerView;
                    options.FileVersion = ACADVersion.R2013;
                    options.MergedViews = true;
                    doc.Export($"{filepath}", "", selectids, options);

                    dwgsetid = dwgSettings.Id;
                    doc.Delete(dwgsetid);

                    tx.Commit();
                }
            }


            
				
			
            
		}
    }
}
