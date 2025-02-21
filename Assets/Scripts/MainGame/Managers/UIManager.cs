using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    
    [SerializeField] private PlayerCharacterController bobby;

    // [SerializeField] private GameObject skillsHolder;

    [SerializeField] private List<SkillButtonUI> skillsButtonsUI;

    [SerializeField] private Sprite[] skillIcons;

    
    public void RefreshHPText(int newHP)
    {
        hpText.text = newHP.ToString();
    }

    private void Awake()
    {
        bobby.onTakeDamageEventAction += RefreshHPText;
    }

    private void Start()
    {
        hpText.text = bobby.Hp.ToString();
        //Moved logic from update that only needs to happen once
        for(int i = 0; i < skillsButtonsUI.Count;i++){
            skillsButtonsUI[i].skillIcon.sprite = skillIcons[i];
            skillsButtonsUI[i].skillNameText.text = $"Skill {i+1}";
        }
    }

    
}
