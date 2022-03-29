﻿namespace MVCVechicleLoanProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMIgrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Passsword = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Phone = c.String(nullable: false, maxLength: 13),
                        Email = c.String(nullable: false, maxLength: 30),
                        City = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        VehicleBrand = c.String(nullable: false, maxLength: 30),
                        Model = c.String(nullable: false, maxLength: 20),
                        OnRoadPrice = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.LoanID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Loans", new[] { "CustomerID" });
            DropTable("dbo.Loans");
            DropTable("dbo.Customers");
            DropTable("dbo.Admins");
        }
    }
}
