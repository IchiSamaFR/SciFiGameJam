using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonsManager : MonoBehaviour
{
    public static UIButtonsManager instance;

    [System.Serializable]
    public struct UIBut
    {
        public string Name;
        public Transform Obj;
        public Sprite Active;
        public Sprite UnActive;
        public bool State;
    }

    [SerializeField]
    private List<UIBut> buttons = new List<UIBut>();

    private void Awake()
    {
        instance = this;
    }


    public void ButtonActive(string name)
    {
        SetActive(name, true);
    }
    public void ButtonUnactive(string name)
    {
        SetActive(name, false);
    }
    private void SetActive(string name, bool state)
    {
        UIBut toSet = new UIBut();
        foreach (var but in buttons)
        {
            if (but.Name == name)
            {
                toSet = but;
                break;
            }
        }

        if (toSet.Name != "")
        {
            toSet.State = state;
            toSet.Obj.GetComponent<Button>().interactable = !state;
            if (state == true)
                toSet.Obj.GetComponent<Image>().sprite = toSet.Active;
            if (state == false)
                toSet.Obj.GetComponent<Image>().sprite = toSet.UnActive;
        }
    }
}
