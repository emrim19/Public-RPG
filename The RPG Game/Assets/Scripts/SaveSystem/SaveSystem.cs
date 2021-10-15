using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    //delete data
    public static void DeleteData(string path) {
        string thePath = Application.persistentDataPath + path;
        try {
            File.Delete(thePath);
        }
        catch(Exception ex) {
            Debug.LogError(ex);
        }
    }
    public static void DeleteAllData() {
        DeleteData("/time.saves");
        DeleteData("/player.saves");
        DeleteData("/inventory.saves");
        DeleteData("/equipment.saves");
        DeleteData("/world.saves");
        DeleteData("/chest.saves");
        DeleteData("/campfire.saves");
        DeleteData("/furnace.saves");
        DeleteData("/crockpot.saves");
        DeleteData("/forest.saves");
        DeleteData("/enemy.saves");
    }


    //Save and load data
    public static void SaveTimer(DayNightCycle cycle) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/time.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        TimeData data = new TimeData(cycle);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static TimeData LoadTime() {
        string path = Application.persistentDataPath + "/time.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            TimeData data = formatter.Deserialize(stream) as TimeData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void SavePlayer(Player player) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
        
    }
    public static PlayerData LoadPlayer() {
        string path = Application.persistentDataPath + "/player.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else{
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }



    public static void SaveInventory(Inventory inventory) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(inventory);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static InventoryData LoadInventory() {
        string path = Application.persistentDataPath + "/inventory.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }


    public static void SaveEquipment(EquipmentManager equipManager) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/equipment.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        EquipmentData data = new EquipmentData(equipManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static EquipmentData LoadEquipment() {
        string path = Application.persistentDataPath + "/equipment.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EquipmentData data = formatter.Deserialize(stream) as EquipmentData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }



    public static void SaveWorld(WorldManager worldManager) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/world.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        WorldData data = new WorldData(worldManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static WorldData LoadWorld() {
        string path = Application.persistentDataPath + "/world.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            WorldData data = formatter.Deserialize(stream) as WorldData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }


    public static void SaveChest(WorldManager worldManager) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/chest.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        ChestData data = new ChestData(worldManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static ChestData LoadChest() {
        string path = Application.persistentDataPath + "/chest.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ChestData data = formatter.Deserialize(stream) as ChestData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }



    public static void SaveCampfire(WorldManager worldManager) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/campfire.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        CampfireData data = new CampfireData(worldManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static CampfireData LoadCampfire() {
        string path = Application.persistentDataPath + "/campfire.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CampfireData data = formatter.Deserialize(stream) as CampfireData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveFurnace(WorldManager worldManager) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/furnace.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        FurnaceData data = new FurnaceData(worldManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static FurnaceData LoadFurnace() {
        string path = Application.persistentDataPath + "/furnace.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            FurnaceData data = formatter.Deserialize(stream) as FurnaceData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }



    public static void SaveCrockpot(WorldManager worldManager) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/crockpot.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        CrockpotData data = new CrockpotData(worldManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static CrockpotData LoadCrockpot() {
        string path = Application.persistentDataPath + "/crockpot.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CrockpotData data = formatter.Deserialize(stream) as CrockpotData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }


    public static void SaveForest(Transform forestPaerent) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/forest.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        ForestData data = new ForestData(forestPaerent);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static ForestData LoadForest() {
        string path = Application.persistentDataPath + "/forest.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ForestData data = formatter.Deserialize(stream) as ForestData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }


    public static void SaveEnemies(Transform enemyTransform) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/enemy.saves";
        FileStream stream = new FileStream(path, FileMode.Create);

        EnemyData data = new EnemyData(enemyTransform);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static EnemyData LoadEnemies() {
        string path = Application.persistentDataPath + "/enemy.saves";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EnemyData data = formatter.Deserialize(stream) as EnemyData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
