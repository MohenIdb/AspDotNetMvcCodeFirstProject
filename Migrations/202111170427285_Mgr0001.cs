namespace AspDotNetMvcCodeFirstProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mgr0001 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BloodGroups", "Decriptions", c => c.String());
            AddColumn("dbo.Donors", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Patients", "Gender", c => c.String());
            AddColumn("dbo.Patients", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Patients", "PatientImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "PatientImage");
            DropColumn("dbo.Patients", "IsActive");
            DropColumn("dbo.Patients", "Gender");
            DropColumn("dbo.Donors", "IsActive");
            DropColumn("dbo.BloodGroups", "Decriptions");
        }
    }
}
