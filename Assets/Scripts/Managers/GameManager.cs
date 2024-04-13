using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] PlayerStats[] playerStats;

    public bool gameMenuOpen, dialogBoxOpen, shopOpen;

    public int currentCoins;

    // Start is called before the first frame update
    void Start()
    {


        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        playerStats = FindObjectsOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SavePositionData();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            LoadPositionData();
        }
        if (gameMenuOpen || dialogBoxOpen || shopOpen)
        {
            Player.instance.movementDisabled = true;
        }
        else
        {
            Player.instance.movementDisabled = false;
        }
    }

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
    }

    public void SavePositionData()
    {
        PlayerPrefs.SetFloat("Player_Pos_X", Player.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Pos_Y", Player.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Pos_Z", Player.instance.transform.position.z);
    }

    public void LoadPositionData()
    {
        Player.instance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("Player_Pos_X"),
            PlayerPrefs.GetFloat("Player_Pos_Y"),
            PlayerPrefs.GetFloat("Player_Pos_Z")
            );    
    }
}
