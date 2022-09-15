using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BasePresenter : MonoBehaviour
{
    // Start is called before the first frame update
    protected void Start()
    {
        setButton();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    protected virtual void setButton(){}
}
