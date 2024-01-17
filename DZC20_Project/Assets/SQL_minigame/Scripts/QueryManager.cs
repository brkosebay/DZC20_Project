using UnityEngine;
using SQLite4Unity3d;
using System.Text;
using System.Collections.Generic;
using System;

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

    public void ExecuteLocationQuery(string query, System.Action<List<LocationResult>> onQueryExecuted)
    {
        try
        {
            var results = _database.Query<LocationResult>(query);
            Debug.Log(results);
            onQueryExecuted?.Invoke(results);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Location Query Execution Error: " + e.Message);
        }
    }

    public void ExecuteClassQuery(string query, System.Action<List<ClassResult>> onQueryExecuted)
    {
        try
        {
            var results = _database.Query<ClassResult>(query);
            Debug.Log(results);
            onQueryExecuted?.Invoke(results);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Class Query Execution Error: " + e.Message);
        }
    }

    public void ExecuteSightingQuery(string query, System.Action<List<SightingResult>> onQueryExecuted)
    {
        try
        {
            Debug.Log("Executing query: " + query);
            var results = _database.Query<SightingResult>(query);
            onQueryExecuted?.Invoke(results);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Sighting Query Execution Error: " + e.Message);
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

public class LocationResult
{
    public string name { get; set; }

    public override string ToString()
    {
        return "Location: " + name;
    }
}

public class ClassResult
{
    public string name { get; set; }
    public string professor_id { get; set; }

    public override string ToString()
    {
        return "Class Name: " + name + ", Professor ID: " + professor_id;
    }
}

public class ProfessorResult
{
    public string name { get; set; }

    public override string ToString()
    {
        return "Professor Name: " + name;
    }
}

public class StudentResult
{
    public string name { get; set; }
    public string major { get; set; }

    public override string ToString()
    {
        return "Student Name: " + name + ", Major: " + major;
    }
}

public class SightingResult
{
    public string time { get; set; }
    public string location_id { get; set; }
    public string witness_id { get; set; }
    public override string ToString()
    {
        return "Time: " + time + ", Location ID: " + location_id + ", Witness ID: " + witness_id;
    }
}


