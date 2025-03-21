using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public GameObject[] slots;
    public List<bool> slot_status;
    public int currentSlot;
    public GameObject[] ui_slots;
    private Image[] ui_slot_frames = new Image[3];
    private Color32 high_color, def_color;
    private int maxSlot = 2;

    void Awake()
    {
        high_color = new Color32(255,255,255,255);
        def_color = new Color32(175,175,175,255);

        for (int i = 0; i < 3; i++){
            ui_slot_frames[i] = ui_slots[i].gameObject.GetComponent<Image>();
        }
    }

    void Update()
    {
        UpdateSlots();

        if (Input.mouseScrollDelta.y > 0f){
            if (currentSlot >= maxSlot){
                currentSlot = 0;
            }
            else{
                currentSlot += 1;
            }
        }

        if (Input.mouseScrollDelta.y < 0f){
            if (currentSlot <= 0){
                currentSlot = maxSlot;
            }
            else{
                currentSlot -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){
            currentSlot = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            currentSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            currentSlot = 2;
        }
    }

    void UpdateSlots(){
        for (int i = 0; i < slots.Length; i++){
            if (i != currentSlot){
                ui_slot_frames[i].color = def_color;
            }
            else{
                ui_slot_frames[i].color = high_color;
            }

            if(!slot_status[i]){
                ui_slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color32(255,255,255,0);
                continue;
            }

            if (i != currentSlot){
                slots[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            else{
                slots[i].transform.GetChild(0).gameObject.SetActive(true);
                ui_slots[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = slots[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
                ui_slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color32(255,255,255,255);
            }
        }
    }
}