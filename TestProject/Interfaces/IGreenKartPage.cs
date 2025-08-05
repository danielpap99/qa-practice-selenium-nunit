using OpenQA.Selenium;

public interface IGreenKartPage
{
    void ClickSortVegFruit();
    void GetAndSortArrayListofProduce();
    void GetArrayListOfProduce();
    void SelectPageSize_StaticDropdown(string size);
}
