using FluentAssertions;
using Xunit;

namespace Pek.Markdig.HighlightJs.Tests
{
    public class HighlightJsEngineTests
    {
        [Fact]
        public void Can_render_code()
        {
            var engine = new HighlightJsEngine();
            var result = engine.Run("js", "console.log('test')");
            result.Should().Be("<span class=\"hljs-built_in\">console</span>.log(<span class=\"hljs-string\">'test'</span>)");
        }
    }
}