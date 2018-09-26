using FluentAssertions;
using Xunit;

namespace Markdig.HighlightJs.Tests
{
    public class RenderTests
    {
        [Fact]
        public void Can_render_code()
        {
            var input = @"```jss
function test() {
}
```";
            var result = Markdig.Markdown.ToHtml(input, new MarkdownPipelineBuilder().UseHighlightJs().Build());
            result.Should().Be("");
        }
    }
}