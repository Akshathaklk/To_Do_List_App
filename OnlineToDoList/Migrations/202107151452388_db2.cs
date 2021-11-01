namespace OnlineToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoViewModels", "StartDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoViewModels", "StartDate");
        }
    }
}
