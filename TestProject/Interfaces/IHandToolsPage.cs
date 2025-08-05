using OpenQA.Selenium;

public interface IHandToolsPage
{
    string ProductNameText { get; }
    string UnitPriceText { get; }
    bool AlertDisplayed { get; }
    string AlertText { get; }
    string CartQuantityText { get; }

    void AddProductToCart();
    void ClickOnPliers();
}