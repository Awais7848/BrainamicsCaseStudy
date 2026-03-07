using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public enum BlockColor
{
    Blue, Green, Red,Yellow,Pink
}
public class ColorBlock : MonoBehaviour
{
    public Color color;

    public Material selectedMaterial,normalMaterial;

    [SerializeField] MeshRenderer renderer;

    [SerializeField]bool MouseSelected;

    
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }
   


    public void Highlight()
    {

        MouseSelected = true;
        renderer.material = selectedMaterial;
    }
    public void Normal()
    {
        MouseSelected = false;
        renderer.material = normalMaterial;

    }
}
