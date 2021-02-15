using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [System.Serializable]
    public struct Menu
    {
        public string id;
        public GameObject panel;
    }
    [System.Serializable]
    public struct ItemMenuSlidder
    {
        public string id;
        public Slider slider;
    }
    [System.Serializable]
    public struct ItemMenuText
    {
        public string id;
        public TextMeshProUGUI text;
    }
    public static MenuManager instance;

    public GameObject background;

    public List<Menu> menus = new List<Menu>();

    public List<ItemMenuSlidder> itemsAudio = new List<ItemMenuSlidder>();

    public List<ItemMenuText> itemsText = new List<ItemMenuText>();

    bool audioSet = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeMenu("main");
        RefreshOptionInputs();
    }

    public virtual void ChangeMenu(string id, string beforeId)
    {
        if(id == "")
        {
            return;
        }
        else if(id == "play")
        {
            SceneManager.LoadScene("MainScene");
        }
        else if(id == "death" || id == "win")
        {
            Time.timeScale = 0;
        }
        else if (id == "options")
        {
            RefreshAudio();
            audioSet = true;
        }
        else if (id == "pause")
        {
            Time.timeScale = 0;
            id = "game_menu";
        }
        else if (id == "resume")
        {
            Time.timeScale = 1;
        }
        else if (id == "return_menu")
        {
            SceneManager.LoadScene("TitleScene");
        }

        bool find = false;
        bool findId = false;
        foreach (Menu item in menus)
        {
            if(item.id == id)
            {
                item.panel.SetActive(true);

                findId = true;

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
        if (!findId && background)
        {
            background.SetActive(false);
        }
        else if (findId && background)
        {
            background.SetActive(true);
        }
    }

    public void ChangeMenu(string id)
    {
        ChangeMenu(id, "");
    }

    public void RefreshAudio()
    {
        foreach (var item in itemsAudio)
        {
            float x = OptionsManager.instance.GetVolume(item.id);
            item.slider.value = x;
        }
    }

    public void ValidateAudio()
    {
        if (audioSet)
        {
            foreach (var item in itemsAudio)
            {
                OptionsManager.instance.SetVolume(item.id, item.slider.value);
            }
        }
    }

    public void ChangeInput()
    {
        KeyCollection.Change();
        RefreshOptionInputs();
    }

    void RefreshOptionInputs()
    {
        if (KeyCollection.qwerty)
        {
            GetText("keyboard").text = "QWERTY";
        }
        else
        {
            GetText("keyboard").text = "AZERTY";
        }
        GetText("inputForward").text = "Forward : " + KeyCollection.forwardKey;
        GetText("inputBackward").text = "Backward : " + KeyCollection.backwardKey;
        GetText("inputInventory").text = "Interract : " + KeyCollection.invKey;
        GetText("inputHangar").text = "Hangar : " + KeyCollection.hangarKey;
        GetText("inputBrake").text = "Brake : " + "L. Shift";
    }

    public void RedirectURL(string url)
    {
        Application.OpenURL(url);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public TextMeshProUGUI GetText(string id)
    {
        foreach (var item in itemsText)
        {
            if (item.id == id)
            {
                return item.text;
            }
        }
        return null;
    }
}
