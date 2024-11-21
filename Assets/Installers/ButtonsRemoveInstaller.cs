using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonsRemoveInstaller : MonoInstaller
{
    [SerializeField] private Button _buttontRemove;

    public override void InstallBindings()
    {
        BindRemoveButton();
    }

    private void BindRemoveButton()
    {
        Container.Bind<Button>().WithId("RemoveButton").FromInstance(_buttontRemove).AsTransient();
    }
}