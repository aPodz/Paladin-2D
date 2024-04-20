using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private bool isBattleActive;

    [SerializeField] GameObject battleScene;
    [SerializeField] Transform[] playerPositions, enemyPositions;
    [SerializeField] BattleCharacters[] playerPrefabs, enemyPrefabs;

    [SerializeField] List<BattleCharacters> activeCharacters = new List<BattleCharacters>();


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.B))
       {
            StartBattle(new string[]{"Undead Knight"});
       }
    }

    public void StartBattle(string[] enemiesToSpawn)
    {
        BattleSceneSetup();
        AddBattleCharacters();
        AddEnemies(enemiesToSpawn);

    }

    private void AddEnemies(string[] enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            if (enemiesToSpawn[i] != "")
            {
                for (int j = 0; j < enemyPrefabs.Length; j++)
                {
                    if (enemyPrefabs[j].characterName == enemiesToSpawn[j])
                    {
                        BattleCharacters newEnemy = Instantiate(
                            enemyPrefabs[j],
                            enemyPositions[i].position,
                            enemyPositions[i].rotation,
                            enemyPositions[i]
                            );

                        activeCharacters.Add(newEnemy);
                    }
                }
            }
        }
    }

    private void AddBattleCharacters()
    {
        for (int i = 0; i < GameManager.instance.GetPlayerStats().Length; i++)
        {
            if (GameManager.instance.GetPlayerStats()[i].gameObject.activeInHierarchy)
            {
                for (int j = 0; j < playerPrefabs.Length; j++)
                {
                    if (playerPrefabs[j].characterName == GameManager.instance.GetPlayerStats()[i].playerName)
                    {
                        BattleCharacters newCharacter = Instantiate(
                            playerPrefabs[j],
                            playerPositions[i].position,
                            playerPositions[i].rotation,
                            playerPositions[i]
                            );

                        activeCharacters.Add(newCharacter);
                        ImportStats(i);
                    }
                }
            }
        }
    }

    private void ImportStats(int i)
    {
        PlayerStats player = GameManager.instance.GetPlayerStats()[i];

        activeCharacters[i].currentHP = player.currentHP;
        activeCharacters[i].maxHP = player.maxHP;

        activeCharacters[i].maxMana = player.maxMana;
        activeCharacters[i].currentMana = player.currentMana;

        activeCharacters[i].armor = player.armor;
        activeCharacters[i].strength = player.strength;
        activeCharacters[i].attackPower = player.attackPower;
        activeCharacters[i].weaponPower = player.weaponAP;
        activeCharacters[i].armorDef = player.armorDef;
    }

    private void BattleSceneSetup()
    {
        if (!isBattleActive)
        {
            isBattleActive = true;
            GameManager.instance.battleActive = true;
            transform.position = new Vector3(
                Camera.main.transform.position.x,
                Camera.main.transform.position.y,
                transform.position.z);
            battleScene.SetActive(true);
        }
    }
}
