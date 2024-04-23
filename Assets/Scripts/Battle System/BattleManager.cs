using JetBrains.Annotations;
//using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private bool isBattleActive;

    [SerializeField] GameObject battleScene;
    [SerializeField] Transform[] playerPositions, enemyPositions;
    [SerializeField] BattleCharacters[] playerPrefabs, enemyPrefabs;

    [SerializeField] List<BattleCharacters> activeCharacters = new List<BattleCharacters>();

    [SerializeField] int currentTurn;
    [SerializeField] bool waitingForTurn;
    [SerializeField] GameObject UIHolder;

    [SerializeField] BattleMoves[] battleMovesList;
    [SerializeField] CombatText damageText;

    [SerializeField] GameObject[] characterBattleStats;
    [SerializeField] TextMeshProUGUI[] characterNameText, characterHPText, characterManaText;
    [SerializeField] Slider[] characterHPSlider, characterManaSlider;
    [SerializeField] GameObject[] onTurnIndicator;
    public GameObject spellPanel;
    [SerializeField] SpellButton[] spellButtons;


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
            StartBattle(new string[] { "Undead Knight" });
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }

        CheckUIHolder();
    }

    

    public void StartBattle(string[] enemiesToSpawn)
    {
        if (!isBattleActive)
        {           
            BattleSceneSetup();
            AddBattleCharacters();
            AddEnemies(enemiesToSpawn);
            UpdateCharacterStats();
            UpdateBattle();

            waitingForTurn = true;
            currentTurn = 0;
        }      
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
        isBattleActive = true;
        GameManager.instance.battleActive = true;
        transform.position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y,
            transform.position.z);
        battleScene.SetActive(true);
    }

    private void NextTurn()
    {

        if (activeCharacters[currentTurn].IsCharacterAPlayer())
        {
            onTurnIndicator[currentTurn].SetActive(false);
        }

        currentTurn++; 
        
        if (currentTurn >= activeCharacters.Count)
        {
            currentTurn = 0;
        }



        waitingForTurn = true;
        UpdateBattle();
    }

    private void UpdateBattle()
    {
        bool allEnemiesDead = true;
        bool allPlayersDead = true;


        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if (activeCharacters[i].currentHP < 0)
            {
                activeCharacters[i].currentHP = 0;
            }   
            
            if (activeCharacters[i].currentHP == 0)
            {
                //Kill Character
            }
            else
            {
                if (activeCharacters[i].IsCharacterAPlayer())
                {
                    allPlayersDead = false;                   
                }
                else
                {
                    allEnemiesDead = false;
                }
            }

        }

        if (allPlayersDead || allEnemiesDead)
        {
            if (allPlayersDead)
            {
                Debug.Log("YOU LOST");
            }
            else
            {
                Debug.Log("YOU WON");
            }

            battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            isBattleActive = false;
        }
        else
        {
            while (activeCharacters[currentTurn].currentHP == 0)
            {
                currentTurn++;
                if (currentTurn >= activeCharacters.Count)
                {
                    currentTurn = 0;
                }
            }
        }
        if (activeCharacters[currentTurn].IsCharacterAPlayer())
        {
            onTurnIndicator[currentTurn].SetActive(true);
        }
    }

    public IEnumerator EnemyMove()
    {
        waitingForTurn = false;
        yield return new WaitForSeconds(1);
        EnemyAttack();

        yield return new WaitForSeconds(1);
        NextTurn();
    }

    private void EnemyAttack()
    {
        List<int> players = new List<int>();
        

        for (int i = 0; i < activeCharacters.Count; i++) 
        {
            if (activeCharacters[i].IsCharacterAPlayer() && activeCharacters[i].currentHP > 0)
            {
                players.Add(i);
            }

        }

        int selectedPlayerToAttack = players[Random.Range(0, players.Count)];

        int selectedAttack = Random.Range(0, activeCharacters[currentTurn].AttacksAvailable().Length);
        int abilityPower = 0;

        for (int i = 0; i < battleMovesList.Length; i++)
        {
            if (battleMovesList[i].abilityName == activeCharacters[currentTurn].AttacksAvailable()[selectedAttack])
            {
                Instantiate(
                    battleMovesList[i].effect,
                    activeCharacters[selectedPlayerToAttack].transform.position,
                    activeCharacters[selectedPlayerToAttack].transform.rotation
                    );

                abilityPower = battleMovesList[i].abilityPower;
            }
        }

        DealDamage(selectedPlayerToAttack, abilityPower);

        UpdateCharacterStats();

    }

    private void CheckUIHolder()
    {
        if (isBattleActive)
        {
            if (waitingForTurn)
            {
                if (activeCharacters[currentTurn].IsCharacterAPlayer())
                {
                    UIHolder.SetActive(true);
                }
                else
                {
                    UIHolder.SetActive(false);
                    StartCoroutine(EnemyMove());
                }
            }
        }
    }

    private void DealDamage(int characterToAttack, int abilityPower)
    {
        
        float attackPower = activeCharacters[currentTurn].strength + activeCharacters[currentTurn].attackPower + activeCharacters[currentTurn].weaponPower;
        float defenceAmount = activeCharacters[characterToAttack].armor + activeCharacters[characterToAttack].armorDef;

        float damageAmount = (attackPower / defenceAmount) * abilityPower * Random.Range(0.8f, 1.2f);
        int damageDealt = (int)damageAmount;

        damageDealt = CriticalChance(damageDealt);
        Debug.Log(activeCharacters[currentTurn].name + " dealt " + damageDealt + " to " + activeCharacters[characterToAttack]);



        activeCharacters[characterToAttack].TakeDamage(damageDealt);

        CombatText combatText = Instantiate(
                        damageText,
                        activeCharacters[characterToAttack].transform.position,
                        activeCharacters[characterToAttack].transform.rotation
                        );

        combatText.DamageTextAmount(damageDealt);
    }

    private int CriticalChance(int damageDealt)
    {
        if (Random.value <= 0.1f)
        {
            Debug.Log("Critical hit!");
            return (damageDealt * 2);
        }
        else 
        {
            return damageDealt;
        }
    }

    public void UpdateCharacterStats()
    {
        for (int i = 0; i < characterNameText.Length; i++)
        {
            if (activeCharacters.Count > i)
            {
                if (activeCharacters[i].IsCharacterAPlayer())
                {
                    BattleCharacters playerData = activeCharacters[i];
                    characterNameText[i].text = playerData.characterName;

                    characterHPSlider[i].maxValue = playerData.maxHP;
                    characterHPSlider[i].value = playerData.currentHP;
                    characterHPText[i].text = (playerData.currentHP.ToString() + "/" + playerData.maxHP.ToString());

                    characterManaSlider[i].maxValue = playerData.maxMana;
                    characterManaSlider[i].value = playerData.currentMana;
                    characterManaText[i].text = (playerData.currentMana.ToString() + "/" + playerData.maxMana.ToString());
                }

                else
                {
                    characterBattleStats[i].SetActive(false);
                }

            }

            else
            {
                characterBattleStats[i].SetActive(false);
            }
        }
    }

    public void PlayerAttack(string abilityName)
    {
        int enemyTarget = 3;
        int abilityPower = 0;


        for (int i = 0; i < battleMovesList.Length; i++)
        {
            if (battleMovesList[i].abilityName == abilityName)
            {
                Instantiate(
                    battleMovesList[i].effect,
                    activeCharacters[enemyTarget].transform.position,
                    activeCharacters[enemyTarget].transform.rotation                                      
                    );

                abilityPower = battleMovesList[i].abilityPower;
            }
        }

        DealDamage(enemyTarget, abilityPower);
        UpdateCharacterStats();
        NextTurn();

    }

    public void OpenSpellPanel()
    {
        spellPanel.SetActive(true);

        for (int i = 0; i < spellButtons.Length; i++)
        {
            if (activeCharacters[currentTurn].AttacksAvailable().Length > i)
            {
                spellButtons[i].gameObject.SetActive(true);
                spellButtons[i].spellName = ActiveCharacters().AttacksAvailable()[i];  
                
                for (int j = 0; j < battleMovesList.Length; j++)
                {
                    if (battleMovesList[j].abilityName == spellButtons[i].spellName)
                    {
                        spellButtons[i].spellCost = battleMovesList[j].manaCost;
                        spellButtons[i].spellCostText.text = spellButtons[i].spellCost.ToString();
                        spellButtons[i].spellIcon.sprite = battleMovesList[j].spellIcon;
                    }
                }
            }
            else
            {
                spellButtons[i].gameObject.SetActive(false);
            }
            
        }
    }

    public BattleCharacters ActiveCharacters()
    {
        return activeCharacters[currentTurn];
    }


}
