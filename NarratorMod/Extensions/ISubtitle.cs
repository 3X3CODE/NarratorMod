using System.Collections;
using TMPro;
using UnityEngine;

namespace NarratorMod.Extensions;

public interface ISubtitle
{
    IEnumerator TransitionIn(GameObject container);
    IEnumerator TextAnimation(string text, TextMeshPro textMeshPro);
    IEnumerator TransitionOut(GameObject container);
}