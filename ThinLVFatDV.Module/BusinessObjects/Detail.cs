using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;

namespace ThinLVFatDV.Module.BusinessObjects
{
    [NavigationItem("Things")]
    [Table("Details")]
    public class Detail
    {
      
         [Key]
        public int Id { get; set; }
        public string Point1 { get; set; }
        public string Point2 { get; set; }

        [ForeignKey("ThingId")]
        public virtual Thing Thing { get; set; }
        public int ThingId { get; set; }
    }
}