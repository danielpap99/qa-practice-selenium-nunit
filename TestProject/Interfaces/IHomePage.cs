public interface IHomePage
{
    string PageTitleText { get; }
    string LanguageText { get; }
    string SortText { get; }

    void NavigateToContactPage();

    void NavigateToHandTools();
    void SetGermanAsLanguage(string language);
    void SetSpanishAsLanguage(string language);
}
