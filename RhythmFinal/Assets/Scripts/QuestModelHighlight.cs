using UnityEngine;

public class QuestModelHighlight : MonoBehaviour
{
    private Vector3 baseScale;
    public float highlightScale = 1.15f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        baseScale = transform.localScale;
    }

    public void SetSelected(bool selected)
    {
        transform.localScale = selected ? baseScale * highlightScale : baseScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
