using College_DB.Data;

var builder = WebApplication.CreateBuilder(args);
string connection_string = "Data Source=COMP11A1\\SQLEXPRESS;" +
            "Initial Catalog=College_DB;" +
            "Integrated Security=True;" +
            "TrustServerCertificate=True;";
StudentRepository stud_repo = new StudentRepository(connection_string);

builder.Services.AddRazorPages();
builder.Services.AddSingleton(stud_repo);
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
