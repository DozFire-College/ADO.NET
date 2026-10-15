using college_DB.Data;
using College_DB.Data;

var builder = WebApplication.CreateBuilder(args);
string connection_string = "Data Source=COMP11A1\\SQLEXPRESS;" +
            "Initial Catalog=College_DB;" +
            "Integrated Security=True;" +
            "TrustServerCertificate=True;";
StudentRepository stud_repo = new StudentRepository(connection_string);
GroupRepository group_repo = new GroupRepository(connection_string);
builder.Services.AddRazorPages();
builder.Services.AddSingleton(stud_repo);
builder.Services.AddSingleton(group_repo);
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.MapGet("students/all", () =>
{
    var students = stud_repo.GetAllStudents();
    return Results.Ok(students);
});
app.MapGet("groups/all", () =>
{
    var groups = group_repo.GetAllGroups();
    return Results.Ok(groups);
});
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
