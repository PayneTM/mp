namespace WannaTravel.Repositories.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedunnecessarystaff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RestaurantCuisineDboes", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantCuisineDboes", "CuisineDbo_Id", "dbo.CuisineDboes");
            DropForeignKey("dbo.FeatureDboRestaurants", "FeatureDbo_Id", "dbo.FeatureDboes");
            DropForeignKey("dbo.FeatureDboRestaurants", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.MealDboRestaurants", "MealDbo_Id", "dbo.MealDboes");
            DropForeignKey("dbo.MealDboRestaurants", "Restaurant_Id", "dbo.Restaurants");
            DropIndex("dbo.RestaurantCuisineDboes", new[] { "Restaurant_Id" });
            DropIndex("dbo.RestaurantCuisineDboes", new[] { "CuisineDbo_Id" });
            DropIndex("dbo.FeatureDboRestaurants", new[] { "FeatureDbo_Id" });
            DropIndex("dbo.FeatureDboRestaurants", new[] { "Restaurant_Id" });
            DropIndex("dbo.MealDboRestaurants", new[] { "MealDbo_Id" });
            DropIndex("dbo.MealDboRestaurants", new[] { "Restaurant_Id" });
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentatorName = c.String(),
                        RestaurantId = c.Int(nullable: false),
                        Text = c.String(),
                        Rate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            AddColumn("dbo.AspNetUsers", "Salt", c => c.String());
            DropTable("dbo.CuisineDboes");
            DropTable("dbo.FeatureDboes");
            DropTable("dbo.MealDboes");
            DropTable("dbo.RestaurantCuisineDboes");
            DropTable("dbo.FeatureDboRestaurants");
            DropTable("dbo.MealDboRestaurants");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MealDboRestaurants",
                c => new
                    {
                        MealDbo_Id = c.Int(nullable: false),
                        Restaurant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MealDbo_Id, t.Restaurant_Id });
            
            CreateTable(
                "dbo.FeatureDboRestaurants",
                c => new
                    {
                        FeatureDbo_Id = c.Int(nullable: false),
                        Restaurant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FeatureDbo_Id, t.Restaurant_Id });
            
            CreateTable(
                "dbo.RestaurantCuisineDboes",
                c => new
                    {
                        Restaurant_Id = c.Int(nullable: false),
                        CuisineDbo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Restaurant_Id, t.CuisineDbo_Id });
            
            CreateTable(
                "dbo.MealDboes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeatureDboes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CuisineDboes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Comments", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Comments", new[] { "RestaurantId" });
            DropColumn("dbo.AspNetUsers", "Salt");
            DropTable("dbo.Comments");
            CreateIndex("dbo.MealDboRestaurants", "Restaurant_Id");
            CreateIndex("dbo.MealDboRestaurants", "MealDbo_Id");
            CreateIndex("dbo.FeatureDboRestaurants", "Restaurant_Id");
            CreateIndex("dbo.FeatureDboRestaurants", "FeatureDbo_Id");
            CreateIndex("dbo.RestaurantCuisineDboes", "CuisineDbo_Id");
            CreateIndex("dbo.RestaurantCuisineDboes", "Restaurant_Id");
            AddForeignKey("dbo.MealDboRestaurants", "Restaurant_Id", "dbo.Restaurants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MealDboRestaurants", "MealDbo_Id", "dbo.MealDboes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FeatureDboRestaurants", "Restaurant_Id", "dbo.Restaurants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FeatureDboRestaurants", "FeatureDbo_Id", "dbo.FeatureDboes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RestaurantCuisineDboes", "CuisineDbo_Id", "dbo.CuisineDboes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RestaurantCuisineDboes", "Restaurant_Id", "dbo.Restaurants", "Id", cascadeDelete: true);
        }
    }
}
