using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Unity.Jobs.LowLevel.Unsafe;

public class SaveLoadManager : MonoBehaviour
{
    public string _filePath;
    public List<GameObject> EnemySaves = new List<GameObject>();
    public GameObject PlayerSaves;
    private void Start()
    {
        _filePath = Application.persistentDataPath + "save.gamesave";
    }

    private void Update()
    {

    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream filestream = new FileStream(_filePath, FileMode.Create);

        Save save = new Save();
        if (EnemySaves != null)
        {
            save.SaveEnemies(EnemySaves);
        }
        save.SavePlayer(PlayerSaves);


        bf.Serialize(filestream, save);

        filestream.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(_filePath))
        {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream filestream = new FileStream(_filePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(filestream);
        filestream.Close();
        var player = save.PlayerData;
        PlayerSaves.GetComponent<PlayerBehaviour>().LoadDataPlayer(player);
        int i = 0;

        foreach (var enemy in save.EnemiesData)
        {
            EnemySaves[i].GetComponent<PatrolEnemy>().LoadDataEnemy(enemy);
            i++;
        }
    }
}

[System.Serializable]
public class Save
{
    [System.Serializable]
    public struct Vec3
    {
        public float x, y, z;

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    [System.Serializable]
    public struct EnemySaveData
    {
        public Vec3 Position, Direction;

        public EnemySaveData(Vec3 pos, Vec3 dir)
        {
            Position = pos;
            Direction = dir;
        }
    }

    public List<EnemySaveData> EnemiesData = new List<EnemySaveData>();

    public void SaveEnemies(List<GameObject> _enemies)
    {
        foreach (var enemy in _enemies)
        {
            if (enemy.GetComponent<PatrolEnemy>() != null)
            {
                var em = enemy.GetComponent<PatrolEnemy>();

                Vec3 pos = new Vec3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
                Vec3 dir = new Vec3(em.MoveVector.x, em.MoveVector.y, em.MoveVector.z);

                EnemiesData.Add(new EnemySaveData(pos, dir));

            }
        }
    }
    [System.Serializable]
    public struct PlayerSaveData
    {
        public Vec3 Position, Direction;

        public PlayerSaveData(Vec3 pos, Vec3 dir)
        {
            Position = pos;
            Direction = dir;
        }
    }
    public PlayerSaveData PlayerData = new PlayerSaveData();

    public void SavePlayer(GameObject _player)
    {
        var player = _player.GetComponent<PlayerBehaviour>();

        Vec3 pos = new Vec3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z);
        Vec3 dir = new Vec3(player.MoveVector.x, player.MoveVector.y, player.MoveVector.z);
        PlayerData = new PlayerSaveData(pos, dir);
    }
}
