using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

namespace ThinLVFatDV.Module.BusinessObjects
{
    [NavigationItem("Things")]
    [DomainComponent]
     
    public class ThinResult
    {
        public int Id { get; set; }
        public string ThingName { get; set; }

        public string ParentThingName { get; set; }

        [Browsable(false)]
        public virtual FatResult FatResult { get; set; }
        public static ThinResult[] GetList()
        {
            using (var connect = new MyDbContext())
            {
                const string sql = @"select t.Id, t.ThingName , p.ThingName as ParentThingName from things t left outer join things p on t.ParentId = p.Id ";

                return connect.Database.SqlQuery<ThinResult>(sql).ToArray();
            }
        }

        [VisibleInListView(false)]
        [ModelDefault("RowCount", "4")]
        public string Notes
        {
            get => FatResult?.Notes;
            set => FatResult.Notes = value;
        }
    }

   
}