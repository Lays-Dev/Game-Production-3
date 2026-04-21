using UnityEngine;

public class PlayerFade : MonoBehaviour
{
    public Transform cameraTransform;
    public float fadeDistance = 2f;
    public float minAlpha = 0.2f;

    private Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        float dist = Vector3.Distance(cameraTransform.position, transform.position);

        float alpha = 1f;

        if (dist < fadeDistance)
        {
            alpha = Mathf.Lerp(minAlpha, 1f, dist / fadeDistance);
        }

        SetAlpha(alpha);
    }

    void SetAlpha(float alpha)
    {
        foreach (Renderer r in renderers)
        {
            foreach (Material mat in r.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color c = mat.color;
                    c.a = alpha;
                    mat.color = c;
                }
            }
        }
    }
}