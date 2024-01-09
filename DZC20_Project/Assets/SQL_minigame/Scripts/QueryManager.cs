using UnityEngine;
using SQLite4Unity3d;
using System.Text;
using System.Collections.Generic;

public class QueryManager : MonoBehaviour
{
    private SQLiteConnection _database;
    private string databasePath;

    void Start()
    {
        // Set the path to the database file
        databasePath = Application.dataPath + "/SQL_minigame/laptopMysteryDatabase.db";
        _database = new SQLiteConnection(databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    public void ExecuteQuery(string query, System.Action<string> onQueryExecuted)
    {
        StringBuilder queryResult = new StringBuilder();

        try
        {
            // Execute the query and get the results
            var results = _database.Query<QueryResult>(query);

            // Process the results
            if (results != null && results.Count > 0)
            {
                foreach (var result in results)
                {
                    // Assuming QueryResult is a class representing your query result
                    // You will need to define this class based on your query structure
                    Debug.Log(result);
                    queryResult.AppendLine(result.ToString());
                }
            }
            else
            {
                queryResult.AppendLine("No results found.");
            }

            onQueryExecuted?.Invoke(queryResult.ToString());
        }
        catch (System.Exception e)
        {
            Debug.LogError("Query Execution Error: " + e.Message);
            onQueryExecuted?.Invoke("Error executing query: " + e.Message);
        }
    }

    private void OnDestroy()
    {
        if (_database != null)
        {
            _database.Close();
        }
    }
}

public class QueryResult
{
    // Assuming a generic structure that might hold a column from any table
    // You can expand this with more properties as per your query requirements

    public int Id { get; set; }
    public string Name { get; set; }
    public string MajorOrSubject { get; set; } // Could be Major (for Students) or Subject (for Classes)
    public string LocationName { get; set; } // For Locations
    public string Time { get; set; } // For LaptopSightings

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        if (Id != 0) sb.AppendLine("Id: " + Id);
        if (!string.IsNullOrEmpty(Name)) sb.AppendLine("Name: " + Name);
        if (!string.IsNullOrEmpty(MajorOrSubject)) sb.AppendLine("Major/Subject: " + MajorOrSubject);
        if (!string.IsNullOrEmpty(LocationName)) sb.AppendLine("Location: " + LocationName);
        if (!string.IsNullOrEmpty(Time)) sb.AppendLine("Time: " + Time);

        return sb.ToString();
    }
}

