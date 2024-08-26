using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string _dataDirPath, string _dataFileName)
    {
        dataDirPath = _dataDirPath;
        dataFileName = _dataFileName;
    }

    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(_data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            // fullPath에 해당하는 파일을 열어 데이터를 덮어쓰거나 파일이 없을시 새로 생성한다
            {
                using (StreamWriter writer = new StreamWriter(stream))
                // stream을 받아 파일에 텍스트를 쓸 수 있게 한다.
                {
                    writer.Write(dataToStore);
                    // dataToStore 문자열을 파일에 기록
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Error on trying to save data to file: " + fullPath + "\n" + e);
            throw;
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.Log("Error on trying to load data from file: " + fullPath + "\n" + e);
            }

        }

        return loadData;
    }

    public void Delete()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
