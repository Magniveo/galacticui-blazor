using Bunit;
using Xunit;

namespace Microsoft.FluentUI.AspNetCore.Components.Tests.Card;

public class GCardTests : TestBase
{
    [Fact]
    public void GCard_Default()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_NotAreaRestricted()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.Add(p => p.AreaRestricted, false);
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_NotAreaRestricted_AdditionalStyle()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.Add(p => p.AreaRestricted, false);
            parameters.Add(p => p.Style, "background-color: red");
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_AdditionalCssClass()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.Add(p => p.Class, "css-class");
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_AdditionalStyle()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.Add(p => p.Style, "background-color: red");
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_AdditionalParameter()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.AddUnmatched("additional-parameter-name", "additional-parameter-value");
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_AdditionalParameters()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.AddUnmatched("additional-parameter1-name", "additional-parameter1-value");
            parameters.AddUnmatched("additional-parameter2-name", "additional-parameter2-value");
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_Width()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.Add(p => p.Width, "400px");
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_Height()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.Add(p => p.Height, "400px");
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_WidthAndHeight()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.Add(p => p.Width, "400px");
            parameters.Add(p => p.Height, "400px");
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_Id()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.Add(p => p.Id, "customid");
            parameters.AddChildContent("childcontent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_MinimalStyle()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.Add(p => p.MinimalStyle, true);
            parameters.Add(p => p.Id, "customId");
            parameters.Add(p => p.Width, "400px");
            parameters.Add(p => p.Height, "400px");
            parameters.AddChildContent("ChildContent");
        });

        // Assert
        cut.Verify();
    }

    [Fact]
    public void GCard_MinimalStyle_NotAreaRestricted()
    {
        // Arrange && Act
        var cut = TestContext.RenderComponent<GCard>(parameters =>
        {
            parameters.Add(p => p.MinimalStyle, true);
            parameters.Add(p => p.Id, "customId");
            parameters.Add(p => p.Width, "400px");
            parameters.Add(p => p.Height, "400px");
            parameters.Add(p => p.AreaRestricted, false);
            parameters.AddChildContent("ChildContent");
        });

        // Assert
        cut.Verify();
    }
}
