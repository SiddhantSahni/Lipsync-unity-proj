using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioTesterDropdownUI : MonoBehaviour
{
    public Dropdown audioDropdown;
    public Button playButton;
    public AudioTester audioTester;
    public AudioClip[] audioClips;

    // Friendly names mapped in the same order as audioClips
    private List<string> displayNames = new List<string>()
    {
        "Danger",
        "Family",
        "Easier",
        "Camera",
        "Banana",
        "Animal"
    };

    void Start()
    {
        audioDropdown.ClearOptions();

        // Add dropdown labels using friendly names
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (string name in displayNames)
        {
            options.Add(new Dropdown.OptionData(name));
        }
        audioDropdown.AddOptions(options);

        // Ensure dropdown caption preview is updated correctly
        audioDropdown.value = 0;
        audioDropdown.captionText.text = displayNames[0];
        audioDropdown.onValueChanged.AddListener(UpdateCaptionText);

        // Setup play button
        playButton.onClick.AddListener(PlaySelected);
    }

    void UpdateCaptionText(int index)
    {
        if (index < displayNames.Count)
        {
            audioDropdown.captionText.text = displayNames[index];
        }
    }

    void PlaySelected()
    {
        int index = audioDropdown.value;
        if (index < audioClips.Length)
        {
            audioTester.GetComponent<AudioSource>().clip = audioClips[index];
            audioTester.audioClip = audioClips[index];
            audioTester.PlaySoundWithLipSync();
        }
    }
}
