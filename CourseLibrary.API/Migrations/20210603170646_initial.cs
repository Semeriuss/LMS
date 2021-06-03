using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseLibrary.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    MainCategory = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    ContentId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: true),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    AuthorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Data = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CourseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Content_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Content_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CourseRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    CourseId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRatings_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CourseRatings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTimeOffset(new DateTime(1650, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Berry", "Griffin Beak Eldritch", "Ships" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("380c2c6b-0d1c-4b82-9d83-3cf635a3e62b"), new DateTimeOffset(new DateTime(1977, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Mabel", "Barnacle Grendel", "Maps" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("d94a64c2-2e8f-4162-9976-0ffe03d30767"), new DateTimeOffset(new DateTime(1976, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Hagley", "Imposter Grendel", "Singing" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("28c1db41-f104-46e6-8943-d31c0291e0e3"), new DateTimeOffset(new DateTime(1982, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Rushford", "Subtle Asema", "Rum" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("71838f8b-6ab3-4539-9e67-4e77b8ede1c0"), new DateTimeOffset(new DateTime(1969, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Huxford", "The Hawk Morris", "Maps" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"), new DateTimeOffset(new DateTime(1721, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Atherton", "Crow Ridley", "Rum" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("119f9ccb-149d-4d3c-ad4f-40100f38e918"), new DateTimeOffset(new DateTime(1972, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Dwennon", "Rigger Quye", "Maps" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"), new DateTimeOffset(new DateTime(1690, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Seabury", "Toxic Reyson", "Maps" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"), new DateTimeOffset(new DateTime(1702, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Arnold", "The Unseen Stafford", "Singing" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), new DateTimeOffset(new DateTime(1701, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Eli", "Ivory Bones Sweet", "Singing" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), new DateTimeOffset(new DateTime(1668, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Nancy", "Swashbuckler Rye", "Rum" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "MainCategory" },
                values: new object[] { new Guid("2aadd2df-7caf-45ab-9355-7f6332985a87"), new DateTimeOffset(new DateTime(1723, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Rutherford", "Fearless Cloven", "General debauchery" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"), "In this course you'll learn how to program a software without sounding like you actually know the words or how to hold a note.", "Computer Science" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "In this course you'll learn how to calculate favourite pirate songs without sounding like you actually know the words or how to hold a note.", "Mathematics" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "In this course you'll learn nature and it's science without sounding like you actually know the words or how to hold a note.", "Natural Science" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "In this course you'll learn the universe and beyond favourite pirate songs without sounding like you actually know the words or how to hold a note.", "Astronomy" });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "ContentId", "FileName", "FilePath" },
                values: new object[] { new Guid("d28898e9-2ba9-473a-a40f-e38cd54f9b35"), new Guid("00000000-0000-0000-0000-000000000000"), "Test", "apathsubpath" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Description", "Title", "UserId", "Username" },
                values: new object[] { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), null, new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Commandeering a ship in rough waters isn't easy.  Commandeering it without getting caught is even harder.  In this course you'll learn how to sail away and avoid those pesky musketeers.", "Commandeering a Ship Without Getting Caught", 1, "Try" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Description", "Title", "UserId", "Username" },
                values: new object[] { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), null, new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "In this course, the author provides tips to avoid, or, if needed, overthrow pirate mutiny.", "Overthrowing Mutiny", 4, "Aben-bel" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Description", "Title", "UserId", "Username" },
                values: new object[] { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), null, new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Every good pirate loves rum, but it also has a tendency to get you into trouble.  In this course you'll learn how to avoid that.  This new exclusive edition includes an additional chapter on how to run fast without falling while drunk.", "Avoiding Brawls While Drinking as Much Rum as You Desire", 1, "Try" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Description", "Title", "UserId", "Username" },
                values: new object[] { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), null, new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), "In this course you'll learn how to sing all-time favourite pirate songs without sounding like you actually know the words or how to hold a note.", "Singalong Pirate Hits", 4, "Aben-bel" });

            migrationBuilder.InsertData(
                table: "Content",
                columns: new[] { "Id", "CategoryId", "CourseId", "Data", "Title", "Type" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b00"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), "Lecture1.mp4", "OOP II", "Video" });

            migrationBuilder.InsertData(
                table: "Content",
                columns: new[] { "Id", "CategoryId", "CourseId", "Data", "Title", "Type" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b01"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), "Lecture1.mp4", "Web", "Video" });

            migrationBuilder.InsertData(
                table: "Content",
                columns: new[] { "Id", "CategoryId", "CourseId", "Data", "Title", "Type" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b03"), new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), "Lecture1.mp4", "DLD", "Video" });

            migrationBuilder.InsertData(
                table: "Content",
                columns: new[] { "Id", "CategoryId", "CourseId", "Data", "Title", "Type" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b02"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), "Lecture1.mp4", "System", "Video" });

            migrationBuilder.InsertData(
                table: "Content",
                columns: new[] { "Id", "CategoryId", "CourseId", "Data", "Title", "Type" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b04"), new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), "Lecture1.mp4", "Dyna", "Video" });

            migrationBuilder.InsertData(
                table: "CourseRatings",
                columns: new[] { "Id", "CategoryId", "CourseId", "UserId", "Value" },
                values: new object[] { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ef"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), 2, 3.0 });

            migrationBuilder.InsertData(
                table: "CourseRatings",
                columns: new[] { "Id", "CategoryId", "CourseId", "UserId", "Value" },
                values: new object[] { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869b"), new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), 3, 4.0 });

            migrationBuilder.InsertData(
                table: "CourseRatings",
                columns: new[] { "Id", "CategoryId", "CourseId", "UserId", "Value" },
                values: new object[] { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6d"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), 2, 4.0 });

            migrationBuilder.InsertData(
                table: "CourseRatings",
                columns: new[] { "Id", "CategoryId", "CourseId", "UserId", "Value" },
                values: new object[] { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad98"), new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), 3, 1.0 });

            migrationBuilder.InsertData(
                table: "CourseRatings",
                columns: new[] { "Id", "CategoryId", "CourseId", "UserId", "Value" },
                values: new object[] { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad99"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), 5, 3.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Content_CategoryId",
                table: "Content",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Content_CourseId",
                table: "Content",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRatings_CategoryId",
                table: "CourseRatings",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRatings_CourseId",
                table: "CourseRatings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "CourseRatings");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
