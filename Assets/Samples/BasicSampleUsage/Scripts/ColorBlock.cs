using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField] LayerMask IgnoreMask;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }
    private void OnMouseDown()
    {
        MouseSelected = true;
        renderer.material = selectedMaterial;

    }
    private void OnMouseUp()
    {
        MouseSelected = false;
        renderer.material = normalMaterial;
    }
    private void Update()
    {
        if (MouseSelected)
        {
            RaycastHit hit; transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,IgnoreMask))
            {
                transform.position =new Vector3( hit.point.x,0.5f,hit.point.y) ;
            }
           
        }
    }

}
