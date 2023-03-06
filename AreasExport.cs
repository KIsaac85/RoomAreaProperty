using System;
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

        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            string myjs ;


            //To convert from internal units to the format units
            FormatOptions areaFormatOptions = doc.GetUnits().GetFormatOptions(SpecTypeId.Area);
            ForgeTypeId areaUnit = areaFormatOptions.GetUnitTypeId();

            Filters flt = new Filters(doc);
            List<Element> AreaElements = new List<Element>();
            AreaElements.AddRange(Filters.elementsReferecne());
            DataRetrieval dt = new DataRetrieval(AreaElements);
            List<AreaObject> areaobjectlist = new List<AreaObject>();
            areaobjectlist.AddRange(dt.data());

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.NullValueHandling = NullValueHandling.Ignore;

            Formatting formatting = RoomAreaProperty
                .UserSettings.JsonIndented ? Formatting.Indented: Formatting.None;

            myjs = JsonConvert.SerializeObject(areaobjectlist, formatting, settings);

            File.WriteAllText(@"D:\Areas Properties Original.js", myjs);

            
        
            return Result.Succeeded;
        }
    }
}
