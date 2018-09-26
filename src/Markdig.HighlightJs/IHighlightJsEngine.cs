namespace Markdig.HighlightJs
{
    public interface IHighlightJsEngine
    {
        string Run(string language, string code);
    }
}