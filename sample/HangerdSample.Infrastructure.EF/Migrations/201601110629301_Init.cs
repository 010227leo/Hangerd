namespace HangerdSample.Infrastructure.EF.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class Init : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Account",
				c => new
				{
					Id = c.String(nullable: false, maxLength: 32),
					LoginName = c.String(nullable: false, maxLength: 50),
					EncryptedPassword = c.String(nullable: false, maxLength: 32),
					Name = c.String(nullable: false, maxLength: 20),
					IsDeleted = c.Boolean(nullable: false),
					CreateTime = c.DateTime(nullable: false),
					LastModified = c.DateTime(nullable: false),
				})
				.PrimaryKey(t => t.Id);
		}

		public override void Down()
		{
			DropTable("dbo.Account");
		}
	}
}
