using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using ThinLVFatDV.Module.BusinessObjects;

namespace ThinLVFatDV.Module.Functions
{
    public class HandyFunctions
    {
        public static void AddPersistentOsToNonPersistentOs(XafApplication application, NonPersistentObjectSpace os, Type type)
        {
            var additionalObjectSpace = application.CreateObjectSpace(type);
            if (os.AdditionalObjectSpaces.Count == 0)
            {
                os.AdditionalObjectSpaces.Add(additionalObjectSpace);
            }
        }

        public static FatResult MakeFatResult(int thingId, IObjectSpace nonPersistentObjectSpace)
        {
            var persistentOs = GetPersistentObjectSpace(nonPersistentObjectSpace);
             
            var fatResult = new FatResult {Thing = new Thing { Id = thingId }};

            PopulateFatResult(fatResult, persistentOs);
           
            return fatResult;
        }

        private static void PopulateFatResult(FatResult fatResult, IObjectSpace os)
        {
            fatResult.Thing = os.GetObject(fatResult.Thing);
            var criteria = $"ThingId ={fatResult.Thing.Id}";
            var details = os.GetObjects<Detail>(CriteriaOperator.Parse(criteria));
            var sb = new StringBuilder();
            foreach (var detail in details)
            {
                sb.AppendLine($"[Point 1] {detail.Point1} [Point 2] {detail.Point2}");
            }

            fatResult.Notes = sb.ToString();
        }

        private static Thing GetThing(int thingId, object persistentOs)
        {
            throw new NotImplementedException();
        }

        private static IObjectSpace GetPersistentObjectSpace(IObjectSpace os)
        {
            return ((NonPersistentObjectSpace)os).AdditionalObjectSpaces.FirstOrDefault();
        }

        public static void DisposeAdditionalPersistentObjectSpace(XafApplication application, NonPersistentObjectSpace os)
        {
            if (os.AdditionalObjectSpaces.Count <= 0) return;
            var additionalObjectSpace = os.AdditionalObjectSpaces.FirstOrDefault();
            os.AdditionalObjectSpaces.Remove(additionalObjectSpace);
            additionalObjectSpace?.Dispose();
        }
    }
}
