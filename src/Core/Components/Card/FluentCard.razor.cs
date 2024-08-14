using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Utilities;

namespace Microsoft.FluentUI.AspNetCore.Components;

public partial class FluentCard
{
    protected string? StyleValue => new StyleBuilder(Style)
        .AddStyle("width", Width, !string.IsNullOrEmpty(Width))
        .AddStyle("height", Height, !string.IsNullOrEmpty(Height))
        .AddStyle("content-visibility", "visible", !AreaRestricted)
        .AddStyle("contain", "none", !AreaRestricted)
        .Build();
/*
 * width: '120px';
   height: '120px';
   display: 'flex';
   alignItems: 'center';
   justifyContent: 'center';
 */
    protected string? ClassValue => new CssBuilder(Class)
        .AddClass("g-card")
        .AddClass("g-card_view_outlined")
        .AddClass(GetCssClassTheme())
        .AddClass("g-box")
        .AddClass("g-card_type_container")
        .AddClass(GetCssClassSize())
        .Build();
    [Parameter]
    public string Theme { get; set; } = "normal";
    [Parameter]
    public string Size { get; set; } = "m";

    private string GetCssClassSize()
    {
        return $"g-card_size_{Size}";
    }

    private string GetCssClassTheme()
    {
        return $"g-card_theme_{Theme}";
        switch (Theme)
        {
            case "normal" :return  "g-card_theme_normal";
            case "info" :return  "g-card_theme_info";
            case "success" :return  "g-card_theme_success";
            case "warning" :return  "g-card_theme_warning";
            case "danger" :return  "g-card_theme_danger";
        }
        return  "g-card_theme_normal";
    }

    /// <summary>
    /// By default, content in the card is restricted to the area of the card itself.
    /// If you want content to be able to overflow the card, set this property to false.
    /// </summary>
    [Parameter]
    public bool AreaRestricted { get; set; } = true;

    /// <summary>
    /// Gets or sets the width of the card. Must be a valid CSS measurement.
    /// </summary>
    [Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the card. Must be a valid CSS measurement.
    /// </summary>
    [Parameter]
    public string? Height { get; set; }

    [Parameter]
    public bool MinimalStyle { get; set; } = false;

    /// <summary>
    /// Gets or sets the content to be rendered inside the component.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
