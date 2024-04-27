using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class ApiManager : MonoBehaviour,IGameService
{
    private const string ApiUrl = "http://127.0.0.1:8000/"; // Replace with your API URL

    public delegate void DataReceivedCallback(string data); // Define a callback delegate
    public  enum NetworkMethod
    {
        Get,
        Post
    } 
    public void CallApi(string url ,NetworkMethod method, DataReceivedCallback callback= null,string data= null)
    {
        if(method == NetworkMethod.Get)
        {
            StartCoroutine(FetchData(url, callback));
        }
        else if (method == NetworkMethod.Post)
        {
            StartCoroutine(PostData(url, data,callback));
        }
    }

    private IEnumerator FetchData(string url, DataReceivedCallback callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonData = request.downloadHandler.text;
                    // Parse the JSON response (e.g., using Newtonsoft.Json or SimpleJSON)

                    // Example: Display data in a text field
                    // myTextField.text = parsedData.someProperty;
                    Debug.Log(jsonData);
                    callback?.Invoke(jsonData);
                }
            else
            {
                callback?.Invoke(null);
                Debug.Log($"Error fetching data: {request.error}");
            }
        }
    }
    IEnumerator PostData(string url, string data,DataReceivedCallback callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, data))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data)) as UploadHandler;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.responseCode);
            }
        }
    }
    public void OnDestroy()
    {
        
    }
}
/// <summary>
/// this should be converted to config later On
/// </summary>
public static class Apis
{
    public static readonly string fetchLeaderBoard = "http://127.0.0.1:8000/api/leaderboard/get-leaderboard";
    public static readonly string addLeaderboardData = "http://127.0.0.1:8000/api/leaderboard/register" ;
}