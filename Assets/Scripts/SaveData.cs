using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveData : MonoBehaviour
{
    private WavesData1 wavesData1;
    

    public void ToSaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(@"E:\Projects\Unity\Tower defenc\Data\MySaveData.dat");
        Data data = new Data();

        //wave
        data.idWave = wavesData1.numBW;
        data.numWave = wavesData1.countM;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Data saved");
    }
    public void ToLoadData()
    {
        if (File.Exists(@"E:\Projects\Unity\Tower defenc\Data\MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(@"E:\Projects\Unity\Tower defenc\Data\MySaveData.dat", FileMode.Open);
            Data data = (Data)bf.Deserialize(file);

            //wave
            wavesData1.startWave = true;
            wavesData1.numBW = data.idWave;
            wavesData1.countM = data.numWave;

            file.Close();
            Debug.Log("Data loaded");
        }
        else Debug.Log("Data not exists");
    }
    public void ToResetData()
    {
        if (File.Exists(@"E:\Projects\Unity\Tower defenc\Data\MySaveData.dat"))
        {
            File.Delete(@"E:\Projects\Unity\Tower defenc\Data\MySaveData.dat");
            Debug.Log("Data deleted");
        }
        else Debug.Log("Data not exist");
    }

    private void Start()
    {
        wavesData1 = GameObject.Find("Main Camera").GetComponent<WavesData1>();
    }
}
[System.Serializable]
class Data
{
    public int idWave;//айді рівня
    public int numWave;//номер останньої хвилі 
}
