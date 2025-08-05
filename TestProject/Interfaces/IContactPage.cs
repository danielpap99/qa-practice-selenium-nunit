public interface IContactPage
{
    string SuccessfulAlertText { get; }
    string MinimumCharacterAlertText { get; }
    bool IsMinimumCharacterAlertVisible { get; }

    void ClickSend();
    void InputEmail(string email);
    void InputFirstName(string firstName);
    void InputLastName(string lastName);
    void InputMessage(string message);
    void InputMessageOver50Characters();
    bool IsCharacterAlertVisible();
    void SelectSubject(string subject);
    void WaitForSuccessfulAlertToAppear();
}
