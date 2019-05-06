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
  private GameObject enempyPrefab;

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
      allSpecialTiles.Add(int.Parse(node.Attributes["id"].Value), node);

    foreach (XmlNode objectGroupNode in mapNode.SelectNodes("objectgroup"))
      BuildObjectGroup(objectGroupNode, tileSize, layerCount);
  }

  /// <summary>
  /// Builds non intractable Layer
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
          foreach (XmlNode gidNode in allSpecialTiles[gid].SelectSingleNode("properties").SelectNodes("property"))
          {
            if (gidNode.Attributes["name"].Value == "Type")
            {
              AddSpecificStuff(ingameObject, gidNode.Attributes["value"].Value);
            }
          }
        }

      }
      else
      {
        XmlNode properties = node.SelectSingleNode("properties");
        foreach (XmlNode prop in properties.SelectNodes("property"))
        {
          string name = prop.Attributes["name"].Value;
          if (name == "playerSpawn")
          {
            GameObject playerSpawnPoint = new GameObject(name)
            {
              name = "PlayerSpawnPoint"
            };
            Vector2 objectPosition = new Vector2((float.Parse(node.Attributes["x"].Value) / tileWidth),
              -(float.Parse(node.Attributes["y"].Value) / tileWidth) + 1);

            playerSpawnPoint.transform.position = objectPosition;

            GameObject.FindGameObjectWithTag("Player").transform.position = objectPosition;
          }
          else if (name == "EnemySpawn")
          {
            Vector2 objectPosition = new Vector2((float.Parse(node.Attributes["x"].Value) / tileWidth),
              -(float.Parse(node.Attributes["y"].Value) / tileWidth) + 1);

            GameObject enemy = Instantiate(enempyPrefab, objectPosition, Quaternion.identity); enemy.name = "Enemy(Clone)";
            foreach (XmlNode prop2 in properties.SelectNodes("property"))
            {
              if (prop2.Attributes["name"].Value == "Health")
                enemy.GetComponent<HealthSystem>().Health = int.Parse(prop2.Attributes["value"].Value);

              if (prop2.Attributes["name"].Value == "MaxHealth")
                enemy.GetComponent<HealthSystem>().healthMax = int.Parse(prop2.Attributes["value"].Value);

              EnemyAI ai = enemy.GetComponent<EnemyAI>();
              if (prop2.Attributes["name"].Value == "AttackTime")
                ai.attackTime = float.Parse(prop2.Attributes["value"].Value);

              if (prop2.Attributes["name"].Value == "ChaseRange")
                ai.chaseRange = float.Parse(prop2.Attributes["value"].Value);

              AbstractAttack attack;
              if (prop2.Attributes["name"].Value == "IsMelee")
              {
                if (prop2.Attributes["value"].Value == bool.TrueString)
                  attack = enemy.AddComponent<MeleeAttack>();
                else
                  attack = enemy.AddComponent<RangeAttack>();
              }
            }
          }
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
          editingObject.isStatic = true;
          break;
        case ObjectTypes.Door:
          editingObject.AddComponent<Door>();
          break;
        case ObjectTypes.Chest:
          editingObject.AddComponent<Chest>();
          break;
        case ObjectTypes.Destructable:
          editingObject.AddComponent<Destructable>();
          break;
        case ObjectTypes.HealthPickup:
          editingObject.AddComponent<HealthPickup>();
          break;
        case ObjectTypes.TansitionDoor:
          editingObject.AddComponent<TransitionDoor>();
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
    //if (valueNames == "Pickup")
      //Debug.Log(valueNames);

    List<string> enumNames = Enum.GetNames(typeof(ObjectTypes)).OfType<string>().ToList();

    if (enumNames.Contains(valueNames))
      AddSpecificStuff(editingObject, (ObjectTypes)Enum.Parse(typeof(ObjectTypes), valueNames, true));
  }
}