using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the object's Z axis angle.
/// Stores the initial and last angles, provides methods to set and add angles.
/// </summary>
public class pivot_angle_script : MonoBehaviour
{
    /// <summary>
    /// The initial Z angle of the object.
    /// </summary>
    private float initialZ;

    /// <summary>
    /// The last set Z angle.
    /// </summary>
    private float lastZ;

    /// <summary>
    /// The current Z angle value, updated in the background for use by other scripts.
    /// </summary>
    [Tooltip("Current Z angle value (normalized, 0-360). Accessible by other scripts.")]
    public float pivotZValue;

    /// <summary>
    /// Records the initial Z angle at start.
    /// </summary>
    void Start()
    {
        initialZ = 0f;
        lastZ = initialZ;
        SetZAngle(0f);
        UpdatePivotZValue();
    }

    /// <summary>
    /// Checks key input every frame.
    /// Increases by 40 degrees with key 1, decreases by 40 degrees with key 2.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetZAngleSmooth(NormalizeAngle(GetLastZ() + 40f));
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetZAngleSmooth(NormalizeAngle(GetLastZ() - 40f));
        UpdatePivotZValue();
    }

    /// <summary>
    /// Sets the Z angle directly to the given value.
    /// </summary>
    /// <param name="z">New Z angle to set.</param>
    public void SetZAngle(float z)
    {
        lastZ = NormalizeAngle(transform.eulerAngles.z);
        float newZ = NormalizeAngle(z);
        Vector3 angles = transform.eulerAngles;
        angles.z = newZ;
        transform.eulerAngles = angles;
        UpdatePivotZValue();
    }

    /// <summary>
    /// Adds the given amount to the Z angle.
    /// </summary>
    /// <param name="delta">Amount to add to the angle.</param>
    public void AddZAngle(float delta)
    {
        lastZ = NormalizeAngle(transform.eulerAngles.z);
        float newZ = NormalizeAngle(transform.eulerAngles.z + delta);
        Vector3 angles = transform.eulerAngles;
        angles.z = newZ;
        transform.eulerAngles = angles;
        UpdatePivotZValue();
    }

    /// <summary>
    /// Sets the Z angle smoothly to the given value using a Coroutine.
    /// </summary>
    /// <param name="z">New Z angle to set.</param>
    /// <param name="duration">Animation duration (seconds).</param>
    public void SetZAngleSmooth(float z, float duration = 0.5f)
    {
        StopAllCoroutines();
        StartCoroutine(SetZAngleCoroutine(z, duration));
    }

    private IEnumerator SetZAngleCoroutine(float targetZ, float duration)
    {
        float startZ = NormalizeAngle(transform.eulerAngles.z);
        float elapsed = 0f;
        lastZ = startZ;
        targetZ = NormalizeAngle(targetZ);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newZ = Mathf.LerpAngle(startZ, targetZ, elapsed / duration);
            newZ = NormalizeAngle(newZ);
            Vector3 angles = transform.eulerAngles;
            angles.z = newZ;
            transform.eulerAngles = angles;
            UpdatePivotZValue();
            yield return null;
        }
        Vector3 finalAngles = transform.eulerAngles;
        finalAngles.z = NormalizeAngle(targetZ);
        transform.eulerAngles = finalAngles;
        lastZ = finalAngles.z;
        UpdatePivotZValue();
        Debug.Log($"Pivot Z angle: {Mathf.RoundToInt(finalAngles.z)}");
    }

    /// <summary>
    /// Returns the last set Z angle.
    /// </summary>
    public float GetLastZ() => lastZ;

    /// <summary>
    /// Normalizes the angle to the 0-360 range, rounds decimals, and sets very small values to 0.
    /// </summary>
    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        angle = Mathf.Round(angle); // Remove decimals
        if (Mathf.Abs(angle) < 0.01f || Mathf.Abs(angle - 360f) < 0.01f)
            return 0f;
        return angle;
    }

    /// <summary>
    /// Updates the public Z angle value for use by other scripts.
    /// </summary>
    private void UpdatePivotZValue()
    {
        pivotZValue = NormalizeAngle(transform.eulerAngles.z);
    }
}