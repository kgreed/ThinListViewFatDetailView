using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using ThinLVFatDV.Module.BusinessObjects;
using ThinLVFatDV.Module.BusinessObjects.NonPersistedObjects;

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

        public static List <FatResult> MakeFatResults(int thingId, IObjectSpace nonPersistentObjectSpace)
        {
            var os = GetPersistentObjectSpace(nonPersistentObjectSpace);
            var criteria = $"ThingId ={thingId}";
            var details = os.GetObjects<Detail>(CriteriaOperator.Parse(criteria)).ToList();
            var results = new List<FatResult>();
            foreach (var detail in details)
            {
                var fatResult = new FatResult {Notes = $"1) {detail.Point1}, 2) {detail.Point2}"};
                results.Add(fatResult);
            }

            return results;
           
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
