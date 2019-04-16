using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using System.Linq;

public class LoadMap : MonoBehaviour
{
  [SerializeField]
  private TextAsset tiledAsset;
  [SerializeField]
  private GameObject objectsLayer;

  private Sprite[] sprites;
  private Dictionary<int, XmlNode> allSpecialTiles;

  private int mapWidth, mapHeight, tileSize;
  private void Start()
  {
    LoadXml();
  }

  /// <summary>
  /// Loads the XML map into the game
  /// </summary>
  private void LoadXml()
  {
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.LoadXml(tiledAsset.text);

    XmlNode mapNode = xmlDoc.SelectSingleNode("map");

    mapWidth = int.Parse(mapNode.Attributes["width"].Value);
    mapHeight = int.Parse(mapNode.Attributes["height"].Value);
    tileSize = int.Parse(mapNode.Attributes["tilewidth"].Value);

    string[] splitImageName = mapNode.SelectSingleNode("tileset/image").Attributes["source"].Value.Split('/');
    string imageName = imageName = splitImageName[splitImageName.Length - 1].Split('.')[0]; ;
    

    Debug.Log($"Map Width: {mapWidth}\nMap Height: {mapHeight}\nTileWidth: {tileSize}\nTileHeight: {tileSize}\nImageName: {imageName}");

    sprites = Resources.LoadAll<Sprite>(imageName);

    XmlNodeList allLayers = mapNode.SelectNodes("layer");

    int layerCount = 0;
    foreach (XmlNode layer in allLayers)
    {
      BuildNonInteractableLayer(layer, mapWidth, mapHeight, tileSize, layer.Attributes["name"].Value, layerCount);
      layerCount++;
    }

    XmlNodeList special = mapNode.SelectSingleNode("tileset").SelectNodes("tile");
    allSpecialTiles = new Dictionary<int, XmlNode>();

    foreach (XmlNode node in special)
    {
      allSpecialTiles.Add(int.Parse(node.Attributes["id"].Value), node);
    }

    foreach (XmlNode objectGroupNode in mapNode.SelectNodes("objectgroup"))
    {
      BuildObjectGroup(objectGroupNode, tileSize, layerCount);
    }
  }

  /// <summary>
  /// Builds all the objects on the map
  /// </summary>
  /// <param name="xmlNodeList"></param>
  private void BuildObject(XmlNodeList xmlNodeList)
  {
    foreach (XmlNode node in xmlNodeList)
    {
      GameObject currentTile = new GameObject();
      currentTile.transform.parent = objectsLayer.transform;
      foreach (XmlNode property in node.SelectSingleNode("properties").SelectNodes("property"))
      {
        if (property.Attributes["name"].Value == "Type")
        {
          AddSpecificStuff(currentTile, property.Attributes["value"].Value);
        }
      }
    }
  }

