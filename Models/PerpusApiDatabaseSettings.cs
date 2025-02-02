namespace PerpusApi.Models;

public class PerpusApiDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string BooksCollectionName { get; set; } = null!;
    
    public string MembersCollectionName { get; set; } = null!;
}