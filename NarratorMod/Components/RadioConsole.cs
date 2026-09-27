using NarratorMod.Assets;
using Reactor.Utilities.Attributes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NarratorMod.Components;

[RegisterInIl2Cpp(new[] { typeof(IUsable) })]
public class RadioConsole : MonoBehaviour
{
    public RadioConsole(IntPtr ptr) : base(ptr) { }
    
    private SpriteRenderer _rend;

    public float UsableDistance => 1.0f;
    public float PercentCool => 0;
    public ImageNames UseIcon => ImageNames.UseButton;

    private void Start()
    {
        _rend = GetComponent<SpriteRenderer>();
        _rend.material = new(LobbyBehaviour.Instance.GetComponentInChildren<OptionsConsole>().Outline.material);
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
        // if (NarratorSettingsMenu.instance) return;

        Object.Instantiate(AssetManager.LoadAsset<GameObject>("SettingsMenuCanvas", "narrator-mod"));
    }
}