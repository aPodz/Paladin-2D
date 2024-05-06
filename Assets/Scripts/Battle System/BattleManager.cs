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
using UnityEngine.SceneManagement;
using System.Linq;


public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private bool isBattleActive;

    public GameObject battleScene;
    [SerializeField] Transform[] playerPositions, enemyPositions;
    [SerializeField] BattleCharacters[] playerPrefabs, enemyPrefabs;

    public List<BattleCharacters> activeCharacters = new List<BattleCharacters>();

    public int currentTurn;
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
    public Notification battleNotification;

    [SerializeField] float chanceToRun;

    public GameObject battleItemsMenu;
    [SerializeField] ItemsManager selectedItem;
    [SerializeField] GameObject itemBox;
    [SerializeField] Transform itemBoxParent;
    [SerializeField] TextMeshProUGUI itemName, itemDesc;

    [SerializeField] GameObject characterChoicePanel;
    [SerializeField] TextMeshProUGUI[] characterName;

    [SerializeField] string gameOverScene;
    private bool runningAway;
    public int XPRewardAmount;
    public ItemsManager[] itemsRewarded;

    [SerializeField] GameObject battleEntry;

    [SerializeField] GameObject runButton;
    private bool canRun;

    // Start is called before the first frame update
    void Start()
    {
            instance = this;      
    }

    // Update is called once per frame
    void Update()
    {
        CheckUIHolder();
    }

    

    public void StartBattle(string[] enemiesToSpawn, bool canRunAway)
    {
        if (!isBattleActive)
        {

            canRun = canRunAway;
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
                    if (enemyPrefabs[j].characterName == enemiesToSpawn[i])
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
                if (activeCharacters[i].IsCharacterAPlayer() && !activeCharacters[i].isDead)
                {
                    activeCharacters[i].KillFriendlyCharacter();
                }

                if (!activeCharacters[i].IsCharacterAPlayer() && !activeCharacters[i].isDead)
                {
                    activeCharacters[i].KillEnemy();
                }
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
                StartCoroutine(GameOverCoroutine());
            }
            else
            {
                StartCoroutine(EndBattleCoroutine());
            }
            
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
        if (canRun)
        {
            runButton.SetActive(true);
        }
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
        int enemyTarget = GameManager.instance.GetPlayerStats().Length;
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

    public void RunAway()
    {
        if (canRun)
        {
            if (Random.value > chanceToRun)
            {
                runningAway = true;
                StartCoroutine(EndBattleCoroutine());
            }
            else
            {
                NextTurn();
                battleNotification.Text("ESCAPE FAILED");
                battleNotification.Activate();
                runButton.SetActive(false);
            }
        }
        else
        {
            NextTurn();
            battleNotification.Text("THERE IS NO ESCAPE");
            battleNotification.Activate();
            runButton.SetActive(false);
        }


    }

    public void UpdateInventory()
    {
        battleItemsMenu.SetActive(true);

        foreach (Transform itemSlot in itemBoxParent)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (ItemsManager item in Inventory.instance.GetItemList())
        {
            if (item.isStackable)
            {
                RectTransform itemSlot = Instantiate(itemBox, itemBoxParent).GetComponent<RectTransform>();

                Image itemImage = itemSlot.Find("ItemImage").GetComponent<Image>();
                itemImage.sprite = item.itemImage;

                TextMeshProUGUI itemAmountText = itemSlot.Find("Amount").GetComponent<TextMeshProUGUI>();

                if (item.amount > 1)
                {
                    itemAmountText.text = item.amount.ToString();
                }
                else
                {
                    itemAmountText.text = "";
                }

                itemSlot.GetComponent<ItemButton>().itemOnButton = item;
            }
        }
    }

    public void SelectedBattleItem(ItemsManager itemToUse)
    {
        selectedItem = itemToUse;
        itemName.text = itemToUse.name;
        itemDesc.text = itemToUse.itemDesc;
    }

    public void OpenCharacterChoicePanel()
    {
        if (selectedItem)
        {
            characterChoicePanel.SetActive(true);
            for (int i = 0; i < activeCharacters.Count; i++)
            {
                if (activeCharacters[i].IsCharacterAPlayer())
                {
                    PlayerStats activePlayer = GameManager.instance.GetPlayerStats()[i];

                    characterName[i].text = activePlayer.name;

                    bool playerActiveInHierarchy = activePlayer.gameObject.activeInHierarchy;
                    characterName[i].transform.parent.gameObject.SetActive(playerActiveInHierarchy);
                }
            }
        }
        else
        {
            battleNotification.Text("No item selected");
            battleNotification.Activate();
        }
    }

    public void UseItemButton(int characterToUse)
    {
        activeCharacters[characterToUse].UseItemInBattle(selectedItem);
        Inventory.instance.RemoveItem(selectedItem);

        UpdateCharacterStats();
        CloseCharacterChoicePanel();
        UpdateInventory();
        selectedItem = null;
    }

    public void CloseCharacterChoicePanel()
    {
        characterChoicePanel.SetActive(false);
        battleItemsMenu.SetActive(false);
    }

    public IEnumerator EndBattleCoroutine()
    {
        if (!runningAway)
        {
            yield return new WaitForSeconds(2);
            Destroy(battleEntry);
            battleNotification.Text("VICTORY");
            battleNotification.Activate();
        }
        else
        {
            battleNotification.Text("YOU ESCAPED");
            battleNotification.Activate();
        }

        yield return new WaitForSeconds(2);

        DestroyBattleCharacters();
        isBattleActive = false;
        spellPanel.SetActive(false);
        UIHolder.SetActive(false);
        runButton.SetActive(true);
        if (runningAway)
        {
            battleScene.SetActive(false);
            activeCharacters.Clear();
            currentTurn = 0;
            GameManager.instance.battleActive = false;
            runningAway = false;
        }
        else
        {
            BattleRewards.instance.OpenRewardPanel(XPRewardAmount, itemsRewarded);
        }

    }

    private void DestroyBattleCharacters()
    {
        foreach (BattleCharacters charactersInBattle in activeCharacters)
        {
            if (charactersInBattle.IsCharacterAPlayer())
            {
                foreach (PlayerStats playerInBattle in GameManager.instance.GetPlayerStats())
                {
                    if (charactersInBattle.characterName == playerInBattle.playerName)
                    {
                        playerInBattle.currentHP = charactersInBattle.currentHP;
                        playerInBattle.currentMana = charactersInBattle.currentMana;
                    }
                }
            }

            Destroy(charactersInBattle.gameObject);
        }
    }

    public IEnumerator GameOverCoroutine()
    {
        battleNotification.Text("Defeat");
        battleNotification.Activate();
        MenuManager.instance.FadeImage();
        
        yield return new WaitForSeconds(3);

        DestroyBattleCharacters();
        isBattleActive = false;
        battleScene.SetActive(false);
        activeCharacters.Clear();
        currentTurn = 0;

        yield return new WaitForSeconds(2);
        MenuManager.instance.FadeOut();
        SceneManager.LoadScene("GameOver");
        
        
        

        
        
        
        
    }
}
