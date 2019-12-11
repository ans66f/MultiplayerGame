using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    private const string roomName = "Cool_Apocalypse";
    private TypedLobby lobbyName = new TypedLobby("New_Lobby", LobbyType.Default);
    private RoomInfo[] roomsList;
    public GameObject player;
    public GameObject blockmanager;


    GameObject[] spawnpoints;

    // Use this for initialization
    void Start()
    {
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    private void OnGUI()
    {
        if (!PhotonNetwork.connected)
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        }
        else if (PhotonNetwork.room == null)
        {
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
            {
                PhotonNetwork.CreateRoom(roomName, new RoomOptions()
                { MaxPlayers = 4, isOpen = true, IsVisible = true }, lobbyName);
            }
            if (roomsList != null)
            {
                for (int i = 0; i < roomsList.Length; i++)
                {
                    if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), 
                        "Join " + roomsList[i].Name))
                    {
                        PhotonNetwork.JoinRoom(roomsList[i].Name);
                    }
                }
            }
            if (GUI.Button(new Rect(400, 100, 250, 100), "View user stats"))
            {
                SceneManager.LoadScene("UserStatsScene");
            }
        }
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(lobbyName);
    }

    void OnReceivedRoomListUpdate()
    {
        Debug.Log("Room was created");
        roomsList = PhotonNetwork.GetRoomList();
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    void OnJoinedRoom()
    {
        GameObject randspawnpoint = spawnpoints[0];
        foreach(GameObject spawnpoint in spawnpoints)
        {
            if(Random.Range(0, spawnpoints.Length) == spawnpoints.Length)
            {
                randspawnpoint = spawnpoint;
            }
        }

        Debug.Log("Connected to Room");
        PhotonNetwork.Instantiate(player.name, randspawnpoint.transform.position, Quaternion.identity, 0);
    }
}
    