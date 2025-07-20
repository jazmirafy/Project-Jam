using System.Collections;
using UnityEngine;

public class PulseToTheBeat : MonoBehaviour
{
    [SerializeField] bool useTestBeat = false;
    [SerializeField] float pulseSize = 1.15f;
    [SerializeField] float returnSpeed = 5f;

    private Vector3 startSize;
    private RectTransform rectTransform;
    private bool isUI;

    private void Start()
    {
        // Determine if this is a UI element
        rectTransform = GetComponent<RectTransform>();
        isUI = rectTransform != null;

        // Set starting scale based on UI or World
        startSize = isUI ? rectTransform.localScale : transform.localScale;

        // Optional test pulse for debugging
        if (useTestBeat)
            StartCoroutine(TestBeat());
    }

    private void Update()
    {
        // Get current scale
        Vector3 currentScale = isUI ? rectTransform.localScale : transform.localScale;

        // Lerp toward original size
        Vector3 newScale = Vector3.Lerp(currentScale, startSize, Time.deltaTime * returnSpeed);

        // Apply it
        if (isUI)
            rectTransform.localScale = newScale;
        else
            transform.localScale = newScale;
    }

    public void Pulse()
    {
        if (isUI)
            rectTransform.localScale = startSize * pulseSize;
        else
            transform.localScale = startSize * pulseSize;
    }

    IEnumerator TestBeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Pulse();
        }
    }
}
