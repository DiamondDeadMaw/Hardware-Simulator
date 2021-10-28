using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject inv; 
    IDictionary<string,IDictionary<string, List<string>>> savedStates;
    List<string> fileNames;
    public GameObject chipTemplate;
    void Start()
    {
        inv = GameObject.Find("InventoryCanvas");
        inv.SetActive(false);
    }
     
    public void activateInventory() {
        inv.SetActive(true);

        GameObject[] currentInventoryItems = GameObject.FindGameObjectsWithTag("inventoryComponent");
        foreach (GameObject obj in currentInventoryItems) {
            if (!obj.GetComponentInChildren<TextMeshProUGUI>().text.Equals("NAND")) {
                Destroy(obj);
            }
        }


        savedStates = gameObject.GetComponent<Load_States>().savedStates;
        fileNames = gameObject.GetComponent<Load_States>().fileNames;
        if (fileNames.Count>0) {
            for (int i =0; i<fileNames.Count; i++) {
                GameObject newChip = Instantiate(chipTemplate) as GameObject;
                newChip.SetActive(true);
                newChip.GetComponent<InvChipDisplay>().setText(fileNames[i]);
                newChip.transform.SetParent(chipTemplate.transform.parent);
                newChip.transform.localScale = chipTemplate.transform.localScale;
            }
        }
    }
    public void deActivateInventory() {
        inv.SetActive(false);
    }
}
