namespace MyNotesDataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tittle = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 150),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tittle = c.String(nullable: false, maxLength: 160),
                        Text = c.String(nullable: false, maxLength: 2000),
                        isDraft = c.Boolean(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        CategoryId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedUserName = c.String(),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCategories", t => t.CategoryId)
                .ForeignKey("dbo.tblMyNotesUsers", t => t.Owner_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.tblComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 300),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedUserName = c.String(),
                        Note_Id = c.Int(),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblNotes", t => t.Note_Id)
                .ForeignKey("dbo.tblMyNotesUsers", t => t.Owner_Id)
                .Index(t => t.Note_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.tblMyNotesUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        LastName = c.String(maxLength: 30),
                        UserName = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        ActivateGuid = c.Guid(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        ProfileImageFileName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        ModifiedUserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblLikeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikedUser_Id = c.Int(),
                        Note_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblMyNotesUsers", t => t.LikedUser_Id)
                .ForeignKey("dbo.tblNotes", t => t.Note_Id)
                .Index(t => t.LikedUser_Id)
                .Index(t => t.Note_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblNotes", "Owner_Id", "dbo.tblMyNotesUsers");
            DropForeignKey("dbo.tblLikeds", "Note_Id", "dbo.tblNotes");
            DropForeignKey("dbo.tblLikeds", "LikedUser_Id", "dbo.tblMyNotesUsers");
            DropForeignKey("dbo.tblComment", "Owner_Id", "dbo.tblMyNotesUsers");
            DropForeignKey("dbo.tblComment", "Note_Id", "dbo.tblNotes");
            DropForeignKey("dbo.tblNotes", "CategoryId", "dbo.tblCategories");
            DropIndex("dbo.tblLikeds", new[] { "Note_Id" });
            DropIndex("dbo.tblLikeds", new[] { "LikedUser_Id" });
            DropIndex("dbo.tblComment", new[] { "Owner_Id" });
            DropIndex("dbo.tblComment", new[] { "Note_Id" });
            DropIndex("dbo.tblNotes", new[] { "Owner_Id" });
            DropIndex("dbo.tblNotes", new[] { "CategoryId" });
            DropTable("dbo.tblLikeds");
            DropTable("dbo.tblMyNotesUsers");
            DropTable("dbo.tblComment");
            DropTable("dbo.tblNotes");
            DropTable("dbo.tblCategories");
        }
    }
}
