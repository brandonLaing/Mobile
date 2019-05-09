using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
  public static string saveName = "default";
  private static string SaveDirectory
  {
    get
    {
      string path = Path.Combine(Application.persistentDataPath, "saves", saveName);
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      return path;
    }
  }

  /// <summary>
  /// Saves serilizable file to memory
  /// </summary>
  /// <typeparam name="T">Type of data being saved</typeparam>
  /// <param name="target">Data being saved</param>
  /// <param name="fileName">Name of requested file including the extention</param>
  public static void Save<T>(T target, string fileName)
  {
    BinaryFormatter bf = new BinaryFormatter();
    using (FileStream fs = new FileStream(Path.Combine(SaveDirectory, fileName), FileMode.Create))
    {
      bf.Serialize(fs, target);
    }
    Debug.Log($"Saved {fileName}");
  }

  public static T Load<T>(string fileName)
  {
    if (SaveExist(fileName))
    {
      BinaryFormatter bf = new BinaryFormatter();
      T returnValue = default(T);
      using (FileStream fs = new FileStream(Path.Combine(SaveDirectory, fileName), FileMode.Open))
        returnValue = (T)bf.Deserialize(fs);

      Debug.Log($"Loaded {fileName}");
      return returnValue;
    }

    Debug.LogError($"Couldn't find {fileName}. Returning default type.");
    return default(T);
  }

  private static bool SaveExist(string fileName)
  {
    return File.Exists(Path.Combine(SaveDirectory, fileName));
  }
}
