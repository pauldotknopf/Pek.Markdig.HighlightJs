using System;
using FluentAssertions;
using Markdig;
using Moq;
using Xunit;

namespace Pek.Markdig.HighlightJs.Tests
{
    public class RenderTests
    {
        [Fact]
        public void Can_render_code()
        {
            var highlightJsEngine = new Mock<IHighlightJsEngine>();
            highlightJsEngine.Setup(x => x.Run(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Func<string, string, string>((lang, c) => $"#{c}#"));
            var pipeline = new MarkdownPipelineBuilder().UseHighlightJs(highlightJsEngine.Object).Build();
            var code = @"function test() {
}";
            var input = $@"```js
{code}
```";
            var result = Markdown.ToHtml(input, pipeline);
            
            highlightJsEngine.Verify(x => x.Run("js", code));
            result.Should().Contain($"<pre><code class=\"language-js hljs\">#{code}#</code></pre>");
        }

        [Fact]
        public void Use_default_render_with_no_language()
        {
            var highlightJsEngine = new Mock<IHighlightJsEngine>();
            highlightJsEngine.Setup(x => x.Run(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Func<string, string, string>((lang, c) => $"#{c}#"));
            var pipeline = new MarkdownPipelineBuilder().UseHighlightJs(highlightJsEngine.Object).Build();
            var code = @"function test() {
}";
            var input = $@"```
{code}
```";
            var result = Markdown.ToHtml(input, pipeline);
            
            highlightJsEngine.Verify(x => x.Run("js", code), Times.Never);
            result.Should().Contain($"<pre><code>{code}\n</code></pre>");
        }
    }
}