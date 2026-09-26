
using HarmonyLib;
using NarratorMod.Assets;
using NarratorMod.Credits;
using Reactor.Utilities.Extensions;
using UnityEngine;

using Object = UnityEngine.Object;

namespace NarratorMod.Credits.Patches;

[HarmonyPatch]
public static class CreditsScreenPatches
{
    private const string MAIN_MENU_SPRITES_TEX_NAME = "MainMenuSprites";

    private static Texture2D _texture;
    private static GameObject _creditsScreen;

    private static GameObject GetCreditsScreen()
    {
        if (_creditsScreen) return _creditsScreen;

        var prefab = PresetAssets.CreditsMenu;
        if (prefab == null) return null;

        _creditsScreen = Object.Instantiate(prefab);
        _creditsScreen.name = "NarratorModCreditsMenu";
        _creditsScreen.AddComponent<CreditsScreenManager>();
        
        Object.DontDestroyOnLoad(_creditsScreen);
        _creditsScreen.SetActive(false);

        return _creditsScreen;
    }
    
    private static SpriteRenderer _staticPressed;
    private static SpriteRenderer _staticUnpressed;
    
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.ActivateMainMenuUI))]
    [HarmonyPostfix]
    public static void CreateCreditsButtonPatch(MainMenuManager __instance)
    {
        DoNotPressButton doNotPressButton = __instance.GetComponentInChildren<DoNotPressButton>(true);
        if (doNotPressButton == null) return;
        
        if (!_texture)
        {
            var tex = AssetManager.GetTexture(MAIN_MENU_SPRITES_TEX_NAME, "narrator-mod");
            if (tex != null) _texture = tex.DontUnload();
        }

        SpriteRenderer pedestalSprite = doNotPressButton.GetComponent<SpriteRenderer>();
        SpriteRenderer pressedSprite = doNotPressButton.transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer unpressedSprite = doNotPressButton.transform.GetChild(1).GetComponent<SpriteRenderer>();
        
        pedestalSprite.gameObject.SetActive(true);
        pressedSprite.gameObject.SetActive(true);
        unpressedSprite.gameObject.SetActive(true);

        unpressedSprite.enabled = true;
        pressedSprite.enabled = false;
        
        if (_texture != null)
        {
            MaterialPropertyBlock block = new();
            block.SetTexture("_MainTex", _texture);

            var spriteRenderers = doNotPressButton.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                if (!spriteRenderer.sprite || !spriteRenderer.sprite.texture) continue;
                
                if (spriteRenderer.sprite.texture.name.Contains(MAIN_MENU_SPRITES_TEX_NAME))
                {
                    spriteRenderer.SetPropertyBlock(block);
                }
            }
        }

        PassiveButton creditsButton = doNotPressButton.GetComponent<PassiveButton>();
        
        creditsButton.OnClick.RemoveAllListeners();
        creditsButton.OnMouseOver.RemoveAllListeners();
        creditsButton.OnMouseOut.RemoveAllListeners();

        _staticPressed = pressedSprite;
        _staticUnpressed = unpressedSprite;

        creditsButton.OnMouseOver.AddListener((UnityEngine.Events.UnityAction)OnHoverIn);
        creditsButton.OnMouseOut.AddListener((UnityEngine.Events.UnityAction)OnHoverOut);
        creditsButton.OnClick.AddListener((UnityEngine.Events.UnityAction)OnButtonClick);

        creditsButton.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
    }
    
    private static void OnHoverIn()
    {
        if (_staticPressed) _staticPressed.enabled = true;
        if (_staticUnpressed) _staticUnpressed.enabled = false;
    }
    
    private static void OnHoverOut()
    {
        if (_staticPressed) _staticPressed.enabled = false;
        if (_staticUnpressed) _staticUnpressed.enabled = true;
    }
    
    private static void OnButtonClick()
    {
        var screen = GetCreditsScreen();
        if (screen != null) screen.SetActive(true);
    }
}