using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsSun : MonoBehaviour
{
    [SerializeField] Transform sol;

    private void Update() {
        transform.eulerAngles = new Vector3(sol.rotation.eulerAngles.x, -sol.rotation.eulerAngles.y, sol.rotation.eulerAngles.z);
    }
}
