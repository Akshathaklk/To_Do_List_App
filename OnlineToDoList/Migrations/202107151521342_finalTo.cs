namespace OnlineToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalTo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoViewModels", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ToDoViewModels", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoViewModels", "Priority");
            DropColumn("dbo.ToDoViewModels", "DueDate");
        }
    }
}
