using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [System.Serializable]
    public struct Menu
    {
        public string id;
        public GameObject panel;
    }

    public List<Menu> menus = new List<Menu>();

    private void Start()
    {
        ChangeMenu("main");
    }

    public void ChangeMenu(string id, string beforeId)
    {
        if(id == "")
        {
            return;
        }

        bool find = false;
        foreach (Menu item in menus)
        {
            if(item.id == id)
            {
                item.panel.SetActive(true);
                if (!find)
                    find = true;
                else
                    break;
            }
            else if ((beforeId != "" && item.id == beforeId) 
                    || (beforeId == "" && item.panel.activeSelf))
            {
                item.panel.SetActive(false);
                if (!find)
                    find = true;
                else
                    break;
            }
        }
    }
    public void ChangeMenu(string id)
    {
        ChangeMenu(id, "");
    }
}
