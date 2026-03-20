using System.Collections.Generic;
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

    [SerializeField]
    private SupportScreenResolutions supportedResolutions;

    private SettingsUIBuilder settingsUIBuilder;
    private SettingsMenuTemplate settingsMenuTemplate;
    private GraphicsSettingsManager graphicsManager;
    private SignalBus signalBus;



    [Inject]
    public void Construct(SettingsUIBuilder settingsUIBuilder, SettingsMenuTemplate settingsMenuTemplate,
        GraphicsSettingsManager graphicsManager, SignalBus signalBus)
    {
        this.signalBus = signalBus;
        this.graphicsManager = graphicsManager;
        this.settingsUIBuilder = settingsUIBuilder;
        this.settingsMenuTemplate = settingsMenuTemplate;
    }


    private void OnEnable()
    {
        if (applyButton != null)
            applyButton.onClick.AddListener(ApplySettings);
        if (returnButton != null)
            returnButton.onClick.AddListener(Hide);
        if (cancelButton != null)
            cancelButton.onClick.AddListener(Cancel);
    }


    private void OnDisable()
    {
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

        settingsUIBuilder.Build(settingsMenuTemplate);
    }


    private void ApplySettings()
    {
        graphicsManager.Apply();
        graphicsManager.Save();
    }


    private void Show()
    {
        settingsUIBuilder.Build(settingsMenuTemplate);
        gameObject.SetActive(true);
    }


    private void Hide()
    {
        settingsUIBuilder.Clear();
        gameObject.SetActive(false);
        graphicsManager.DiscardUnappliedChanges();
    }


    private void Cancel()
    {
        graphicsManager.DiscardUnappliedChanges();
        settingsUIBuilder.Build(settingsMenuTemplate);
    }
}
