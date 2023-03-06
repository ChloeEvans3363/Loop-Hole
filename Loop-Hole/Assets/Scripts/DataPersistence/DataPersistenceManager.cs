using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{

    private GameData gameData;
    private List<IDataPersistence> dataPersistencesObjects;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();

        // Temporary to test out if the save function works
        LoadGame();
    }

    public void LoadGame()
    {
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            this.gameData = new GameData();
        }

        foreach(IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach(IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        Debug.Log(gameData.highScore);
    }

    private void OnApplicationQuit()
    {
        // Temporary to test out if the save function works
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistencesObects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistencesObects);
    }
}
