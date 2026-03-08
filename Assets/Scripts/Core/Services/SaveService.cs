using Assets.Scripts.Game.Services;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace playground.Assets.Scripts.Core.Services
{
    public class SaveService :
            Service
    {
        string _savePath = Application.persistentDataPath;

        public bool CheckFileExists(string name)
        {
            return File.Exists(_savePath + "/" + name + ".iwd");
        }

        private void SaveFile(string jsonString, string fileName)
        {
            FileStream file;

            string destination = _savePath + "/" + fileName;

            if (File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, jsonString);
            file.Close();
        }

        private string LoadFile(string fileName)
        {
            string destination = _savePath + "/" + fileName;
            FileStream file;

            if (File.Exists(destination)) file = File.OpenRead(destination);
            else
            {
                Debug.LogWarning("File not found");
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            string jsonString = (string)bf.Deserialize(file);
            file.Close();
            return jsonString;
        }

        private string LoadFileFromResources(string fileName)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(fileName);
            /*string destination = Application.streamingAssetsPath + "/" + fileName;
            FileStream file;

            if (File.Exists(destination)) file = File.OpenRead(destination);
            else
            {
                Debug.LogError("File not found");
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();*/
            string jsonString = /*(string)bf.Deserialize(file)*/textAsset.text;
            //file.Close();
            return jsonString;
        }
    }
}