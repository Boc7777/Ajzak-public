using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
   

    public List<GameObject>
        GoldenChestItems = new List<GameObject>(),
        TreasureRoomItems = new List<GameObject>(),
        DevilRoomItems = new List<GameObject>(),
        AngelRoomItems = new List<GameObject>();


    

    public void RemoveGoldenChestItem(string name) => RemoveItem(ref GoldenChestItems, name);
    public void RemoveTreasureRoomItem(string name) => RemoveItem(ref TreasureRoomItems, name);

    public void RemoveDevilRoomItem(string name) => RemoveItem(ref DevilRoomItems, name);

    public void RemoveAngelRoomItem(string name) => RemoveItem(ref AngelRoomItems, name);

    private void RemoveItem(ref List<GameObject> list, string name)
    {
        
        var item = list.Find(listItem => listItem.name + "(Clone)" == name);
        var itemOriginal = list.Find(listItem => listItem.name == name);

        if (item)
        {
            list.Remove(item);
        }

        else if (itemOriginal)
        {
            list.Remove(itemOriginal);
        }
            
    }

}
