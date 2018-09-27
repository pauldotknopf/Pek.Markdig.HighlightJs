# Highlight.js extension for Markdig.

[![Pek.Markdig.HighlightJs](https://img.shields.io/nuget/v/Pek.Markdig.HighlightJs.svg?style=flat&label=Pek.Markdig.HighlightJs)](http://www.nuget.org/packages/Pek.Markdig.HighlightJs/)

An extensions that adds code highlighting support to [Markdig](https://github.com/lunet-io/markdig), using the [highlight.js](https://highlightjs.org/) library.

# Example

**Before**

```
public class TestClass
{
    public string Method()
    {
        return "yo!";
    }
}
```

**After**

```cs
public class TestClass
{
    public string Method()
    {
        return "yo!";
    }
}
```

# Installation

```bash
dotnet add package Pek.Markdig.HighlightJs
```

```csharp
var pipeline = new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .UseHightlightJs()
    .Build();
```

This extension only provides html with the appropriate classes. You must manually ensure that the appropriate css style-sheet is loaded in your web page. You can find all the available styles [here](https://highlightjs.org/static/demo/) and [here](https://github.com/highlightjs/highlight.js/tree/master/src/styles).