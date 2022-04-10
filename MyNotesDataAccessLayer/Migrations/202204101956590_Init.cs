namespace MyNotesDataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tblNotes", name: "Category_Id", newName: "CategoryId");
            RenameIndex(table: "dbo.tblNotes", name: "IX_Category_Id", newName: "IX_CategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.tblNotes", name: "IX_CategoryId", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.tblNotes", name: "CategoryId", newName: "Category_Id");
        }
    }
}
