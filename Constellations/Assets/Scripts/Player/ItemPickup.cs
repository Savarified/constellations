using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Inventory iv;

    public GameObject[] all_items;
    [SerializeField] private GameObject closest_item;
    [SerializeField] private GameObject current_item;
    [SerializeField] private float range;
    void Awake()
    {
        all_items = GameObject.FindGameObjectsWithTag("Item");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            all_items = GameObject.FindGameObjectsWithTag("Item");
            if (iv.slot_status[iv.currentSlot]){return;}
            if (all_items.Length == 0){return;}
            FindClosestItem();
            if(Vector3.Distance(transform.position, closest_item.transform.position) <= range)
            {
                iv.slot_status[iv.currentSlot] = true;
                Equip();
            }
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            if(iv.slot_status[iv.currentSlot]){
                Unequip();
                iv.slot_status[iv.currentSlot] = false;
                FindClosestItem();
            }
        }

        if (iv.slot_status[iv.currentSlot]){
            current_item = iv.slots[iv.currentSlot].transform.GetChild(0).gameObject;
        }
        else{
            current_item = null;
        }
    }

    void FindClosestItem(){
        float closest_distance = Mathf.Infinity;
        foreach (GameObject item in all_items)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < closest_distance)
            {
                closest_distance = distance;
                closest_item = item;
            }
        }

    }

    void Equip(){
        closest_item.transform.SetParent(iv.slots[iv.currentSlot].gameObject.transform);
        closest_item.transform.localScale = new Vector3(1f,1f,1f);
        closest_item.tag = "Untagged";
        closest_item.transform.localPosition = new Vector3(0.65f,-0.15f,0f);
    }

    void Unequip(){
        if (current_item == null){
            return;
        }
        current_item.transform.localPosition += new Vector3(1f, 0f,0f);
        current_item.transform.SetParent(null);
        current_item.tag = "Item"; 
    }
}
