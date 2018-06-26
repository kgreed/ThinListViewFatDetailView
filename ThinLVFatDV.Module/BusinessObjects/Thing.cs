using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;

namespace ThinLVFatDV.Module.BusinessObjects
{
    [NavigationItem("Things")]
    [Table("Things")]
    public class Thing
    {
        [Key]
        public int Id { get; set; }
        public string ThingName { get; set; }
        public int ParentId { get; set; }
    }
}