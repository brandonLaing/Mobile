using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
  #region Variables
  [SerializeField]
  [Tooltip("How many items might be spawned. Inclusive")]
  private int minItems = 1, maxItems = 5;
  [Tooltip("Items that may be spawned")]
  [SerializeField]
  private GameObject[] itemTable;

  private GameObject RandomItem
  {
    get
    {
      return itemTable[Random.Range(0, itemTable.Length)];
    }
  }
  #endregion

  #region Mono Behavior Functions

  #endregion

  #region Functions
  public void Interact()
  {
    int numberOfItems = Random.Range(minItems, maxItems + 1);
    for (int i = 0; i < numberOfItems; i++)
    {
      GameObject newItem = Instantiate(RandomItem, transform.position, Quaternion.identity);
      Rigidbody2D rb = newItem.GetComponent<Rigidbody2D>();
      rb.AddForce(new Vector2(Random.Range(-1F, 1F), Random.Range(-1F, 1F)));
    }

    Destroy(this.gameObject);
  }
  #endregion
}