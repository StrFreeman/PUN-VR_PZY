using System.Collections.Generic;
using UnityEngine;

public class EyeTrackingRay : MonoBehaviour
{
    public bool isLeft;
    [SerializeField]
    private ExpRecorder recorder;

    [SerializeField]
    private LayerMask tergetLayer;
    [SerializeField]
    private float trackDistance = Mathf.Infinity;

    private List<EyeInteractable> lastHitInteractables = new List<EyeInteractable>();

    // Start is called before the first frame update
    void Start()
    {


    }

    void FixedUpdate()
    {
        UnHover(lastHitInteractables);
        lastHitInteractables.Clear();


        Vector3 rayCastDirection = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, rayCastDirection, out hit, trackDistance, tergetLayer))
        {
            var eyeInteractable = hit.transform.GetComponent<EyeInteractable>();
            if (eyeInteractable != null)
            {
                recorder.RecordEye(eyeInteractable.groupIndex, eyeInteractable.itemIndex0, eyeInteractable.itemIndex1, isLeft);
                eyeInteractable.IsHovered = true;

                DebugHelper.Log(DebugHelper.Field.eyeTrack, $"eyeTracking: {eyeInteractable.groupIndex},{eyeInteractable.itemIndex0},{eyeInteractable.itemIndex1}");
            }
        }
    }

    void UnHover(List<EyeInteractable> eyeInteractables)
    {
        for (int i = 0; i < eyeInteractables.Count; i++)
        {
            eyeInteractables[i].IsHovered = false; 
        }
    }

}
