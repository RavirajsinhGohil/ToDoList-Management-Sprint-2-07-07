using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoListManagement.Entity.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    ErrorLogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.ErrorLogId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    LeaveId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestedUserId = table.Column<int>(type: "integer", nullable: true),
                    ApprovalUserId = table.Column<int>(type: "integer", nullable: true),
                    Reason = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    AlternatePhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsAvailableOnPhone = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.LeaveId);
                    table.ForeignKey(
                        name: "FK_Leaves_Users_ApprovalUserId",
                        column: x => x.ApprovalUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Leaves_Users_RequestedUserId",
                        column: x => x.RequestedUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CanView = table.Column<bool>(type: "boolean", nullable: false),
                    CanAddEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CanDelete = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Permissions_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    AssignedToProgramManager = table.Column<int>(type: "integer", nullable: true),
                    AssignedToScrumMaster = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Users_AssignedToProgramManager",
                        column: x => x.AssignedToProgramManager,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Projects_Users_AssignedToScrumMaster",
                        column: x => x.AssignedToScrumMaster,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Projects_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    ProjectUserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => x.ProjectUserId);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    SprintId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SprintName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ScrumMasterId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.SprintId);
                    table.ForeignKey(
                        name: "FK_Sprints_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sprints_Users_ScrumMasterId",
                        column: x => x.ScrumMasterId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDoLists",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: true),
                    SprintId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    AssignedTo = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Priority = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoLists", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_ToDoLists_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK_ToDoLists_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "SprintId");
                    table.ForeignKey(
                        name: "FK_ToDoLists_Users_AssignedTo",
                        column: x => x.AssignedTo,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ToDoLists_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "TaskAttachments",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: true),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsImage = table.Column<bool>(type: "boolean", nullable: true),
                    UploadedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UploadedBy = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAttachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_TaskAttachments_ToDoLists_TaskId",
                        column: x => x.TaskId,
                        principalTable: "ToDoLists",
                        principalColumn: "TaskId");
                    table.ForeignKey(
                        name: "FK_TaskAttachments_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_TaskAttachments_Users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedAt", "CreatedBy", "IsDeleted", "RoleName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2547), null, false, "Admin", null, null },
                    { 2, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2595), null, false, "Program Manager", null, null },
                    { 3, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2612), null, false, "Member", null, null },
                    { 4, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2629), null, false, "ScrumMaster", null, null }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "CanAddEdit", "CanDelete", "CanView", "CreatedAt", "CreatedBy", "IsDeleted", "PermissionName", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, true, true, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2659), null, false, "Projects", 1, null, null },
                    { 2, true, true, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2696), null, false, "Employees", 1, null, null },
                    { 3, true, true, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2715), null, false, "Task Board", 1, null, null },
                    { 4, true, true, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2732), null, false, "Role And Permissions", 1, null, null },
                    { 5, true, true, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2751), null, false, "Leave", 1, null, null },
                    { 6, false, false, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2772), null, false, "Projects", 2, null, null },
                    { 7, true, true, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2791), null, false, "Employees", 2, null, null },
                    { 8, true, true, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2808), null, false, "Task Board", 2, null, null },
                    { 9, false, false, false, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2825), null, false, "Role And Permissions", 2, null, null },
                    { 10, true, true, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2843), null, false, "Leave", 2, null, null },
                    { 11, false, false, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2862), null, false, "Projects", 3, null, null },
                    { 12, false, false, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2879), null, false, "Employees", 3, null, null },
                    { 13, false, false, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2898), null, false, "Task Board", 3, null, null },
                    { 14, false, false, false, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2915), null, false, "Role And Permissions", 3, null, null },
                    { 15, true, true, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2932), null, false, "Leave", 3, null, null },
                    { 16, false, false, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2950), null, false, "Projects", 4, null, null },
                    { 17, false, false, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2966), null, false, "Employees", 4, null, null },
                    { 18, false, false, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(2986), null, false, "Task Board", 4, null, null },
                    { 19, false, false, false, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(3004), null, false, "Role And Permissions", 4, null, null },
                    { 20, true, true, true, new DateTime(2025, 7, 7, 8, 36, 21, 880, DateTimeKind.Utc).AddTicks(3021), null, false, "Leave", 4, null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "IsActive", "IsDeleted", "Name", "PasswordHash", "PhoneNumber", "RoleId" },
                values: new object[] { 1, "admin@outlook.com", true, false, "Admin", "$2a$11$YCiiJxUwumHUtegC05ahFej29UzVm/s1HRwPriPIuta4b.GWddWuW", "9988556644", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_ApprovalUserId",
                table: "Leaves",
                column: "ApprovalUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_RequestedUserId",
                table: "Leaves",
                column: "RequestedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreatedBy",
                table: "Permissions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UpdatedBy",
                table: "Permissions",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AssignedToProgramManager",
                table: "Projects",
                column: "AssignedToProgramManager");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AssignedToScrumMaster",
                table: "Projects",
                column: "AssignedToScrumMaster");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedBy",
                table: "Projects",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_ProjectId",
                table: "ProjectUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_UserId",
                table: "ProjectUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_ProjectId",
                table: "Sprints",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_ScrumMasterId",
                table: "Sprints",
                column: "ScrumMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_DeletedBy",
                table: "TaskAttachments",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_TaskId",
                table: "TaskAttachments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_UploadedBy",
                table: "TaskAttachments",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_AssignedTo",
                table: "ToDoLists",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_CreatedBy",
                table: "ToDoLists",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_ProjectId",
                table: "ToDoLists",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_SprintId",
                table: "ToDoLists",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "Leaves");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "TaskAttachments");

            migrationBuilder.DropTable(
                name: "ToDoLists");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
