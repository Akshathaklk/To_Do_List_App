namespace OnlineToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finl : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ToDoViewModels", "Priority");
            DropColumn("dbo.ToDoViewModels", "TaskPriority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToDoViewModels", "TaskPriority", c => c.String());
            AddColumn("dbo.ToDoViewModels", "Priority", c => c.Int(nullable: false));
        }
    }
}
