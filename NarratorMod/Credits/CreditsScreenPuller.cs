using System;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace NarratorMod.Credits;

[RegisterInIl2Cpp]
public class CreditsScreenPuller : MonoBehaviour
{
    public static CreditsScreenPuller Instance;
    public bool isDragging;
    public bool targetReached;
    private Vector3 _offset;
    public Vector3 EndPos = new(4f, 1f, -5f);
    public Vector3 initialPos;

    public Action OnTargetReached;
    
    public void Awake()
    {
        if (Instance) Destroy(Instance.gameObject);

        Instance = this;
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (isDragging && !Input.GetMouseButton(0))
        {
            isDragging = false;
        }
    }

    public void OnMouseDown()
    {
        _offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    public void OnMouseDrag()
    {
        if (!isDragging) return;

        if (transform.position.y > 0f && !targetReached)
        {
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offset;
            transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);

            if (transform.position.y < 1f)
            {
                OnTargetReached?.Invoke();
                targetReached = true;

                transform.position = EndPos;
            }
        }
    }
    
    public void Reset()
    {
        targetReached = false;
        transform.localPosition = initialPos;
    }
}