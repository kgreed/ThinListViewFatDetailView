using System.Data.Entity;
using System.Linq;
using DevExpress.ExpressApp.EF.Updating;

namespace ThinLVFatDV.Module.BusinessObjects
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(string connectionString)
            : base(connectionString)
        {
            SetInitializer();
        }

        public MyDbContext()
            : base("name=ConnectionString")
        {
            SetInitializer();
        }

        private static void SetInitializer()
        {
#if DEBUG
            Database.SetInitializer(new DropCreateIfChangeInitializer());
#else
        Database.SetInitializer<MyDbContext> (new CreateInitializer ());
#endif
        }


        public DbSet<ModuleInfo> ModulesInfo { get; set; }

        public DbSet<Thing> Things { get; set; }

        public DbSet<Detail> Details { get; set; }


        public void Seed(MyDbContext Context)
        {
#if DEBUG
            // Create my debug (testing) objects here
            //var TestMyClass = new MyClass() { ...};
            //Context.MyClasses.Add(TestMyClass);
#endif

            // Normal seeding goes here

            Context.Things.Add(new Thing {ThingName = "Red Thing"});
            Context.SaveChanges();
            var redThing = Context.Things.Single();
            Context.Things.Add(new Thing {ThingName = "Blue Thing"});
            Context.Things.Add(new Thing {ThingName = "Green Thing", ParentId = redThing.Id});
            Context.Details.Add(new Detail {Point1 = "happy", Point2 = "frog", Thing = redThing});
            Context.Details.Add(new Detail {Point1 = "green", Point2 = "kite", Thing = redThing});
            Context.SaveChanges();
        }
    }

    public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }

    public class CreateInitializer : CreateDatabaseIfNotExists<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }
}