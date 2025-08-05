public interface IPracticePage
{
    string JavaAlertText { get; }
    string CountryFieldText { get; }
    bool PhotoSlideComponentVisible { get; }
    string CartProductCount { get; }
    string CartContentText { get; }
    string SuccessFulPaymentText { get; }

    void AcceptJavaAlert();
    void CancelJavaAlert();
    void ClickCheckoutButton();
    void ClickConfirmName();
    void ClickContinueShoppingButton();
    void ClickNewTabButton();
    void ClickOnCountrySuggestion(string country);
    void ClickPurchaseButton();
    void ClickShoppingCart();
    void EnterCountry(string country);
    void EnterFirstThreeCharactersOfCountry_AutoSuggestive(string country);
    void HoverOnMouseHoverButton();
    void InputName(string name);
    void OpenInterviewLinkInNewTab();
    void ScrollToMouseHoverButton();
    void ScrollToRahulFrame();
    void TickTermsAndConditionsCheckbox();
}