  /// <summary>
  /// Builds non interactable Layer
  /// </summary>
  /// <param name="mapNode"></param>
  /// <param name="mapWidth"></param>
  /// <param name="mapHeight"></param>
  /// <param name="tileWidth"></param>
  /// <param name="layerName"></param>
  /// <param name="layerCount"></param>
  private void BuildNonInteractableLayer(XmlNode mapNode, int mapWidth, int mapHeight, int tileWidth, string layerName, int layerCount)
  {
    GameObject layerObject = new GameObject(layerName);
    layerObject.transform.parent = this.transform;

    string[] mapGrid = mapNode.SelectSingleNode("data").InnerText.Split(',');

    float xPos = 0, yPos = 0;
    float spriteSize = tileWidth / sprites[0].pixelsPerUnit;

    int currentTile = 0;
    for (int i = 0; i < mapHeight; i++)
    {
      xPos = 0;
      for (int j = 0; j < mapWidth; j++)
      {
        int currentSprite = int.Parse(mapGrid[currentTile]);
        if (currentSprite != 0)
        {
          GameObject spriteObject = new GameObject($"Tile: ({xPos}, {yPos})", typeof(SpriteRenderer));
          spriteObject.transform.parent = layerObject.transform;
          spriteObject.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite - 1];
          spriteObject.transform.position = new Vector3(xPos, yPos);
          spriteObject.GetComponent<SpriteRenderer>().sortingOrder = layerCount;

          if (layerName == "Walls")
            AddSpecificStuff(spriteObject, ObjectTypes.Wall);

        }
        xPos += spriteSize;
        currentTile++;
      }
      yPos -= spriteSize;
    }
  }

  /// <summary>
  /// Builds the object group
  /// </summary>
  /// <param name="objectLayer"></param>
  /// <param name="tileWidth"></param>
  /// <param name="layerCount"></param>
  private void BuildObjectGroup(XmlNode objectLayer, int tileWidth, int layerCount)
  {
    GameObject layerObject = new GameObject("ObjectLayer");
    layerObject.transform.parent = this.transform;

    float spriteSize = tileWidth / sprites[0].pixelsPerUnit;

    foreach (XmlNode node in objectLayer.SelectNodes("object"))
    {
      if (node.Attributes["gid"] != null)
      {
        int sprite = int.Parse(node.Attributes["gid"].Value);

        GameObject ingameObject = new GameObject("InGameObject", typeof(SpriteRenderer));
        ingameObject.transform.parent = layerObject.transform;
        ingameObject.GetComponent<SpriteRenderer>().sprite = sprites[sprite - 1];
        ingameObject.GetComponent<SpriteRenderer>().sortingOrder = layerCount;

        Vector2 objectPosition = new Vector2((float.Parse(node.Attributes["x"].Value) / tileWidth),
          -(float.Parse(node.Attributes["y"].Value) / tileWidth) + 1);

        ingameObject.transform.position = objectPosition;

        int gid = int.Parse(node.Attributes["gid"].Value) - 1;
        if (allSpecialTiles.ContainsKey(gid))
        {
          //Debug.Log("found gid");
          foreach (XmlNode gidNode in allSpecialTiles[gid].SelectSingleNode("properties").SelectNodes("property"))
          {
            //Debug.Log($"Going though property {gidNode.Attributes["name"].Value}");
            if (gidNode.Attributes["name"].Value == "Type")
            {
              //Debug.Log("Found Type with value of " + gidNode.Attributes["value"].Value);
              AddSpecificStuff(ingameObject, gidNode.Attributes["value"].Value);
            }
          }
        }

      }
      else
      {
        string objectType = node.SelectSingleNode("properties").SelectSingleNode("property").Attributes["name"].Value;
        if (objectType == "playerSpawn")
        {
          GameObject playerSpawnPoint = new GameObject(objectType);
          playerSpawnPoint.name = "PlayerSpawnPoint";
          Vector2 objectPosition = new Vector2((float.Parse(node.Attributes["x"].Value) / tileWidth),
            -(float.Parse(node.Attributes["y"].Value) / tileWidth) + 1);

          playerSpawnPoint.transform.position = objectPosition;

          GameObject.FindGameObjectWithTag("Player").transform.position = objectPosition;
        }
      }
    }
    layerCount++;
  }

  /// <summary>
  /// Adds certain items to the object based on its type
  /// </summary>
  /// <param name="editingObject"></param>
  /// <param name="values"></param>
  private void AddSpecificStuff(GameObject editingObject, params ObjectTypes[] values)
  {
    foreach (ObjectTypes type in values)
    {
      switch (type)
      {
        case ObjectTypes.Wall:
          editingObject.AddComponent<BoxCollider2D>();
          Rigidbody2D rb = editingObject.AddComponent<Rigidbody2D>();
          rb.isKinematic = true;
          rb.simulated = false;
          editingObject.isStatic = true;
            
          break;
        case ObjectTypes.Door:
          editingObject.AddComponent<Door>();
          break;
        case ObjectTypes.Pickup:
          editingObject.AddComponent<Pickup>();
          break;
        case ObjectTypes.Destructable:
          editingObject.AddComponent<Destructable>();
          break;
      }
    }
  }

  /// <summary>
  /// Add adds certain items to the object based on its type
  /// </summary>
  /// <param name="editingObject"></param>
  /// <param name="valueNames"></param>
  private void AddSpecificStuff(GameObject editingObject, string valueNames)
  {
    if (valueNames == "Pickup")
      Debug.Log(valueNames);

    List<string> enumNames = Enum.GetNames(typeof(ObjectTypes)).OfType<string>().ToList();

    if (enumNames.Contains(valueNames))
      AddSpecificStuff(editingObject, (ObjectTypes)Enum.Parse(typeof(ObjectTypes), valueNames, true));
  }
}