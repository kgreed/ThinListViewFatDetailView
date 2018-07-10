using System;
using System.Diagnostics;
using System.Linq;
using DevExpress.ExpressApp;
using ThinLVFatDV.Module.BusinessObjects;
using ThinLVFatDV.Module.BusinessObjects.NonPersistedObjects;
using ThinLVFatDV.Module.Functions;

namespace ThinLVFatDV.Module.Win.Controllers
{
     public partial class ThinResultObjectController : ObjectViewController<ListView, ThinResult>
    {
       
       
        public ThinResultObjectController()
        {
            InitializeComponent();
            TargetObjectType = typeof(ThinResult);
           
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            ObjectSpace.Refresh();
        }

        protected override void OnActivated()
        {
            base.OnActivated();


            var os = (NonPersistentObjectSpace) ObjectSpace;
             HandyFunctions.AddPersistentOsToNonPersistentOs(Application, os, typeof(Thing));
            os.ObjectsGetting += os_ObjectsGetting;
          //  View.CollectionSource.CriteriaApplied += CollectionSource_CriteriaApplied;
            View.CreateCustomCurrentObjectDetailView += View_CreateCustomCurrentObjectDetailView;
            
        }


        private void os_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
                e.Objects = ThinResult.GetList().ToList();
                Console.WriteLine(e.Objects.Count);
        }


        private void View_CreateCustomCurrentObjectDetailView(object sender,
            CreateCustomCurrentObjectDetailViewEventArgs e)
        {
            if (e.ListViewCurrentObject is ThinResult currentRec)
                currentRec.FatResults = HandyFunctions.MakeFatResults(currentRec.Id, View.ObjectSpace);
        }


        protected override void OnDeactivated()
        {
            var os = (NonPersistentObjectSpace) ObjectSpace;
            os.ObjectsGetting -= os_ObjectsGetting;
           // View.CollectionSource.CriteriaApplied -= CollectionSource_CriteriaApplied;
            base.OnDeactivated();

            View.CreateCustomCurrentObjectDetailView -= View_CreateCustomCurrentObjectDetailView;
            HandyFunctions.DisposeAdditionalPersistentObjectSpace(Application, os);
        }

        //private void CollectionSource_CriteriaApplied(object sender, EventArgs e)
        //{
        //     // why is this recursive to 1 level ?

        //     //ObjectSpace.Refresh(); 
            
        //}
    }
}