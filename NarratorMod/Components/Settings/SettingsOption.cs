using Reactor.Utilities;
using Reactor.Utilities.Attributes;
using TMPro;
using UnityEngine;

namespace NarratorMod.Components.Settings;

[RegisterInIl2Cpp]
public class SettingsOption : MonoBehaviour
{
    public SettingsOption(IntPtr ptr) : base(ptr) { }
    
    public string OptionName;
    public PassiveButton? nextButton;
    public PassiveButton? previousButton;
    public TextMeshPro? valueTMP;

    public SettingsManager? manager;

    private void Awake()
    {
        nextButton = transform.Find("Value/PlusButton").GetComponent<PassiveButton>();
        previousButton = transform.Find("Value/MinusButton").GetComponent<PassiveButton>();
        valueTMP = transform.Find("Value/Value_TMP").GetComponent<TextMeshPro>();
        
        nextButton.OnClick.RemoveAllListeners();
        previousButton.OnClick.RemoveAllListeners();

        OptionName = gameObject.name;
    }

    public void UpdateValue(string value)
    {
        if (valueTMP == null) return;

        valueTMP.text = value;
        
        Logger<NarratorModPlugin>.Info($"{OptionName} updated to {value}");
    }
}