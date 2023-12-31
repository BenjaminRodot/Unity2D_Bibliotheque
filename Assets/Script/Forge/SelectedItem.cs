using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SelectedItem : MonoBehaviour
{
    #region Singleton
    public static SelectedItem instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of SelectedItem found !");
            return;
        }

        instance = this;
        //DontDestroyOnLoad(this);
    }
    #endregion

    public Item itemSelected;
    public GameObject itemToClone;
    public List<GameObject> mineralsDropedGameObject;
    public List<Mineral> mineralsDroped;

    private Camera mainCamera;
    private Vector3 mousePos;
    private InventoryManagerForge inventoryManagerForge;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        inventoryManagerForge = GameObject.FindObjectOfType<InventoryManagerForge>();
    }
    public void SpawnItem(Item item)
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        itemToClone.GetComponent<SpriteRenderer>().sprite = item.image;
        GameObject itemDroped = Instantiate(itemToClone, mousePos, Quaternion.identity);
        mineralsDropedGameObject.Add(itemDroped);
        mineralsDroped.Add((Mineral)item);
    }

    public void RetakeDroppedItem()
    {
        for(int i = 0; i < mineralsDropedGameObject.Count; i++)
        {
            Inventory.instance.Add(mineralsDroped[i]);
            inventoryManagerForge.AddItem(mineralsDroped[i]);
            Destroy(mineralsDropedGameObject[i]);
        }
        mineralsDropedGameObject.Clear();
        mineralsDroped.Clear();
    }

    public void LoseDroppedItem()
    {
        for (int i = 0; i < mineralsDropedGameObject.Count; i++)
        {
            Destroy(mineralsDropedGameObject[i]);
        }
        mineralsDropedGameObject.Clear();
        mineralsDroped.Clear();
    }
}
