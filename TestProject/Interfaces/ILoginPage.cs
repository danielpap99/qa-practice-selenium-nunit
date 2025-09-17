public interface ILoginPage
{
    string InvalidCodeMessageText { get; }
    string LoginComponentText { get; }
    string LoginFormText { get; }

    void ClickVerifyButton();
    void EnterEmail(string email);
    void EnterVerificationCode(string digit0, string digit1, string digit2, string digit3, string digit4, string digit5);
    bool IsVerifyButtonDisplayed();
    void UnsuccessfulLogin(string username, string password);
    void SuccessfulLogin(string username, string password);
    void UntickRememberMeButton();
    void WaitForInvalidCodeMessageToAppear();
}

