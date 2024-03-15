using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

public class HomeController : Controller
{
    private readonly string _connectionString = "Server=localhost;Database=nesa1;Uid=root;Pwd=;";

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RetrieveData()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand("SELECT file_content FROM TextFiles", connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var fileContent = reader.GetString("file_content");
                    var textFile = new TextFile { FileContent = fileContent };
                    return View("Data", textFile);
                }
            }
        }

        return View("Data", new TextFile());
    }
}
