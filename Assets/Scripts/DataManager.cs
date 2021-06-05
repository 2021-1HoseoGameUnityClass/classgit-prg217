using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //시스템에서 파일을 생성하기 위한 DLL
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance = null;
    public static DataManager instance { get { return _instance;  } }

    public int playerHP = 3;
    public string currentScene = "Scene2";

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.sceneName = currentScene;
        saveData.playerHP = playerHP;

        //파일 생성
        FileStream fileStream = File.Create(Application.persistentDataPath + "/save.dat");

        Debug.Log("저장 파일 생성");

        //직렬화
        Binarymatter binarymatter = new BinaryFormatter();
        binarymatter.Serialize(fileStream, saveData);

        //파일을 닫는다
        fileStream.Close();
    }

    public void Load()
    {
        //파일 있는지 체크
        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            FileStream fileStream = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);

            if (fileStream != null && fileStream.Length > 0)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                
                SaveData saveData = (SaveData)binaryFormatter.Deserialize(fileStream);

                playerHP = saveData.playerHP;
                UIManager.instance.PlayerHP();
                currentScene = SaveData.sceneName;

                fileStream.Close();
            }
        }
    }
}
