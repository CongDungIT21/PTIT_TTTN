using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractScreen : MonoBehaviour
{
    public virtual void Show() { this.gameObject.SetActive(true); }

    public virtual void Hide() { this.gameObject.SetActive(false); }
}
