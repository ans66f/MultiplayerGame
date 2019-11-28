using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatsMenuScript : MonoBehaviour
{
    //string username = DataHandler.username;
    string username = "nolwennlg";

    public GameObject UsernameText;

    public GameObject NbOfGamesTextNb;
    public GameObject TimePlayedTextNb;
    public GameObject NbOfKillsTextNb;
    public GameObject TotalScoreTextNb;
    public GameObject BulletsShotTextNb;

    public GameObject DbControllerManager;

    // Start is called before the first frame update
    void Start()
    {
        this.UsernameText.GetComponent<Text>().text = username;
        // fetch stats
        StartCoroutine(GetStats());
    }

    IEnumerator GetStats()
    {
        string[] stats = null;
        DbControllerManager.GetComponent<DbController>().LoadStats(username);
        yield return new WaitForSeconds(1);
        stats = DbControllerManager.GetComponent<DbController>().stats;

        this.NbOfGamesTextNb.GetComponent<Text>().text = DbControllerManager.GetComponent<DbController>().GetDataValue(stats[0], "nbOfGames:");
        this.TimePlayedTextNb.GetComponent<Text>().text = DbControllerManager.GetComponent<DbController>().GetDataValue(stats[0], "timePlayed:");
        this.NbOfKillsTextNb.GetComponent<Text>().text = DbControllerManager.GetComponent<DbController>().GetDataValue(stats[0], "nbOfKills:");
        this.TotalScoreTextNb.GetComponent<Text>().text = DbControllerManager.GetComponent<DbController>().GetDataValue(stats[0], "totalScore:");
        this.BulletsShotTextNb.GetComponent<Text>().text = DbControllerManager.GetComponent<DbController>().GetDataValue(stats[0], "bulletsShot:");

    }
}
