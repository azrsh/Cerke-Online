using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Azarashi.CerkeOnline.Presentation.Presenter.Inputs;

namespace Azarashi.CerkeOnline.Presentation.Presenter.Commands
{
    [RequireComponent(typeof(IInputEventProvider))]
    public class CommandInputPresenter : MonoBehaviour
    {
        [SerializeField] InputField commandInputField = default;

        readonly CommandFactory factory = new CommandFactory();

        void Start()
        {
            commandInputField.gameObject.SetActive(false);
            commandInputField.OnEndEditAsObservable().TakeUntilDestroy(this).Subscribe(OnEndEdit);
            IInputEventProvider inputEventProvider = GetComponent<IInputEventProvider>();
            inputEventProvider.OnCommandButton.TakeUntilDestroy(this).Subscribe(OnCommandButton);
        }

        void OnCommandButton(Unit unit)
        {
            ActivateInputField();
        }
        
        void OnEndEdit(string value)
        {
            commandInputField.gameObject.SetActive(false);

            ICommand command = factory.CreateInstance(value);
            if (command == null)
            {
                Application.GameController.Instance.SystemLogger.Log("Invalid Command");
                return;
            }

            CommandResult result = command.Execute();
            Debug.Log("Command Executed : " + result?.message);
        }

        void ActivateInputField()
        {
            commandInputField.gameObject.SetActive(true);
            commandInputField.ActivateInputField();
        }
    }
}
