using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class DbController : MonoBehaviour
{string createUserURL = "http://kunet.kingston.ac.uk/k1931511/unitySQL/createUser.php";
    string createStatsURL = "http://kunet.kingston.ac.uk/k1931511/unitySQL/createStats.php";
    string loadScoresURL = "http://kunet.kingston.ac.uk/k1931511/unitySQL/scoreData.php";
    string saveScoresURL = "http://kunet.kingston.ac.uk/k1931511/unitySQL/saveGameScore.php";
    string checkUserURL = "http://kunet.kingston.ac.uk/k1931511/unitySQL/checkUserConnection.php";
    string loadStatsURL = "http://kunet.kingston.ac.uk/k1931511/unitySQL/statsData.php";
    string updateStatsURL = "http://kunet.kingston.ac.uk/k1931511/unitySQL/updateStats.php";

    public string[] items;
    public string[] stats;

    public string log;
    bool done = false;

    IEnumerator Waiter() // just for testing
    {
        Debug.Log("CREATE USER");
        CreateUser("nolwennlg", GetSha1("okcomputer"));
        yield return new WaitForSeconds(2);
        Debug.Log("CHECK USER");
        CheckUser("nolwennlg", GetSha1("okcomputer"));
        yield return new WaitForSeconds(2);
        Debug.Log("SAVE SCORE");
        SaveScores("nolwennlg", 20);
        yield return new WaitForSeconds(2);
        Debug.Log("LOAD SCORE");
        LoadScores();
        yield return new WaitForSeconds(2);
        Debug.Log("UPDATE STATS");
        UpdateStats("nolwennlg", 14, 35, 65, 2, 23);
        yield return new WaitForSeconds(2);
        Debug.Log("LOAD STATS");
        LoadStats("nolwennlg");
    }

    void Start()
    {
        // StartCoroutine(Waiter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateUser(string user, string pass)
    {
        StartCoroutine(CCreateUser(user, pass));
    }


    public void CheckUser(string user, string pass)
    {
        StartCoroutine(CCheckUser(user, pass));
    }


    public void SaveScores(string user, int score)
    {
        StartCoroutine(CSaveScores(user, score));
    }

    public void LoadScores()
    {
        StartCoroutine(CLoadScores());
    }

    public void LoadStats(string user)
    {
        StartCoroutine(CLoadStats(user));
    }

    public void UpdateStats(string user, int nbOfGames, int timePlayed, int nbOfKills, int totalScore, int bulletsShot)
    {
        StartCoroutine(CUpdateStats(user, nbOfGames, timePlayed, nbOfKills, totalScore, bulletsShot));
    }


    IEnumerator CCreateUser(string user, string pass)
    {
        done = false;
        WWWForm form = new WWWForm();

        form.AddField("username", user);
        form.AddField("password", pass);

        WWW webRequest = new WWW(createUserURL, form);
        yield return webRequest;
        if (!string.IsNullOrEmpty(webRequest.error))
        {
            print("Error: " + webRequest.error);
        }
        else
        {
            log = webRequest.text.ToString();
            Debug.Log(webRequest.text.ToString());
        }

        done = true;

        //create stats
        if (!log.Equals("user_already_taken"))
        {
            WWW webRequest2 = new WWW(createStatsURL, form);
            yield return webRequest;
            if (!string.IsNullOrEmpty(webRequest.error))
            {
                print("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.text.ToString());
            }
        }
    }

    IEnumerator CCheckUser(string user, string pass)
    {
        WWWForm form = new WWWForm();

        form.AddField("username", user);
        form.AddField("password", pass);

        WWW webRequest = new WWW(checkUserURL, form);
        yield return webRequest;

        if (!string.IsNullOrEmpty(webRequest.error))
        {
            print("Error: " + webRequest.error);
        }
        else
        {
            log = webRequest.text.ToString();
            Debug.Log(webRequest.text.ToString());
            // ok for connection
        }
    }

    IEnumerator CSaveScores(string user, int score)
    {
        WWWForm form = new WWWForm();

        form.AddField("username", user);
        form.AddField("score", score);

        WWW webRequest = new WWW(saveScoresURL, form);
        yield return webRequest;

        if (!string.IsNullOrEmpty(webRequest.error))
        {
            print("Error: " + webRequest.error);
        }
        else
        {
            Debug.Log(webRequest.text.ToString());
        }
    }

    IEnumerator CLoadScores()
    {
        WWW webRequest = new WWW(loadScoresURL);
        yield return webRequest;
        string itemsDataString = webRequest.text;
        print(itemsDataString);
        items = itemsDataString.Split(';');
        print(GetDataValue(items[0], "score:"));
    }

    IEnumerator CLoadStats(string user)
    {
        WWWForm form = new WWWForm();

        form.AddField("username", user);

        WWW webRequest = new WWW(loadStatsURL, form);
        yield return webRequest;
        string itemsDataString = webRequest.text;
        //print(itemsDataString);
        items = itemsDataString.Split(';');
        this.stats = items;
        //print(GetDataValue(items[0], "nbOfKills:"));
    }

    IEnumerator CUpdateStats(string user, int nbOfGames, int timePlayed, int nbOfKills, int totalScore, int bulletsShot)
    {
        WWWForm form = new WWWForm();

        form.AddField("username", user);
        form.AddField("nbOfGames", 1); // this will be called for every game
        form.AddField("timePlayed", timePlayed);
        form.AddField("nbOfKills", nbOfKills);
        form.AddField("totalScore", totalScore);
        form.AddField("bulletsShot", bulletsShot);

        WWW webRequest = new WWW(updateStatsURL, form);
        yield return webRequest;

        if (!string.IsNullOrEmpty(webRequest.error))
        {
            print("Error: " + webRequest.error);
        }
        else
        {
            Debug.Log(webRequest.text.ToString());
        }
    }

    public string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

    public string GetSha1(string value)
    {
        var data = Encoding.ASCII.GetBytes(value);
        var hashData = new SHA1Managed().ComputeHash(data);
        var hash = string.Empty;
        foreach (var b in hashData)
        {
            hash += b.ToString("X2");
        }
        return hash;
    }
}
