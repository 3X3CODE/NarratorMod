using NarratorMod.API;
using NarratorMod.Assets;
using NarratorMod.Components.Settings;
using Reactor.Utilities.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NarratorMod.Components;

[RegisterInIl2Cpp(new[] { typeof(IUsable) })]
public class RadioConsole : MonoBehaviour
{
    public RadioConsole(IntPtr ptr) : base(ptr) { }
    
    private SpriteRenderer _rend;
    private GameObject? _settings;
    private GameObject? _highlight;

    public float UsableDistance => 1.0f;
    public float PercentCool => 0;
    public ImageNames UseIcon => ImageNames.UseButton;

    private void Start()
    {
        _rend = GetComponent<SpriteRenderer>();
        //_rend.material = new(LobbyBehaviour.Instance.GetComponentInChildren<OptionsConsole>().Outline.material);
        _highlight = transform.Find("Highlight").gameObject;
        _highlight.SetActive(false);;
    }

    private void SetOutline(bool isVisible, bool isTargeted)
    {
        if (_rend == null) return;
        
        _rend.material.SetFloat("_Outline", isVisible ? 1 : 0);
        _rend.material.SetColor("_OutlineColor", Color.white);
        _rend.material.SetColor("AddColor", isTargeted ? Color.white : Color.clear);
    }

    public float CanUse(NetworkedPlayerInfo playerInfo, out bool canUse, out bool couldUse)
    {
        var playerControl = playerInfo.Object;
        var truePosition = playerControl.GetTruePosition();

        couldUse = playerControl.CanMove && AmongUsClient.Instance.AmHost;
        canUse = couldUse;

        if (couldUse)
        {
            var playerDistance = Vector2.Distance(truePosition, transform.position);
            canUse = couldUse && playerDistance <= UsableDistance;
            return playerDistance;
        }

        return float.MaxValue;
    }

    public void Use()
    {
        CanUse(PlayerControl.LocalPlayer.Data, out var canUse, out _);
        if (!canUse) return;
        
        if (_settings == null && HudManager.InstanceExists)
        {
            _settings = Instantiate(PresetAssets.SettingsPanelPrefab, HudManager.Instance.transform);
            if (_settings == null) return;
            _settings.AddComponent<SettingsManager>();
            _settings.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
            SetLayerRecursively(_settings.transform, "UI");
        }
        if (_settings == null) return;
        
        _settings.SetActive(true);
        if (SettingsManager.Instance != null && NarratorRegistry.ActiveNarrator != null)
        {
            SettingsManager.Instance.UpdateNarrator(NarratorRegistry.ActiveNarrator);
        }
    }

    public void SetLayerRecursively(Transform? target, string layer)
    {
        if (target == null) return;

        for (int i = 0; i < target.GetChildCount(); i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.layer = LayerMask.NameToLayer(layer);
            SetLayerRecursively(child, layer);
        }
    }
    
    /*public void Update()
    {
        CanUse(PlayerControl.LocalPlayer.Data, out var canUse, out _);
        if (canUse && !_highlight.active)
        {
            _highlight.SetActive(true);
            return;
        }
        _highlight.SetActive(false);
    }*/
}