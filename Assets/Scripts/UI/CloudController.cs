using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script rotates the sky.
/// </summary>
public class CloudController : MonoBehaviour
{
    /// <summary>
    /// Here the sky is rotated.
    /// </summary>
    private void Update()
    {
        transform.Rotate(Time.deltaTime * Vector3.up, Space.World);
    }
}
