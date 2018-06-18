using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Dictionary<PlayerShips, HangarShip> Ships = new Dictionary<PlayerShips, HangarShip>();

    private PlayerShips shipName;
    public PlayerShips ShipName
    {
        get { return shipName; }
        set
        {
            shipName = value;
            Log.OnCatchMessage("Statek " + value + " zostal zaladowany.");
        }
    }
    public PlayerShip Ship;

    public string Username; //INPUT -> LOAD SAVE

    public int Scrap; //SAVE
    public int Metal; //SAVE
    public int Experience; //SAVE
    public int Level; //SAVE
    public int Map; //SAVE
    public List<int> Lasers; //SAVE
    public List<int> Rockets; //SAVE
    public int[] SelectedAmmo; //SAVE
    public List<Item> Items; //SAVE



    IEnumerator Start()
    {
        GameData.LocalPlayer = this;
        yield return new WaitUntil(() => Username != null);
        
        foreach (PlayerShips ship in Enum.GetValues(typeof(PlayerShips)))
        {
            Ships.Add(ship, new HangarShip(GameData.PlayerShipObjects[ship], ship));
        }

        ApplyData(Save.LoadPlayer(Username));
        MainCamera.CameraTarget = gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Save.SavePlayer(GameData.LocalPlayer);
            Debug.LogError("Save");
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            Gui.OpenWindow(Windows.Hangar);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Gui.OpenWindow(Windows.Inventory);
        }
    }



    public void ApplyReward(Reward reward)
    {
        Scrap += reward.Scrap;
        Metal += reward.Metal;
        Experience += reward.Experience;

        string message = "Otrzymano " + reward.Scrap + " zlomu. \n";
        message += "Otrzymano " + reward.Metal + " metalu. \n";
        message += "Otrzymano " + reward.Experience + " doswiadczenia.";

        foreach (ItemReward itemReward in reward.Items)
        {
            float random = UnityEngine.Random.Range(0, 100.0f);
            if (random <= itemReward.Chance)
            {
                //Debug.Log("Nowy item: " + itemReward.Item);
                Item item = GameData.ItemList[itemReward.Item].Clone();

                message += "\nOtrzymano przedmiot: " + item.Name;

                Items.Add(item);
                UseItem(item);
            }
            else
            {
                //Debug.Log("Pech, " + random + " szansa " + itemReward.Chance);
            }
        }

        Log.OnCatchMessage(message);
    }

    public void UseItem(Item item)
    {
        if (item.RequiredLevel > Level)
        {
            Debug.Log("Zly level");
            return;
        }

        CheckItem(item, item.Type);
    }

    private void CheckItem(Item item, ItemTypes itemType)
    {
        int slots = 0;
        switch(itemType)
        {
            case ItemTypes.Laser:
                slots = Ship.LaserSlots;
                break;
            case ItemTypes.Engine:
                slots = Ship.EngineSlots;
                break;
            case ItemTypes.Shield:
                slots = Ship.ShieldSlots;
                break;
        }

        if (Items.Where(o => o.Equipped && o.Type == itemType).Count() < slots)
        {
            item.Equipped = true;
            Debug.LogError("Nowy item zostal wyposazony!");
        }
        else
            Debug.LogError("Brak miejsca na " + item.Type);
    }

    private void ApplyData(SaveData data)
    {
        if (data == null)
            return;

        Scrap = data.Scrap;
        Metal = data.Metal;
        Experience = data.Experience;
        Level = data.Level;
        Map = data.Map;

        transform.position = new Vector3(data.PositionX, data.PositionY);

        Lasers = data.Lasers;
        Rockets = data.Rockets;

        if (data.Ships != null)
        {
            for (int i = 0; i < data.Ships.Count; i++)
            {
                Ships[(PlayerShips)i].UpgradeHitpoints = data.Ships[i].UpgradeHitpoints;
                Ships[(PlayerShips)i].UpgradeSpeed = data.Ships[i].UpgradeSpeed;
                Ships[(PlayerShips)i].Bought = data.Ships[i].Bought;
                Ships[(PlayerShips)i].Used = data.Ships[i].Used;
            }
            ChangeShip(Ships.First(o => o.Value.Used).Key, true);
        }

        SelectedAmmo = data.SelectedAmmo;

        Items = data.PlayerItems;



        MapGenerator.ChangeMap((Maps)Map);
        
        //Save.CheckSave(this);
    }

    public void ChangeShip(PlayerShips shipName, bool start = false)
    {
        if (!Ships[shipName].Bought)
        {
            Debug.Log(shipName + " nie jest kupiony");
            return;
        }

        if (Ship != null)
        {
            foreach (HangarShip hangar in Ships.Values.Where(o => o.Used))
                hangar.Used = false;
        }

        foreach (Transform t in transform.Find("Ship"))
            Destroy(t.gameObject);
        
        Ships[shipName].Used = true;

        GameObject ship = Instantiate(Ships[shipName].Model, transform.Find("Ship"));
        Ship = ship.GetComponent<PlayerShip>();
        ShipName = shipName;
        
        if(!start)
        {
            foreach (Item item in Items.Where(o => o.Equipped))
                item.Equipped = false;

            Gui.windows[Windows.Hangar].Script.Refresh();
        }
    }

    public void BuyShip(PlayerShips shipName, bool scrap)
    {
        if (!Ships[shipName].Bought)
        {
            bool buy = false;
            if(scrap)
            {
                if (Scrap >= Ships[shipName].Ship.CostScrap)
                {
                    Scrap -= Ships[shipName].Ship.CostScrap;
                    buy = true;
                }
                else
                    Debug.LogError("Brak scrapu");
            }
            else
            {
                if (Metal >= Ships[shipName].Ship.CostMetal)
                {
                    Scrap -= Ships[shipName].Ship.CostMetal;
                    buy = true;
                }
                else
                    Debug.LogError("Brak metalu");
            }

            if (buy)
            {
                Ships[shipName].Bought = true;
                Gui.windows[Windows.Hangar].Script.Refresh();
            }
        }
    }
}