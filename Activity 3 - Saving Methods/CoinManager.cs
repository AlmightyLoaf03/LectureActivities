using System.Collections;
using System.Collections.Generic;
using System.IO; //gives access to files inside the computer
using System.Runtime.Serialization.Formatters.Binary; //allows the converstion of objects to binary data
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Serializable class that holds the data we want to save
[System.Serializable]
public class SaveData
{
    public int coins;
}

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance; //singleton pattern allows other scripts to access this easily

    private int coins;
    [SerializeField] private TextMeshProUGUI coinsDisplay;
    [SerializeField] private TextMeshProUGUI messageDisplay;

    private string jsonPath;

    private void Awake()
    {
        //checks if there is only one CoinManger in the scene
        if(!instance)
        {
            instance = this;
        }
        else
            Destroy(gameObject); 
    }

    private void Start()
    {
        //builds the path for JSON file in persistent data folder
        jsonPath = Application.persistentDataPath + "/coinsave.json";
        UpdateUI();
    }

    private void UpdateUI()
    {
        coinsDisplay.text = coins.ToString();
    }

    public void ChangeCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    private void ShowMessage(string msg)
    {
        if(messageDisplay != null)
        {
            messageDisplay.text = msg;
            CancelInvoke(nameof(ClearMessage));
            Invoke(nameof(ClearMessage), 2f);
        }
    }

    private void ClearMessage()
    {
        if (messageDisplay != null)
            messageDisplay.text = "";
    }

    #region PLAYERPREFS
    public void SavePLayerPrefs()
    {
        // Save the current coin count in PlayerPrefs under the key "Coins"
        PlayerPrefs.SetInt("Coins", coins);

        // Force PlayerPrefs to actually write data to disk (not just memory)
        PlayerPrefs.Save();

        ShowMessage("Saved with PlayerPrefs");
    }

    public void LoadPlayerPrefs()
    {
        // Check if a save exists in PlayerPrefs with the key "Coins"
        if (PlayerPrefs.HasKey("Coins"))
        {
            // If it exists, load the saved coin value
            coins = PlayerPrefs.GetInt("Coins");

            // Update the UI so the player sees the restored coin count
            UpdateUI();

            ShowMessage("Loaded with PlayerPrefs");
        }
        else
            ShowMessage("No PlayerPrefs save found");
    }
    #endregion



    #region JSON
    public void SaveJSON()
    {
        // Create a data object that will hold our coin count
        SaveData data = new SaveData { coins = coins };

        // Convert the SaveData object into a JSON string (formatted nicely with 'true')
        string json = JsonUtility.ToJson(data, true);

        // Write the JSON string into a file on disk (jsonPath = a path we set earlier)
        File.WriteAllText(jsonPath, json);

        ShowMessage("Saved with JSON");
    }

    public void LoadJSON()
    {
        // Check if the JSON save file exists
        if (File.Exists(jsonPath))
        {
            // Read the text from the file
            string json = File.ReadAllText(jsonPath);

            // Convert the JSON text back into a SaveData object
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Restore the coin value from the SaveData object
            coins = data.coins;

            // Update the UI to show the loaded value
            UpdateUI();

            ShowMessage("Loaded with JSON");
        }
        else
            ShowMessage("No JSON save found");
    }
    #endregion



    #region BINARY
    public void SaveBinary()
    {
        // Create a SaveData object to hold the coin value
        SaveData data = new SaveData { coins = coins };

        // BinaryFormatter is used to turn objects into binary data (0s and 1s)
        BinaryFormatter formatter = new BinaryFormatter();

        // Path where the save file will be stored (persistentDataPath works on all devices)
        string path = Application.persistentDataPath + "/coinsave.dat";

        // Create a new file (overwrite if it already exists)
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            // Write (serialize) the SaveData object into binary form inside the file
            formatter.Serialize(stream, data);
        }

        ShowMessage("Saved with Binary");
    }

    public void LoadBinary()
    {
        // Path where the binary save file should exist
        string path = Application.persistentDataPath + "/coinsave.dat";

        // Check if the file exists
        if (File.Exists(path))
        {
            // Create a formatter to read binary data
            BinaryFormatter formatter = new BinaryFormatter();

            // Open the file to read its contents
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                // Convert (deserialize) the binary data back into a SaveData object
                SaveData data = (SaveData)formatter.Deserialize(stream);

                // Restore the coin count from the SaveData object
                coins = data.coins;
            }

            // Update the UI to reflect the loaded coin count
            UpdateUI();

            ShowMessage("Loaded with Binary");
        }
        else
            ShowMessage("No Binary save found");
    }
    #endregion


    #region RESET
    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    #endregion

    #region KEYBOARD SHORTCUTS
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SavePLayerPrefs();
        if (Input.GetKeyDown(KeyCode.Alpha2)) LoadPlayerPrefs();
        if (Input.GetKeyDown(KeyCode.Alpha3)) SaveJSON();
        if (Input.GetKeyDown(KeyCode.Alpha4)) LoadJSON();
        if (Input.GetKeyDown(KeyCode.Alpha5)) SaveBinary();
        if (Input.GetKeyDown(KeyCode.Alpha6)) LoadBinary();
        if (Input.GetKeyDown(KeyCode.R)) ResetScene();
    }
    #endregion
}
