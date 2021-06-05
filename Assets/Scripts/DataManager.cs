using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //�ý��ۿ��� ������ �����ϱ� ���� DLL
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

        //���� ����
        FileStream fileStream = File.Create(Application.persistentDataPath + "/save.dat");

        Debug.Log("���� ���� ����");

        //����ȭ
        Binarymatter binarymatter = new BinaryFormatter();
        binarymatter.Serialize(fileStream, saveData);

        //������ �ݴ´�
        fileStream.Close();
    }

    public void Load()
    {
        //���� �ִ��� üũ
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
