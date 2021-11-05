using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public Player player;
    public Inventory inventory;
    public EquipmentManager equipManager;
    public WorldManager worldManager;
    public DayNightCycle cycle;
    public ForestGenerator forestGenerator;

    public Transform forestParent;
    public Transform enemyParent;
    
    public GameObject itemParent;
    public GameObject placeableObjectParent;


    #region singelton

    public static GameManager instance;
 
    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("Warning: More than one instance of GameManager found!");
            return;
        }
        instance = this;

        player = GameObject.Find("Player").GetComponent<Player>();
        inventory = Inventory.instance;
        equipManager = EquipmentManager.instance;
        worldManager = WorldManager.instance;
        cycle = GetComponent<DayNightCycle>();
        forestParent = GameObject.Find("Forest").GetComponent<Transform>();
        enemyParent = GameObject.Find("Enemies").GetComponent<Transform>();
        forestGenerator = GameObject.Find("Forest").GetComponent<ForestGenerator>();

    }
    #endregion

    private void OnApplicationQuit() {
        //SaveGame();
    }

    private void OnLevelWasLoaded(int level) {
        if(level == 1) {
            if (SceneConnector.GetNewGame()) {
                SaveSystem.DeleteAllData();
                SetNewGameParameters();
                SceneConnector.SetNewGame(false);
            }
            else if (SceneConnector.GetLoadGame()) {
                LoadGame();
                SceneConnector.SetLoadGame(false);
            }
        }
    }

    public void SaveGame() {
        SaveSystem.SaveTimer(cycle);
        SaveSystem.SavePlayer(player);
        SaveSystem.SaveInventory(inventory);
        SaveSystem.SaveEquipment(equipManager);
        SaveSystem.SaveWorld(worldManager);
        SaveSystem.SaveChest(worldManager);
        SaveSystem.SaveCampfire(worldManager);
        SaveSystem.SaveFurnace(worldManager);
        SaveSystem.SaveCrockpot(worldManager);
        SaveSystem.SaveForest(forestParent);
        SaveSystem.SaveEnemies(enemyParent);
    }

    public void LoadGame() {
        LoadTime();
        LoadPlayer();
        LoadEquipment();
        LoadInventory();
        LoadWorld();
        LoadChests();
        LoadCampfires();
        LoadFurnaces();
        LoadCrockpots();
        LoadForest();
        LoadEnemies();
    }


    public void SetNewGameParameters() {
        cycle.time = cycle.startTime;

        player.SetSpawnPoint(forestGenerator.forestSize);

        player.playerStats.health = player.playerStats.maxHealth;
        player.playerStats.stamina = player.playerStats.maxStamina;
        player.playerStats.foodValue = player.playerStats.maxFood;
    }



    //Load methods
    private void LoadTime() {
        TimeData data = SaveSystem.LoadTime();
        if(data != null) {
            cycle.time = data.time;
        }
    }
    
    private void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null) {
            player.playerStats.health = data.health;
            player.playerStats.foodValue = data.food;
            player.playerStats.stamina = data.stamina;

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            player.spawnPoint = position;
            player.gameObject.transform.position = position;
            player.RemoveFocus();
        }
    }

    private void LoadInventory() {
        InventoryData data = SaveSystem.LoadInventory();
       
        if (data != null) {
            inventory.items.Clear();

            if (inventory.onItemChangedCallback != null) {
                inventory.onItemChangedCallback.Invoke();
            }

            for (int i = 0; i < data.itemPaths.Length; i++) {
                string path = data.itemPaths[i];
                int amount = data.itemAmounts[i];
                int durability = data.equipmentDurability[i];

                Item newItem = Resources.Load<Item>(path);

                Item theItem = ScriptableObject.Instantiate(newItem);
                theItem.amount = amount;
                theItem.name = theItem.name.Replace("(Clone)", "");

                if(theItem.type.ToString() == "Equipment") {
                    Equipment theEquip = theItem as Equipment;
                    theEquip.currentDurability = durability;
                    Inventory.instance.AddItem(theEquip);
                }
                else {
                    Inventory.instance.AddItem(theItem);
                } 
            }
        }
    }
    private void LoadEquipment() {
        EquipmentData data = SaveSystem.LoadEquipment();
       
        if (data != null) {
            for (int i = 0; i < equipManager.currentEquipment.Length; i++) {
                equipManager.DestroyEquipment(i);
            }

            for (int i = 0; i < data.equipmentPath.Length; i++) {
                if(data.equipmentPath[i] != null) {
                    string path = data.equipmentPath[i];
                    int durability = data.equipmentDurrability[i];

                    Equipment newEquip = Resources.Load<Equipment>(path);

                    Equipment theEquip = ScriptableObject.Instantiate(newEquip);
                    theEquip.currentDurability = durability;
                    theEquip.name = theEquip.name.Replace("(Clone)", "");

                    equipManager.Equip(theEquip);
                    player.playerStats.OnEquipmentChanged(theEquip, null);
                }
            }

            equipManager.currentAmmo = null;
            if(data.ammoPath != null) {
                Ammo ammo = Resources.Load<Ammo>(data.ammoPath);
                ammo.amount = data.ammoAmount;
                ammo.name = ammo.name.Replace("(Clone)", "");

                EquipmentManager.instance.EquipAmmo(ammo);
            }
        }
    }

    private void LoadWorld() {
        WorldData data = SaveSystem.LoadWorld();

        //items
        if (data != null) {
            worldManager.DestroyItemsInWorld();
            for (int i = 0; i < data.itemPaths.Length; i++) {
                string path = data.itemPaths[i];
                float x = data.x_coordinates_item[i];
                float y = data.y_coordinates_item[i];
                float z = data.z_coordinates_item[i];
                float xr = data.x_rotation_item[i];
                float yr = data.y_rotation_item[i];
                float zr = data.z_rotation_item[i];

                Vector3 position;
                position.x = x;
                position.y = y;
                position.z = z;

                GameObject prefab = Resources.Load<GameObject>(path);
                GameObject theItem = Instantiate(prefab, position, Quaternion.Euler(new Vector3(xr, yr, zr)), itemParent.transform);
                theItem.name = theItem.name.Replace("(Clone)", "");
            }
        }

        //constructions
        if(data != null) {
            worldManager.DestroyConstructionsInWorld();
            for (int i = data.constructionPath.Length - 1; i >= 0 ; i--) {
                string path = data.constructionPath[i];
                float x = data.x_coordinates_con[i];
                float y = data.y_coordinates_con[i];
                float z = data.z_coordinates_con[i];
                float xr = data.x_rotation_con[i];
                float yr = data.y_rotation_con[i];
                float zr = data.z_rotation_con[i];

                Vector3 position;
                position.x = x;
                position.y = y;
                position.z = z;

                GameObject prefab = Resources.Load<GameObject>(path);
                
                GameObject theObject = Instantiate(prefab, position, Quaternion.Euler(new Vector3(xr, yr, zr)), placeableObjectParent.transform);
                theObject.name = theObject.name.Replace("(Clone)", "");

            }
        }
    }
    private void LoadChests() {
        ChestData data = SaveSystem.LoadChest();
        
        if (data != null) {
            worldManager.SetChestsInWorld();
            int index = 0;

            for(int j = 0; j < data.itemsPerChest.Length; j++) {
                for(int i = 0; i < data.itemsPerChest[j]; i++) {
                    
                    string path = data.itemPaths[index];
                    int amount = data.itemAmounts[index];
                    int durability = data.equipmentDurability[index];

                    Item newItem = Resources.Load<Item>(path);

                    Item theItem = ScriptableObject.Instantiate(newItem);
                    theItem.amount = amount;
                    theItem.name = theItem.name.Replace("(Clone)", "");

                    if (theItem.type.ToString() == "Equipment") {
                        Equipment theEquip = theItem as Equipment;
                        theEquip.currentDurability = durability;
                        worldManager.GetChest(j).AddItemToChest(theEquip);
                    }
                    else {
                        for(int q = 0; q < amount; q++) {
                            worldManager.GetChest(j).AddItemToChest(theItem);
                        }
                    }

                    index++;
                }
            } 
        }    
    }
    private void LoadCampfires() {
        CampfireData data = SaveSystem.LoadCampfire();
        

        if(data != null) {
            worldManager.SetCampfiresInWorld();
            for (int i = 0; i < data.currentFuelPerCampfire.Length; i++) {
                worldManager.GetCampfire(i).AddFuel(data.currentFuelPerCampfire[i]);
            }
        }
    }
    private void LoadFurnaces() {
        FurnaceData data = SaveSystem.LoadFurnace();
        

        if(data != null) {
            worldManager.SetFurnacesInWorld();
            for (int i = 0; i < data.currentFuelPerFurnace.Length; i++) {
                float fuel = data.currentFuelPerFurnace[i];
                string inputPath = data.inputPath[i];
                int inputAmount = data.inputAmount[i];
                string outputPath = data.outpurPath[i];
                int outputAmount = data.outputAmount[i];
                string bufferPath = data.bufferPath[i];
                float sliderValue = data.sliderValue[i];

                worldManager.GetFurnace(i).AddFuel(fuel);

                if(inputPath != null) {
                    Item newItem = Resources.Load<Item>(inputPath);

                    Item theItem = ScriptableObject.Instantiate(newItem);
                    theItem.name = theItem.name.Replace("(Clone)", "");

                    for(int j = 0; j < inputAmount; j++) {
                        worldManager.GetFurnace(i).inputItem = theItem;
                        worldManager.GetFurnace(i).inputAmount = inputAmount;
                        worldManager.GetFurnace(i).sliderValue = sliderValue;
                    }
                }

                if (outputPath != null) {
                    Item newItem = Resources.Load<Item>(outputPath);

                    Item theItem = ScriptableObject.Instantiate(newItem);
                    theItem.name = theItem.name.Replace("(Clone)", "");

                    for (int j = 0; j < outputAmount; j++) {
                        worldManager.GetFurnace(i).outputItem = theItem;
                        worldManager.GetFurnace(i).outputAmount = outputAmount;
                    }
                }

                if(bufferPath != null) {
                    Item newItem = Resources.Load<Item>(bufferPath);

                    Item theItem = ScriptableObject.Instantiate(newItem);
                    theItem.name = theItem.name.Replace("(Clone)", "");

                    worldManager.GetFurnace(i).bufferItem = theItem;
                }
            }
        }

    }
    private void LoadCrockpots() {
        CrockpotData data = SaveSystem.LoadCrockpot();
        

        if(data != null) {
            worldManager.SetCrockpotsInWorld();
            for (int i = 0; i < data.currentFuelPerCrockpot.Length; i++) {
                float fuel = data.currentFuelPerCrockpot[i];
                string foodPath1 = data.foodPath1[i];
                string foodPath2 = data.foodPath2[i];
                string foodPath3 = data.foodPath3[i];
                string foodPath4 = data.foodPath4[i];
                string outpurPath = data.foodOutputPath[i];


                worldManager.GetCrockpot(i).currentFuel = fuel;

                if(foodPath1 != null) {
                    Item newItem = Resources.Load<Item>(foodPath1);

                    Food theItem = ScriptableObject.Instantiate(newItem) as Food;
                    theItem.name = theItem.name.Replace("(Clone)", "");

                    worldManager.GetCrockpot(i).food1 = theItem;
                }
                if (foodPath2 != null) {
                    Item newItem = Resources.Load<Item>(foodPath2);

                    Food theItem = ScriptableObject.Instantiate(newItem) as Food;
                    theItem.name = theItem.name.Replace("(Clone)", "");

                    worldManager.GetCrockpot(i).food2 = theItem;
                }
                if (foodPath3 != null) {
                    Item newItem = Resources.Load<Item>(foodPath3);

                    Food theItem = ScriptableObject.Instantiate(newItem) as Food;
                    theItem.name = theItem.name.Replace("(Clone)", "");

                    worldManager.GetCrockpot(i).food3 = theItem;
                }
                if (foodPath4 != null) {
                    Item newItem = Resources.Load<Item>(foodPath4);

                    Food theItem = ScriptableObject.Instantiate(newItem) as Food;
                    theItem.name = theItem.name.Replace("(Clone)", "");

                    worldManager.GetCrockpot(i).food4 = theItem;
                }
                if(outpurPath != null) {
                    Item newItem = Resources.Load<Item>(outpurPath);

                    Food theItem = ScriptableObject.Instantiate(newItem) as Food;
                    theItem.name = theItem.name.Replace("(Clone)", "");

                    worldManager.GetCrockpot(i).outputFood = theItem;
                }

            }
        }
    }
    private void LoadForest() {
        ForestData data = SaveSystem.LoadForest();
        
        if (data != null) {
            for (int i = 0; i < forestParent.childCount; i++) {
                Destroy(forestParent.GetChild(i).gameObject);
            }
            for (int i = 0; i < data.forestPaths.Length; i++) {
                string path = "Environment/Forest/" + data.forestPaths[i];
                float x_coord = data.x_coordinates[i];
                float y_coord = data.y_coordinates[i];
                float z_coord = data.z_coordinates[i];
                float x_rot = data.x_rotation[i];
                float y_rot = data.y_rotation[i];
                float z_rot = data.z_rotation[i];
                float scale = data.scale[i];

                Vector3 position;
                position.x = x_coord;
                position.y = y_coord;
                position.z = z_coord;

                Vector3 rotation;
                rotation.x = x_rot;
                rotation.y = y_rot;
                rotation.z = z_rot;

                GameObject element = Resources.Load<GameObject>(path);
                GameObject theElement = Instantiate(element);
                theElement.name = theElement.name.Replace("(Clone)", string.Empty);
                theElement.transform.SetParent(forestParent);
                theElement.transform.position = position;
                theElement.transform.eulerAngles = rotation;
                theElement.transform.localScale = Vector3.one * scale;
            }
        }
    }

    private void LoadEnemies() {
        EnemyData data = SaveSystem.LoadEnemies();
        if(data != null) {
            for (int i = 0; i < enemyParent.childCount; i++) {
                Destroy(enemyParent.GetChild(i).gameObject);
            }

            for (int i = 0; i < data.enemyPaths.Length; i++) {
                string path = "Enemy/" + data.enemyPaths[i];
                path = path.Replace("(Clone)", string.Empty);
                float x_coord = data.x_coordinates[i];
                float y_coord = data.y_coordinates[i];
                float z_coord = data.z_coordinates[i];
                float x_rot = data.x_rotation[i];
                float y_rot = data.y_rotation[i];
                float z_rot = data.z_rotation[i];
                float scale = data.scale[i];

                Vector3 position;
                position.x = x_coord;
                position.y = y_coord;
                position.z = z_coord;

                Vector3 rotation;
                rotation.x = x_rot;
                rotation.y = y_rot;
                rotation.z = z_rot;

                GameObject element = Resources.Load<GameObject>(path);
                GameObject theElement = Instantiate(element);
                theElement.name = theElement.name.Replace("(Clone)", string.Empty);
                theElement.transform.SetParent(enemyParent);
                theElement.transform.position = position;
                theElement.transform.eulerAngles = rotation;
                theElement.transform.localScale = Vector3.one * scale;
            }
        }
    }
}

