using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;



public class SettingsMenu : MonoBehaviour, IInitializable
{
    [Header("Control Buttons")]
    [SerializeField]
    private Button applyButton;

    [SerializeField]
    private Button returnButton;

    [SerializeField]
    private Button cancelButton;

    [Header("Graphics settings")]
    [SerializeField]
    private TMP_Dropdown graphicsDropdown;
    [SerializeField]
    private Toggle isFullScreenToggle;


    [SerializeField]
    private SupportScreenResolutions supportedResolutions;

    private GraphicsSettingsManager graphicsManager;
    private SignalBus signalBus;



    [Inject]
    public void Construct(GraphicsSettingsManager graphicsManager, SignalBus signalBus)
    {
        this.signalBus = signalBus;
        this.graphicsManager = graphicsManager;
    }


    private void OnEnable()
    {
        if (graphicsDropdown != null)
            graphicsDropdown.onValueChanged.AddListener(OnResolutionChanged);
        if (isFullScreenToggle != null)
            isFullScreenToggle.onValueChanged.AddListener(OnScreenModeChanged);

        if (applyButton != null)
            applyButton.onClick.AddListener(ApplySettings);
        if (returnButton != null)
            returnButton.onClick.AddListener(Hide);
        if (cancelButton != null)
            cancelButton.onClick.AddListener(Cancel);
    }


    private void OnDisable()
    {
        if (graphicsDropdown != null)
            graphicsDropdown.onValueChanged.RemoveListener(OnResolutionChanged);
        if (applyButton != null)
            applyButton.onClick.RemoveListener(ApplySettings);
        if (returnButton != null)
            returnButton.onClick.RemoveListener(Hide);
        if (cancelButton != null)
            cancelButton.onClick.RemoveListener(Cancel);
    }


    public void Initialize()
    {
        signalBus.Subscribe<OpenSettingsMenuSignal>(Show);
        signalBus.Subscribe<CloseSettingsMenuSignal>(Hide);

        SetupDropdown();
    }


    private void SetupDropdown()
    {
        var graphicsSettings = graphicsManager.GetPendingSettings();

        var currentResolution = graphicsSettings.Resolution;
        graphicsDropdown.ClearOptions();

        var options = new List<string>();
        var currentIndex = 0;

        for(var i = 0; i < supportedResolutions.screenResolutions.Length; i++)
        {
            var res = supportedResolutions.screenResolutions[i];

            int height = AspectRatioUtility.GetHeight(res.aspectRatio, res.Width);
            options.Add($"({res.aspectRatio.ToString().Remove(0, 3)}){res.Width} x {height}");

            if(currentResolution.x == res.Width && currentResolution.y == height)
            {
                currentIndex = i;
            }
        }

        graphicsDropdown.AddOptions(options);
        graphicsDropdown.SetValueWithoutNotify(currentIndex);
    }


    private void OnResolutionChanged(int index)
    {
        var settings = graphicsManager.GetPendingSettings();

        var res = supportedResolutions.screenResolutions[index];
        int height = AspectRatioUtility.GetHeight(res.aspectRatio, res.Width);

        settings.Resolution = new Vector2Int(res.Width, height);

        // Preview immediately
        graphicsManager.Change(settings);
    }


    private void OnScreenModeChanged(bool isFullScreen)
    {
        var settings = graphicsManager.GetPendingSettings();
        settings.IsFullScreen = isFullScreen;

        // Preview immediately
        graphicsManager.Change(settings);
    }


    private void ApplySettings()
    {
        graphicsManager.Apply();
        graphicsManager.Save();
    }


    private void Show()
    {
        SetupDropdown();
        gameObject.SetActive(true);
    }


    private void Hide()
    {
        gameObject.SetActive(false);
        graphicsManager.DiscardUnappliedChanges();
    }


    private void Cancel()
    {
        graphicsManager.DiscardUnappliedChanges();
    }
}
