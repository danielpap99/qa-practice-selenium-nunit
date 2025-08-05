using OpenQA.Selenium;

public interface ICheckoutPage
{
    void ClickOnCountrySuggestion(string country);
    void ClickPurchaseButton();
    void EnterCountry(string country);
    void TickTermsAndConditionsCheckbox();
}
