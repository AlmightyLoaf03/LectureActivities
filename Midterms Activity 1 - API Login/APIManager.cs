using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    [Header("API Settings")]
    [SerializeField] private string baseUrl = "https://retoolapi.dev/0XSfSb/LogCreds"; // place base url here

    [Header("Login UI")]
    [SerializeField] private TMP_InputField loginUsername;
    [SerializeField] private TMP_InputField loginPassword;
    [SerializeField] private TMP_Text loginFeedback;

    [Header("Register UI")]
    [SerializeField] private TMP_InputField registerUsername;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private TMP_Text registerFeedback;

    public void OnRegisterClicked()
    {
        //get the registered Username and trim removes spaces
        string username = registerUsername.text.Trim(); 
        string password = registerPassword.text.Trim();

        //checks if input field is empty
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            registerFeedback.text = "Please enter username and password.";
            return;
        }

        StartCoroutine(CheckUserExists(username, password));
    }

    //coroutine that checks if a username already exists
    private IEnumerator CheckUserExists(string username, string password)
    {
        string url = $"{baseUrl}?username={username}"; // API query to filter by username
        UnityWebRequest req = UnityWebRequest.Get(url); // Send GET request

        yield return req.SendWebRequest(); // Wait for server response

        if (req.result == UnityWebRequest.Result.Success)
        {
            string jsonResult = req.downloadHandler.text; // gives the raw JSON text returned from the API
            User[] existingUsers = JsonHelper.FromJson<User>(jsonResult); //converts that JSON string into an array of User objects you can use in C#

            if (existingUsers.Length > 0)
            {
                registerFeedback.text = "Username already taken.";
            }
            else
            {
                // Username is free → register
                StartCoroutine(RegisterUser(username, password));
            }
        }
        else
        {
            registerFeedback.text = "Error checking user: " + req.error;
        }
    }

    private IEnumerator RegisterUser(string username, string password)
    {
        string jsonData = JsonUtility.ToJson(new User(username, password)); // Convert user to JSON

        UnityWebRequest req = new UnityWebRequest(baseUrl, "POST");    // POST request to create new user
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData); // Convert JSON string to bytes
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);             // Send JSON as request body
        req.downloadHandler = new DownloadHandlerBuffer();             // Store server response
        req.SetRequestHeader("Content-Type", "application/json");      // Set request type to JSON

        yield return req.SendWebRequest();  // Wait for response

        if (req.result == UnityWebRequest.Result.Success)
        {
            registerFeedback.text = "Registration Successful!";
        }
        else
        {
            registerFeedback.text = "Error: " + req.error;
        }
    }

    public void OnLoginClicked()
    {
        // Starts login process with entered username and password
        StartCoroutine(LoginUser(loginUsername.text.Trim(), loginPassword.text.Trim()));
    }

    // Coroutine to log in an existing user
    private IEnumerator LoginUser(string username, string password)
    {
        string url = $"{baseUrl}?username={username}";  // Query users by username
        UnityWebRequest req = UnityWebRequest.Get(url); // Send GET request

        yield return req.SendWebRequest();  // Wait for response

        if (req.result == UnityWebRequest.Result.Success)
        {
            string jsonResult = req.downloadHandler.text;   // Get JSON response
            User[] users = JsonHelper.FromJson<User>(jsonResult);   // Parse into User objects   

            if (users.Length == 0)
            {
                loginFeedback.text = "User not found.";
            }
            else if (users[0].password == password)
            {
                loginFeedback.text = "Login Successful!";
            }
            else
            {
                loginFeedback.text = "Wrong password.";
            }
        }
        else
        {
            loginFeedback.text = "Error: " + req.error;
        }
    }

    [System.Serializable]
    public class User
    {
        public string username;
        public string password;

        // Constructor to create new User objects
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }

    // Helper class to parse JSON arrays into objects 
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{\"array\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }
}
