using NarratorMod.Assets;
using Reactor.Utilities.Attributes;
using TMPro;
using UnityEngine;

namespace NarratorMod.Credits;

[RegisterInIl2Cpp]
public sealed class CreditsScreenManager : MonoBehaviour
{
    private const string NARRATORS = "$ir Jackler(English), Hleb(Polish), Esto Sobrio(Spanish), Nolixo(German)";

    public static GameObject Additional;
    private static GameObject _puller;

    public void Awake()
    {
        TMP_FontAsset font = FindObjectOfType<VersionShower>().text.font;

        foreach (TextMeshPro tmp in GetComponentsInChildren<TextMeshPro>(true))
        {
            tmp.font = font;
        }
        
        Additional = transform.Find("Additional").gameObject;
        TransitionBounceDown transition = Additional.AddComponent<TransitionBounceDown>();
        transition.OutOfFramePos = new Vector3(Additional.transform.position.x, 6f, Additional.transform.position.z);
        transition.InFramePos = new Vector3(Additional.transform.position.x, Additional.transform.position.y,
            Additional.transform.position.z);
    }

    public void OnEnable()
    {
        Transform textParent = transform.Find("Credits");
        if (textParent == null) return;
        
        Transform translatorsTransform = transform.Find("Narrators/Text");
        if (translatorsTransform != null)
        {
            translatorsTransform.GetComponent<TextMeshPro>().text = $"<u><b>Narrators:</b></u> {NARRATORS}";
        }
        
        if (Additional != null) Additional.SetActive(false);
        
        GetPuller().SetActive(true);
        CreditsScreenPuller.Instance.Reset();
    }

    public void OnDisable()
    {
        SetAdditionalInactive();
        GetPuller().SetActive(false);
    }

    private void SetText(Transform parent, string childName, string text)
    {
        Transform child = parent.Find(childName);
        if (child != null)
        {
            var tmp = child.GetComponent<TextMeshPro>();
            if (tmp != null) tmp.text = text;
        }
    }
    
    public static void SetAdditionalInactive()
    {
        if (Additional != null) Additional.SetActive(false);
    }
    
    public GameObject GetPuller()
    {
        if (_puller) return _puller;

        var prefab = Assets.PresetAssets.CreditsPullerPrefab;
        if (prefab == null) return null;

        // _puller = Instantiate(prefab);
        _puller = transform.Find("PullerPrefab").gameObject;
        // _puller.name = "NarratorModPuller";
        CreditsScreenPuller comp = _puller.AddComponent<CreditsScreenPuller>();
        
        comp.OnTargetReached += () =>
        {
            Additional?.SetActive(true);
        };

        _puller.transform.localScale = new Vector3(0.08f, 0.08f, 1f);
        // _puller.transform.localPosition = new Vector3(4f,3f,-5f);
        comp.initialPos = new Vector3(4f,3f,-5f);
        
        DontDestroyOnLoad(_puller);
        _puller.SetActive(false);

        return _puller;
    }
}