namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredArchitectureAndDataAnnotations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropIndex("dbo.Attendances", new[] { "AttendeeId" });
            DropIndex("dbo.Followings", new[] { "FolloweeId" });
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropPrimaryKey("dbo.Attendances");
            DropPrimaryKey("dbo.Followings");
            DropPrimaryKey("dbo.UserNotifications");
            AddColumn("dbo.Attendances", "Attendee_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Followings", "Followee_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserNotifications", "Notification_Id", c => c.Int());
            AlterColumn("dbo.UserNotifications", "NotificationId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Attendances", "AttendeeId");
            AddPrimaryKey("dbo.Followings", "FolloweeId");
            AddPrimaryKey("dbo.UserNotifications", "NotificationId");
            CreateIndex("dbo.Attendances", "Attendee_Id");
            CreateIndex("dbo.Followings", "Followee_Id");
            CreateIndex("dbo.UserNotifications", "Notification_Id");
            AddForeignKey("dbo.Attendances", "Attendee_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Followings", "Followee_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserNotifications", "Notification_Id", "dbo.Notifications", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "Notification_Id", "dbo.Notifications");
            DropForeignKey("dbo.Followings", "Followee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "Attendee_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserNotifications", new[] { "Notification_Id" });
            DropIndex("dbo.Followings", new[] { "Followee_Id" });
            DropIndex("dbo.Attendances", new[] { "Attendee_Id" });
            DropPrimaryKey("dbo.UserNotifications");
            DropPrimaryKey("dbo.Followings");
            DropPrimaryKey("dbo.Attendances");
            AlterColumn("dbo.UserNotifications", "NotificationId", c => c.Int(nullable: false));
            DropColumn("dbo.UserNotifications", "Notification_Id");
            DropColumn("dbo.Followings", "Followee_Id");
            DropColumn("dbo.Attendances", "Attendee_Id");
            AddPrimaryKey("dbo.UserNotifications", new[] { "UserId", "NotificationId" });
            AddPrimaryKey("dbo.Followings", new[] { "FollowerId", "FolloweeId" });
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AttendeeId" });
            CreateIndex("dbo.UserNotifications", "NotificationId");
            CreateIndex("dbo.Followings", "FolloweeId");
            CreateIndex("dbo.Attendances", "AttendeeId");
            AddForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
