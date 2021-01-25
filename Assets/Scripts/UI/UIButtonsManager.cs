using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonsManager : MonoBehaviour
{
    public static UIButtonsManager instance;

    [System.Serializable]
    public class UIBut
    {
        public string Name;
        public Transform Obj;
        public Sprite Active;
        public Sprite UnActive;
        public bool State = false;
        public bool Interactable = true;

        private float set;
        
        public float Set { get => set; set => set = value; }
    }

    [SerializeField]
    private List<UIBut> buttons = new List<UIBut>();

    private void Awake()
    {
        instance = this;
    }
    
    /* Change sprite if opened or not
     */
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
        UIBut _toSet = null;
        foreach (var but in buttons)
        {
            if (but.Name == name)
            {
                _toSet = but;
                break;
            }
        }
        
        if (_toSet != null && _toSet.State != state)
        {
            _toSet.State = state;
            if (state == true)
                _toSet.Obj.GetComponent<Image>().sprite = _toSet.Active;
            if (state == false)
                _toSet.Obj.GetComponent<Image>().sprite = _toSet.UnActive;
        }
    }

    /* Change interactable if opened or not
     */
    public void ButtonInteractable(string name)
    {
        SetInteractable(name, true);
    }
    public void ButtonNotInteractable(string name)
    {
        SetInteractable(name, false);
    }
    private void SetInteractable(string name, bool state)
    {
        UIBut _toSet = null;
        foreach (var but in buttons)
        {
            if (but.Name == name)
            {
                _toSet = but;
                break;
            }
        }

        if (_toSet != null && _toSet.Interactable != state)
        {
            _toSet.Obj.GetComponent<Button>().interactable = state;
            _toSet.Interactable = state;
            if(_toSet.State == true)
            {
                SetActive(name, false);
            }
        }
    }

    /* Return button
     */
    public UIButton GetButton(string name)
    {
        foreach (var but in buttons)
        {
            if (but.Name == name)
            {
                return but.Obj.GetComponent<UIButton>();
            }
        }
        return null;
    }

    public void ActionButton(string name)
    {
        GetButton(name).GetComponent<UIButton>().Action();
    }
    public void OpenButton(string name)
    {
        GetButton(name).GetComponent<UIButton>().Open();
    }
    public void CloseButton(string name)
    {
        GetButton(name).GetComponent<UIButton>().Close();
    }
}
