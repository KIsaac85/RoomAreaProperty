using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomAreaProperty
{
    class DrawingsExportClass 
    {
        public void ExportToDwg(UIDocument uidoc)
        {

			
			Document doc = uidoc.Document;

			FilteredElementCollector collector = new FilteredElementCollector(doc);
			//Create Filter
			ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Views);
			
			IList<Element> elements = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
			List<ElementId> selectids = new List<ElementId>();
			
			foreach (var element in elements)
			{
                Autodesk.Revit.DB.View v = element as Autodesk.Revit.DB.View;
				
				if(v.CanBePrinted)
				selectids.Add(v.Id);
				switch (v.ViewType)
				{
					case ViewType.AreaPlan:
						using (Transaction tx = new Transaction(doc))
						{
							tx.Start("Export");
							
							foreach (ElementId e in selectids)
							{
								DWGExportOptions options = new DWGExportOptions();
								ExportDWGSettings dwgSettings = ExportDWGSettings
									.Create(doc, "filexport");
								options = dwgSettings.GetDWGExportOptions();

								options.Colors = ExportColorMode.TrueColorPerView;
								options.FileVersion = ACADVersion.R2013;
								options.MergedViews = true;





								doc.Export("G:\\FreeLancing\\Fievr",
							v.Name, selectids, options);
								ElementId dwgsetid = dwgSettings.Id;
								doc.Delete(dwgsetid);
							}
							tx.Commit();
						}
						break;
					default:
						break;


				}
			}
			
			

		}
    }
}
