using Microsoft.EntityFrameworkCore;
using reviseAuthentication.Data;
using reviseAuthentication.Models;
namespace reviseAuthentication.Controllers.Endpoints;

public static class StudentModelEndpoints
{
    public static void MapStudentModelEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/StudentModel", async (MyDbContext db) =>
        {
            return await db.StudentModel.ToListAsync();
        })
        .WithName("GetAllStudentModels")
        .Produces<List<StudentModel>>(StatusCodes.Status200OK);

        routes.MapGet("/api/StudentModel/{id}", async (int Id, MyDbContext db) =>
        {
            return await db.StudentModel.FindAsync(Id)
                is StudentModel model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetStudentModelById")
        .Produces<StudentModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/StudentModel/{id}", async (int Id, StudentModel studentModel, MyDbContext db) =>
        {
            var foundModel = await db.StudentModel.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(studentModel);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateStudentModel")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/StudentModel/", async (StudentModel studentModel, MyDbContext db) =>
        {
            db.StudentModel.Add(studentModel);
            await db.SaveChangesAsync();
            return Results.Created($"/StudentModels/{studentModel.Id}", studentModel);
        })
        .WithName("CreateStudentModel")
        .Produces<StudentModel>(StatusCodes.Status201Created);

        routes.MapDelete("/api/StudentModel/{id}", async (int Id, MyDbContext db) =>
        {
            if (await db.StudentModel.FindAsync(Id) is StudentModel studentModel)
            {
                db.StudentModel.Remove(studentModel);
                await db.SaveChangesAsync();
                return Results.Ok(studentModel);
            }

            return Results.NotFound();
        })
        .WithName("DeleteStudentModel")
        .Produces<StudentModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
