using System;
using System.Linq;
using DevExpress.ExpressApp;
using ThinLVFatDV.Module.BusinessObjects;
using ThinLVFatDV.Module.Functions;

namespace ThinLVFatDV.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ThinResultObjectController : ObjectViewController<ListView, ThinResult>
    {
        public ThinResultObjectController()
        {
            InitializeComponent();
            TargetObjectType = typeof(ThinResult);
        }

        protected override void OnActivated()
        {
            base.OnActivated();


            var os = (NonPersistentObjectSpace) ObjectSpace;
            os.ObjectsGetting += os_ObjectsGetting;
            HandyFunctions.AddPersistentOsToNonPersistentOs(Application, os, typeof(Thing));
            View.CollectionSource.CriteriaApplied += CollectionSource_CriteriaApplied;

            View.CreateCustomCurrentObjectDetailView += View_CreateCustomCurrentObjectDetailView;
            ObjectSpace.Refresh();
        }


        private void os_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            e.Objects = ThinResult.GetList().ToList();
        }


        private void View_CreateCustomCurrentObjectDetailView(object sender,
            CreateCustomCurrentObjectDetailViewEventArgs e)
        {
            if (e.ListViewCurrentObject is ThinResult currentRec)
                currentRec.FatResult = HandyFunctions.MakeFatResult(currentRec.Id, View.ObjectSpace);
        }


        protected override void OnDeactivated()
        {
            var os = (NonPersistentObjectSpace) ObjectSpace;
            os.ObjectsGetting -= os_ObjectsGetting;
            View.CollectionSource.CriteriaApplied -= CollectionSource_CriteriaApplied;
            base.OnDeactivated();

            View.CreateCustomCurrentObjectDetailView -= View_CreateCustomCurrentObjectDetailView;
            HandyFunctions.DisposeAdditionalPersistentObjectSpace(Application, os);
        }

        private void CollectionSource_CriteriaApplied(object sender, EventArgs e)
        {
            ObjectSpace.Refresh();
        }
    }
}