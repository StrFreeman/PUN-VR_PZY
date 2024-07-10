using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EyeInteractable : MonoBehaviour
{
    public int groupIndex;
    public int itemIndex0;
    public int itemIndex1;
    public bool changeMaterialOnHovered;

    public bool IsHovered { get; set; }

    [SerializeField]
    private UnityEvent<GameObject> OnObjectHover;

    [SerializeField]
    private Material OnHoverActiveMaterial;

    [SerializeField]
    private Material OnHoverInactiveMaterial;

    private MeshRenderer meshRenderer;

    void Start() => meshRenderer = GetComponent<MeshRenderer>();
    

    // Update is called once per frame
    void Update()
    {
        if (meshRenderer==null)
        {
            return;
        }
        if (IsHovered)
        {
            if(changeMaterialOnHovered)
            {
                meshRenderer.material = OnHoverActiveMaterial;
            }    
        }
        else
        {
            if (changeMaterialOnHovered)
            {
                meshRenderer.material = OnHoverInactiveMaterial;
            } 
        }

        IsHovered = false;
    }
}
