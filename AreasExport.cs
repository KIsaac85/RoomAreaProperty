/*
using System.Collections.Generic;
using System.Text;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.IO;
using Autodesk.Revit.Attributes;
using Newtonsoft.Json;


namespace RoomAreaProperty
{
    [TransactionAttribute(TransactionMode.ReadOnly)]
    public class AreasExport : IExternalCommand
    {

        /// <summary>
        /// Entry point for the plugin
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ///some lines will be moved or replaced this is not final!
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            




            Filters flt = new Filters(doc);

            DataRetrieval dt = new DataRetrieval(Filters.elementsAreaReferecne());
            List<AreaObject> areaobjectlist = new List<AreaObject>();
            areaobjectlist.AddRange(dt.data());

            #region JSON File Creation

            
            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.NullValueHandling = NullValueHandling.Ignore;

            Formatting formatting = RoomAreaProperty
                .UserSettings.JsonIndented ? Formatting.Indented : Formatting.None;

            var sjson = JsonConvert.SerializeObject(areaobjectlist, formatting, settings);
            File.WriteAllText(@"D:\Areas Properties Original.js", sjson);
            
            #endregion
             

            
            #region Drawings Export
            DWGExportClass dwgex = new DWGExportClass();
            DXFExportClass dxf = new DXFExportClass();
            dwgex.ExportToDwg(doc, Filters.elementsViewReferecne());
            dxf.ExportToDXF(doc, Filters.elementsViewReferecne()); 
            #endregion
            

            return Result.Succeeded;
        }
    }
    
}
*/