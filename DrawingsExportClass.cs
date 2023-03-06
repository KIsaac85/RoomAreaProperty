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
			using (Transaction tx = new Transaction(doc))
			{
				tx.Start("Export");
				ICollection<ElementId> selectids = uidoc.Selection.GetElementIds();
				foreach (ElementId e in selectids)
				{
					DWGExportOptions options = new DWGExportOptions();
					ExportDWGSettings dwgSettings = ExportDWGSettings
						.Create(doc, "filexport");
					options = dwgSettings.GetDWGExportOptions();

					options.Colors = ExportColorMode.TrueColorPerView;
					options.FileVersion = ACADVersion.R2013;
					options.MergedViews = true;

					Element f = doc.GetElement(e);
					ElementType ftype = doc.GetElement(f.GetTypeId()) as ElementType;
					List<ElementId> icollection = new List<ElementId>();

					icollection.Add(e);

					doc.Export("G:\\FreeLancing\\Fievr",
				ftype.FamilyName + " " + f.Name, icollection, options);
					ElementId dwgsetid = dwgSettings.Id;
					doc.Delete(dwgsetid);
				}
				tx.Commit();
			}

		}
    }
}
