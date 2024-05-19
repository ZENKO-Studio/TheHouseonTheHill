using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventBus;

public class ItemInspector : MonoBehaviour
{
    public GameObject inspectionPanel;
    public Transform inspectionArea;
    private GameObject currentItem;

    private void Start()
    {
        EventBus.Subscribe<ItemInspectedEvent>(OnItemInspected);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<ItemInspectedEvent>(OnItemInspected);
    }

    public void InspectItem(GameObject itemPrefab)
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
        }
        currentItem = Instantiate(itemPrefab, inspectionArea);
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;
        inspectionPanel.SetActive(true);
    }

    public void RotateItem(Vector2 rotation)
    {
        if (currentItem != null)
        {
            currentItem.transform.Rotate(Vector3.up, rotation.x, Space.World);
            currentItem.transform.Rotate(Vector3.right, -rotation.y, Space.World);
        }
    }

    public void CloseInspection()
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
        }
        inspectionPanel.SetActive(false);
    }

    private void OnItemInspected(ItemInspectedEvent inspectedEvent)
    {
        InspectItem(inspectedEvent.Item.gameObject);
    }
}
