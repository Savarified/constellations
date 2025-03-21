using UnityEngine;
using System.Collections.Generic;
public class Inventory : MonoBehaviour
{
    public GameObject[] slots;
    public List<bool> slot_status;
    [SerializeField] private int currentSlot;
    private int maxSlot = 2;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        UpdateSlots();
    }

    void UpdateSlots(){
        for (int i = 0; i < slots.Length; i++){
            if ( (i != currentSlot)&&(slot_status[i]) ){
                slots[i].SetActive(false);
            }
            if ( (i==currentSlot)&&(slot_status[i])){
                slots[i].SetActive(false);
            }
        }
    }
}
