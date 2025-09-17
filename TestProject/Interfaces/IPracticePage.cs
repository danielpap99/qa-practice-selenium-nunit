﻿public interface IPracticePage
{
    string JavaAlertText { get; }
    string CountryFieldText { get; }
    bool PhotoSlideComponentVisible { get; }
    string CartProductCount { get; }
    string CartContentText { get; }
    string SuccessFulPaymentText { get; }
    bool CheckoutVisible { get; }

    void AcceptJavaAlert();
    void CancelJavaAlert();
    void ClickCheckout();
    void ClickCheckoutButton();
    void ClickConfirmName();
    void ClickContinueShoppingButton();
    void ClickNewTabButton();
    void ClickShoppingCart();
    void EnterFirstThreeCharactersOfCountry_AutoSuggestive(string country);
    void HoverOnMouseHoverButton();
    void InputName(string name);
    void OpenInterviewLinkInNewTab();
    void ScrollToMouseHoverButton();
    void ScrollToRahulFrame();
}
