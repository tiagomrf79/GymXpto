using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkoutTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Workout_WorkoutId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Workout_Routines_RoutineId",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workout",
                table: "Workout");

            migrationBuilder.RenameTable(
                name: "Workout",
                newName: "Workouts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workouts",
                newName: "WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_RoutineId",
                table: "Workouts",
                newName: "IX_Workouts_RoutineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts",
                column: "WorkoutId");

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "WorkoutId", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "RoutineId", "Title" },
                values: new object[,]
                {
                    { new Guid("40ea0d4b-aa1f-4128-8ea3-3a63e0b01164"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"), "Upper body" },
                    { new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("baf3caf7-b1e2-4b50-ba93-b41677751d98"), "Lower body" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Workouts_WorkoutId",
                table: "Group",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "WorkoutId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Routines_RoutineId",
                table: "Workouts",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "RoutineId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Workouts_WorkoutId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Routines_RoutineId",
                table: "Workouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workouts",
                table: "Workouts");

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "WorkoutId",
                keyValue: new Guid("40ea0d4b-aa1f-4128-8ea3-3a63e0b01164"));

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "WorkoutId",
                keyValue: new Guid("e7b65a83-9fa1-4702-a2cd-6efc8955af83"));

            migrationBuilder.RenameTable(
                name: "Workouts",
                newName: "Workout");

            migrationBuilder.RenameColumn(
                name: "WorkoutId",
                table: "Workout",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Workouts_RoutineId",
                table: "Workout",
                newName: "IX_Workout_RoutineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workout",
                table: "Workout",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Workout_WorkoutId",
                table: "Group",
                column: "WorkoutId",
                principalTable: "Workout",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_Routines_RoutineId",
                table: "Workout",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "RoutineId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
