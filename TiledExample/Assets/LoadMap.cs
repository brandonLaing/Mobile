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

    string imageName = mapNode.SelectSingleNode("tileset/image").Attributes["source"].Value;

    Debug.Log($"Map Width: {mapWidth}\nMap Height: {mapHeight}\nTileWidth: {tileWidth}\nTileHeight: {tileHeight}\nImageName: {imageName}");

    sprites = Resources.LoadAll<Sprite>(imageName.Split('.')[0]);


    /** First Example
    int firstSprite = int.Parse(mapNode.SelectSingleNode("layer/data").InnerText.Split(',')[0]);

    Debug.LogWarning(firstSprite);

    Sprite firstSpriteImage = sprites[firstSprite];

    GameObject spriteObject = new GameObject("tileOne", typeof(SpriteRenderer));
    spriteObject.GetComponent<SpriteRenderer>().sprite = sprites[firstSprite - 1];
  */

    //BuildLayer(mapNode, mapWidth, mapHeight, tileWidth, "FirstLayer");

    XmlNodeList allLayers = mapNode.SelectNodes("layer");

    foreach (XmlNode layer in allLayers)
    {
      BuildLayer(layer, mapWidth, mapHeight, tileWidth, layer.Attributes["name"].Value);
    }

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
          GameObject spriteObject = new GameObject($"Tile: {xPos} - {yPos}", typeof(SpriteRenderer));
          spriteObject.transform.parent = layerObject.transform;
          spriteObject.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite - 1];
          spriteObject.transform.position = new Vector3(xPos, yPos);
        }
        xPos += spriteSize;
        currentTile++;
      }
      yPos -= spriteSize;
    }
  }
}
