using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class LoadMap : MonoBehaviour
{
  [SerializeField]
  private TextAsset tiledAsset;
  [SerializeField]
  private Sprite[] sprites;

  private void Start()
  {
    LoadXml();
  }

  private void LoadXml()
  {
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.LoadXml(tiledAsset.text);

    XmlNode mapNode = xmlDoc.SelectSingleNode("map");

    int mapWidth = int.Parse(mapNode.Attributes["width"].Value);
    int mapHeight = int.Parse(mapNode.Attributes["height"].Value);
    int tileWidth = int.Parse(mapNode.Attributes["tilewidth"].Value);
    int tileHeight = int.Parse(mapNode.Attributes["tileheight"].Value);

    string[] splitImageName = mapNode.SelectSingleNode("tileset/image").Attributes["source"].Value.Split('/');
    string imageName = imageName = splitImageName[splitImageName.Length - 1].Split('.')[0]; ;
    

    Debug.Log($"Map Width: {mapWidth}\nMap Height: {mapHeight}\nTileWidth: {tileWidth}\nTileHeight: {tileHeight}\nImageName: {imageName}");

    sprites = Resources.LoadAll<Sprite>(imageName);

    XmlNodeList allLayers = mapNode.SelectNodes("layer");

    foreach (XmlNode layer in allLayers)
    {
      BuildLayer(layer, mapWidth, mapHeight, tileWidth, layer.Attributes["name"].Value);
    }

    BuildObjectLayer(mapNode.SelectSingleNode("objectgroup"), tileWidth);

  }

  private void BuildLayer(XmlNode mapNode, int mapWidth, int mapHeight, int tileWidth, string layerName)
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

          AddSpecificStuff(spriteObject, layerName);

        }
        xPos += spriteSize;
        currentTile++;
      }
      yPos -= spriteSize;
    }
  }

  private void AddSpecificStuff(GameObject sprite, string layerName)
  {
    switch(layerName)
    {
      case "Walls":
        sprite.AddComponent<BoxCollider2D>();
        break;
    }
  }

  private void BuildObjectLayer(XmlNode objectLayer, int tileWidth)
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

        Vector2 objectPosition = new Vector2((float.Parse(node.Attributes["x"].Value) / tileWidth),
          -(float.Parse(node.Attributes["y"].Value) / tileWidth) + 1);

        ingameObject.transform.position = objectPosition;
      }
      else
      {
        string objectType = node.SelectSingleNode("properties").SelectSingleNode("property").Attributes["name"].Value;
        GameObject playerSpawnPoint = new GameObject(objectType);

        Vector2 objectPosition = new Vector2((float.Parse(node.Attributes["x"].Value) / tileWidth),
          -(float.Parse(node.Attributes["y"].Value) / tileWidth) + 1);

        playerSpawnPoint.transform.position = objectPosition;
      }
    }
  }
}
