using College_DB.Data;

var builder = WebApplication.CreateBuilder(args);

string conn_str =
        "Data Source=COMP11A1\\SQLEXPRESS;" +
        "Initial Catalog=College;" +
        "Integrated Security=True;" +
        "TrustServerCertificate=True;";

StudentRepository student_repo = new StudentRepository(conn_str);

GroupRepository group_repo = new GroupRepository(conn_str);

builder.Services.AddRazorPages();
builder.Services.AddSingleton(student_repo);
builder.Services.AddSingleton(group_repo);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapGet("students/all", () => { 
    var students = student_repo.GetAllStudents();
    return Results.Ok(students);
});

app.MapGet("groups/all", () => {
    var groups = group_repo.GetAllGroups();
    return Results.Ok(groups);
});

app.MapPost("groups/{id}", (string id) =>
{
    var group = group_repo.GetGroupById(id);
    return Results.Ok(group);
});

app.UseHttpsRedirection();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
