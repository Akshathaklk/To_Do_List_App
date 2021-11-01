namespace OnlineToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalTodo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoViewModels", "TaskPriority", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoViewModels", "TaskPriority");
        }
    }
}
