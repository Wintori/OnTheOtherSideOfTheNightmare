using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QFSW.QC;
using Minimap;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private MinimapIcon playerMinimapIcon;

    private void Start()
    {
        Minimap.MinimapClass.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Minimap.MinimapClass.ZoomIn();
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Minimap.MinimapClass.ZoomOut();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            MinimapClass.ShowWindow();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            MinimapClass.HideWindow();
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            playerMinimapIcon.Show();
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            playerMinimapIcon.Hide();
        }
    }

    private void Awake()
    {
        SoundManageer.Initialize();

        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            Destroy(gameAsset);
            Destroy(pause);
            Destroy(console);
            Destroy(inputManager);
            Destroy(dialogueWindow);

            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Resources

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprite;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References

    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;
    public GameObject gameAsset;
    public GameObject pause;
    public GameObject console;
    public GameObject inputManager;
    public GameObject dialogueWindow;

    // Logic
    public int pesos;
    public int experience;

    // Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public bool TryUpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLeve)
            return false;
        if (pesos >= weaponPrices[weapon.weaponLeve])
        {
            pesos -= weaponPrices[weapon.weaponLeve];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;

    }


    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoing;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

   

    [Command]
    public int GetCurrenetLevel()
    {
        int r = 0;
        int add = 0;
        while (experience > add)
        {
            if (r == xpTable.Count)
                return r;
            else
            {
                add += xpTable[r];
                r++;
            }
        }
        return r;
    }
    public int GetExpLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrenetLevel();
        experience += xp;
        if (currLevel < GetCurrenetLevel())
            OnLevelUp();
    }
    public void OnLevelUp()
    {
        Debug.Log("Level up!");
        player.OnLevelUp();
        OnHitpointChange();
    }

    // On Scene Loaded

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Death Menu and Respawn

    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("main menu");
        player.Respawn();
    }


    // Save state
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLeve.ToString();


        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] date = PlayerPrefs.GetString("SaveState").Split('|');
        //Change player skin

        pesos = int.Parse(date[1]);

        // Experience
        experience = int.Parse(date[2]);
        if (GetCurrenetLevel() != 1)
            player.SetLevel(GetCurrenetLevel());
        // Change the weapon Level
        weapon.SetWeaponLevel(int.Parse(date[3]));

        
    }

    [Command]
    private static void AddGold(int value)
    {
        GameManager.instance.pesos += value;
    }

    [Command]
    private static void AddXP(int value)
    {
        GameManager.instance.experience += value;
    }
}
