using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelManager : MonoBehaviour
{
    [SerializeField]
    private Image imageSelected;
    [SerializeField]
    private Image imageMinimap;

    [SerializeField]
    private List<Transform> turretsContent = new List<Transform>();
    [SerializeField]
    private List<Transform> thrustersContent = new List<Transform>();

    public List<Transform> TurretsContent { get => turretsContent; set => turretsContent = value; }
    public List<Transform> ThrustersContent { get => thrustersContent; set => thrustersContent = value; }
    public Image ImageMinimap { get => imageMinimap; set => imageMinimap = value; }
    public Image ImageSelected { get => imageSelected; set => imageSelected = value; }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
