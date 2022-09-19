using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private const int playerMaxLevel = 10;
    private const int playerAddAbilityPointPerLevel = 10;

    [SerializeField]
    private PlayerArtifactData[] artifactDatas;
    [SerializeField]
    private PlayerSkillData[] skillDatas;
    [SerializeField]
    private int[] playerNeedExp;
    [SerializeField]
    private BattleEnemyTableData[] enemyTableDatas;

    [SerializeField]
    private GameObject ImagePopupPrefab;

    private string prevSceneName;


    private int playerLevel;
    private int playerExp;
    private PlayerAbility playerAbility;
    private int playerGold;

    private float playerMaxHp;
    private float playerNowHp;

    private List<PlayerArtifact> artifacts = new List<PlayerArtifact>();

    private List<PlayerSkillBloc> playerSkillBlocs = new List<PlayerSkillBloc>();
    private List<PlayerSkillBloc> equipedPlayerSkillBlocs = new List<PlayerSkillBloc>();
    private Dictionary<PlayerSkillBloc,Vector2> equipedSkillBlocPositions = new Dictionary<PlayerSkillBloc, Vector2>();


    private List<int> battleRootIndex = new List<int>();

    public PlayerArtifactData[] ArtifactDatas
    {
        get
        {
            return artifactDatas;
        }
    }
    public PlayerSkillData[] SkillDatas
    {
        get
        {
            return skillDatas;
        }
    }
    public BattleEnemyTableData[] EnemyTableDatas
    {
        get
        {
            return enemyTableDatas;
        }
    }




    public PlayerAbility PlayerAbility
    {
        get
        {
            return playerAbility;
        }
    }
    public int PlayerLevel
    {
        get
        {
            return playerLevel;
        }
    }
    public int PlayerGold
    {
        get
        {
            return playerGold;
        }
    }
    public float PlayerMaxHp
    {
        get
        {
            return playerMaxHp;
        }
    }
    public float PlayerNowHp
    {
        get
        {
            return playerNowHp;
        }
    }


    public List<PlayerArtifact> Artifacts
    {
        get
        {
            return artifacts;
        }
    }
    public List<PlayerSkillBloc> PlayerSkillBlocs
    {
        get
        {
            return playerSkillBlocs;
        }
    }
    public List<PlayerSkillBloc> EquipedPlayerSkillBlocs
    {
        get
        {
            return equipedPlayerSkillBlocs;
        }
    }

    public int NextBattleTableIndex
    {
        get
        {
            int index = battleRootIndex[0];

            battleRootIndex.RemoveAt(0);

            return index;
        }
    }

    public int BattleRootLength
    {
        get
        {
            return battleRootIndex.Count;
        }
    }

    private void Awake()
    {
        Init();

        playerLevel = 1;
        playerExp = 0;
        playerAbility = new PlayerAbility(0, 0, 0, playerLevel * playerAddAbilityPointPerLevel);

        playerMaxHp = 50f;
        playerNowHp = 50f;

        LoadScene("StartScene");
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null)
            {
                Debug.LogError("Error 49841 - Not Ready GameManager");
            }
            if (go.GetComponent<GameManager>() == null)
            {
                go.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(go);

            instance = go.GetComponent<GameManager>();
        }
    }

    public void OpenImagePopup(Sprite sprite, string title, string content)
    {
        GameObject popup = Instantiate(ImagePopupPrefab);

        popup.GetComponent<ImagePopup>().OpenImagePopup(sprite, title, content);
    }

    public PlayerArtifactData GetPlayerArtifactData(int index)
    {
        foreach(PlayerArtifactData artifactData in artifactDatas)
        {
            if(artifactData.Index == index)
            {
                return artifactData;
            }
        }

        return null;
    }

    public PlayerSkillData GetPlayerSkillData(int index)
    {
        foreach (PlayerSkillData skillData in skillDatas)
        {
            if (skillData.Index == index)
            {
                return skillData;
            }
        }

        return null;
    }

    public BattleEnemyTableData GetBattleEnemyTableData(int index)
    {
        foreach (BattleEnemyTableData enemyTableData in enemyTableDatas)
        {
            if (enemyTableData.Index == index)
            {
                return enemyTableData;
            }
        }

        return null;
    }

    public void SetBattleRoot(int index)
    {
        battleRootIndex.Add(index);
    }

    public void GainPlayerExp(int exp)
    {
        playerExp += exp;

        CheckLevelup();
    }

    public void GainPlayerGold(int gold)
    {
        playerGold += gold;
    }

    public void GetArtifact(PlayerArtifact artifact)
    {
        if (artifacts.Exists(x => x.Index == artifact.Index))
        {
            artifacts.Find(x => x.Index == artifact.Index).AddStack();
        }
        else
        {
            artifacts.Add(artifact);
        }
    }

    public void GetSkillBloc(PlayerSkillBloc skillBloc)
    {
        playerSkillBlocs.Add(skillBloc);
    }

    public bool UsePlayerGold(int gold)
    {
        if(playerGold >= gold)
        {
            playerGold -= gold;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPlayerHp(float value)
    {
        if(value >= playerMaxHp)
        {
            playerNowHp = playerMaxHp;
        }
        else
        {
            playerNowHp = value;
        }
    }

    public void RecoveryPlayerHp(float value)
    {
        if(playerNowHp + value >= PlayerMaxHp)
        {
            playerNowHp = playerMaxHp;
        }
        else
        {
            playerNowHp += value;
        }
    }

    public void EquipSkillBloc(PlayerSkillBloc skillBloc, Vector2 position)
    {
        if (playerSkillBlocs.Contains(skillBloc))
        {
            equipedPlayerSkillBlocs.Add(skillBloc);

            equipedSkillBlocPositions.Add(skillBloc, position);
        }
        else
        {
            Debug.LogError("Error 95097 - Not Contain Bloc");
        }
    }
    public void UnEquipSkillBloc(PlayerSkillBloc skillBloc)
    {
        if (equipedPlayerSkillBlocs.Contains(skillBloc))
        {
            equipedPlayerSkillBlocs.Remove(skillBloc);

            equipedSkillBlocPositions.Remove(skillBloc);
        }
        else
        {
            Debug.LogError("Error 95098 - Not Equip Bloc");
        }
    }

    public Vector2 CheckEquipBlocPosition(PlayerSkillBloc skillBloc)
    {
        return equipedSkillBlocPositions[skillBloc];
    }

    public void LoadScene(string sceneName)
    {
        prevSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(sceneName);
    }

    public void LoadPrevScene()
    {
        Debug.Log(prevSceneName);
        SceneManager.LoadScene(prevSceneName);
    }

    public void NextSceneLoad()
    {
        Debug.Log("next Scene");
    }

    private void CheckLevelup()
    {
        if(playerLevel >= playerMaxLevel)
        {
            Debug.Log("Player Max Level");

            return;
        }

        int needExp = playerNeedExp[playerLevel - 1];

        if(playerExp >= needExp)
        {
            playerLevel++;

            playerExp -= needExp;

            int nowAttackDamage = playerAbility.AttackDamage;
            int nowAttackSpeed = playerAbility.AttackSpeed;
            int nowDefense = playerAbility.Defense;
            int nowAbilityPoint = playerAbility.AbilityPoint;

            playerAbility = new PlayerAbility(nowAttackDamage, nowDefense, nowAttackSpeed, nowAbilityPoint + playerAddAbilityPointPerLevel);

            Debug.Log("Player Level Up - now Level : " + playerLevel);
        }
        else
        {
            return;
        }

        CheckLevelup();
    }
}
