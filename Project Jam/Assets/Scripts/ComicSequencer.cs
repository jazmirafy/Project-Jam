using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicSequencer : MonoBehaviour
{
    [Tooltip("Assign your frame GameObjects in order: 1,2,3,4... (UI Images or Sprites).")]
    public List<GameObject> frames = new List<GameObject>();

    [Tooltip("Button that advances the comic.")]
    public Button nextButton;

    [Tooltip("Hide the Next button when the sequence finishes.")]
    public bool hideNextOnComplete = true;

    int step = -1; // -1 = none visible yet

    void Awake()
    {
        // Start with everything hidden
        for (int i = 0; i < frames.Count; i++)
            if (frames[i]) frames[i].SetActive(false);

        if (nextButton) nextButton.onClick.AddListener(Advance);
    }

    // Call this when the player wins to pop up the first frame immediately
    public void StartSequence()
    {
        gameObject.SetActive(true);
        step = -1;
        for (int i = 0; i < frames.Count; i++)
            if (frames[i]) frames[i].SetActive(false);

        Advance();
    }

    // Hook this to the Next button to advance the comic sequence
    public void Advance()
    {
        step++;

        // If everything is already visible
        if (step >= frames.Count)
        {
            if (hideNextOnComplete && nextButton) nextButton.gameObject.SetActive(false);
            return;
        }

        // Show all frames up to current step (1, 1+2, 1+2+3, ...)
        for (int i = 0; i < frames.Count; i++)
            if (frames[i]) frames[i].SetActive(i <= step);
    }
}
