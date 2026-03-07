using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ColorBlock : MonoBehaviour
{

    public Material selectedMaterial,normalMaterial,highlightMaterial;

    [SerializeField] MeshRenderer renderer;

    private void Awake()
    {
      //  renderer = GetComponent<MeshRenderer>();
    }
   


    public void Highlight()
    {

        renderer.material = highlightMaterial;
    }
    public void Normal()
    {
        renderer.material = normalMaterial;

    }
    public void Select()
    {
        renderer.material = selectedMaterial;

    }
}
