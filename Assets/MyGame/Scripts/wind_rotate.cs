using System;
using UnityEngine;

/// <summary>
/// RotationManager lets you configure multiple targets with independent rotation settings.
/// Configure entries in the Inspector (assign Transforms, speed, axis, and local/world space).
/// Other scripts can access entries, change speeds, enable/disable entries, or populate from children.
/// </summary>
[DisallowMultipleComponent]
public class WindRotateManager : MonoBehaviour
{
    [Serializable]
    public class RotationEntry
    {
        [Tooltip("Target Transform to rotate.")]
        public Transform target;

        [Tooltip("Rotation speed in degrees per second.")]
        public float speed = 180f;

        [Tooltip("Axis to rotate around. For typical windmills use Vector3.up or Vector3.forward depending on model orientation.")]
        public Vector3 axis = Vector3.up;

        public enum RotationMode { SpinLocal, SpinWorld, RotateAroundPivot }

        [Tooltip("How the target should be rotated: spin on its own axis (local/world) or rotate around a pivot (hub).")]
    public RotationMode mode = RotationMode.SpinLocal;

        [Tooltip("If using RotateAroundPivot mode, this transform will be used as the pivot. If null, the manager's transform is used.")]
        public Transform pivot;

        [Tooltip("Enable or disable this entry at runtime.")]
        public bool enabled = true;
    }

    [Tooltip("List of rotation entries to apply every frame.")]
    public RotationEntry[] entries = new RotationEntry[0];

    [Tooltip("If true and entries is empty, automatically populate entries with direct children on Awake.")]
    public bool populateChildrenIfEmpty = false;

    void Awake()
    {
        if (populateChildrenIfEmpty && (entries == null || entries.Length == 0))
            PopulateFromChildren();
    }

    void Update()
    {
        float dt = Time.deltaTime;
        if (dt <= 0f) return;

        for (int i = 0; i < entries.Length; i++)
        {
            var e = entries[i];
            if (e == null || !e.enabled) continue;
            if (e.target == null) continue;
            if (e.axis == Vector3.zero) continue;

            float degrees = e.speed * dt;
            Vector3 axis = e.axis.normalized;

            switch (e.mode)
            {
                case RotationEntry.RotationMode.SpinLocal:
                    // Rotate the target around its local axis (like spinning a blade around its own pivot)
                    e.target.Rotate(axis, degrees, Space.Self);
                    break;
                case RotationEntry.RotationMode.SpinWorld:
                    // Rotate the target around the world axis (keeps the world axis constant)
                    e.target.Rotate(axis, degrees, Space.World);
                    break;
                case RotationEntry.RotationMode.RotateAroundPivot:
                    // Rotate the target around a pivot point (hub). If no pivot assigned, use manager's transform as hub.
                    Vector3 pivotPos = (e.pivot != null) ? e.pivot.position : transform.position;
                    e.target.RotateAround(pivotPos, axis, degrees);
                    break;
            }
        }
    }

    // --- Helpers for runtime control ---
    public void SetSpeedForAll(float newSpeed)
    {
        foreach (var e in entries)
            if (e != null) e.speed = newSpeed;
    }

    public bool SetSpeedForTarget(Transform target, float newSpeed)
    {
        var e = FindEntryForTarget(target);
        if (e == null) return false;
        e.speed = newSpeed;
        return true;
    }

    public RotationEntry FindEntryForTarget(Transform target)
    {
        if (target == null) return null;
        foreach (var e in entries)
            if (e != null && e.target == target)
                return e;
        return null;
    }

    public void PopulateFromChildren()
    {
        var children = new System.Collections.Generic.List<RotationEntry>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var t = transform.GetChild(i);
            var entry = new RotationEntry { target = t, speed = 180f, axis = Vector3.up, mode = RotationEntry.RotationMode.SpinLocal, enabled = true };
            children.Add(entry);
        }
        entries = children.ToArray();
    }

    // Utility: enable/disable an entry by index
    public void SetEnabled(int index, bool enabled)
    {
        if (index < 0 || index >= entries.Length) return;
        var e = entries[index];
        if (e != null) e.enabled = enabled;
    }

    // Utility: get number of configured entries
    public int EntryCount() => entries != null ? entries.Length : 0;
}

