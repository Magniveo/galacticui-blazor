using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using Microsoft.FluentUI.AspNetCore.Components.Utilities;
using Microsoft.JSInterop;

namespace Microsoft.FluentUI.AspNetCore.Components;
public partial class FluentButton : FluentComponentBase, IAsyncDisposable
{

    private const string JAVASCRIPT_FILE = "./_content/Microsoft.FluentUI.AspNetCore.Components/Components/Button/FluentButton.razor.js";

    private readonly RenderFragment _renderButton;

    /// <summary />
    [Inject]
    private LibraryConfiguration LibraryConfiguration { get; set; } = default!;

    /// <summary />
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    /// <summary />
    private IJSObjectReference? _jsModule { get; set; }

    private bool LoadingOverlay => Loading && IconStart == null && IconEnd == null;

    /// <summary>
    /// Optional CSS class names. If given, these will be included in the class attribute of the component.
    /// </summary>
    [Parameter]
    public override string? Class { get; set; } = "g-button";
    /// <summary>
    /// Determines if the element should receive document focus on page load.
    /// </summary>
    [Parameter]
    public bool? Autofocus { get; set; }

    /// <summary>
    /// Gets or sets the id of a form to associate the element to.
    /// </summary>
    [Parameter]
    public string? FormId { get; set; }

    /// <summary>
    /// See <see href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/button">button</see> element for more details.
    /// </summary>
    [Parameter]
    public string? Action { get; set; }

    /// <summary>
    /// See <see href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/button">button</see> element for more details.
    /// </summary>
    [Parameter]
    public string? Enctype { get; set; }

    /// <summary>
    /// See <see href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/button">button</see> element for more details.
    /// </summary>
    [Parameter]
    public string? Method { get; set; }

    /// <summary>
    /// See <see href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/button">button</see> element for more details.
    /// </summary>
    [Parameter]
    public bool? NoValidate { get; set; }

    /// <summary>
    /// See <see href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/button">button</see> element for more details.
    /// Possible values: "_self" | "_blank" | "_parent" | "_top"
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// Gets or sets the button type. See <see cref="ButtonType"/> for more details.
    /// Default is ButtonType.Button.
    /// </summary>
    private ButtonType? _type;
    [Parameter]
    public ButtonType? Type {
        get
        {
            return _type?? ButtonType.Normal;
        }
        set
        {
            _type = value;
            setClassViaType(_type);
        }
    }

    /// <summary>
    /// Gets or sets the value of the element.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the element's current value.
    /// </summary>
    [Parameter]
    public string? CurrentValue { get; set; }

    /// <summary>
    /// Disables the form control, ensuring it doesn't participate in form submission.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the name of the element.
    /// Allows access by name from the associated form.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the element needs to have a value.
    /// </summary>
    [Parameter]
    public bool Required { get; set; }

    /// <summary>
    /// Gets or sets the visual appearance. See <seealso cref="AspNetCore.Components.Appearance"/>
    /// Defaults to <seealso cref="AspNetCore.Components.Appearance.Neutral"/>
    /// </summary>
    [Parameter]
    public Appearance? Appearance { get; set; } = AspNetCore.Components.Appearance.Neutral;

    /// <summary>
    /// Gets or sets the background color of this button (overrides the <see cref="Appearance"/> property).
    /// Set the value "rgba(0, 0, 0, 0)" to display a transparent button.
    /// </summary>
    [Parameter]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the color of the font (overrides the <see cref="Appearance"/> property).
    /// </summary>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// Display a progress ring and disable the button.
    /// </summary>
    [Parameter]
    public bool Loading { get; set; } = false;

    /// <summary>
    /// Gets or sets the <see cref="Icon"/> displayed at the start of button content.
    /// </summary>
    [Parameter]
    public Icon? IconStart { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Icon"/> displayed at the end of button content.
    /// </summary>
    [Parameter]
    public Icon? IconEnd { get; set; }

    /// <summary>
    /// Gets or sets the title of the button.
    /// The text usually displayed in a 'tooltip' popup when the mouse is over the button.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }
    /// <summary>
    /// Gets or sets the content to be rendered inside the component.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Command executed when the user clicks on the button.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }
    [Parameter]public EventCallback<MouseEventArgs> OnMouseEnter { get; set; }
    [Parameter]public EventCallback<MouseEventArgs> OnMouseLeave { get; set; }
    [Parameter]public EventCallback<FocusEventArgs> OnFocus { get; set; }
    [Parameter]public EventCallback<FocusEventArgs> OnBlur { get; set; }
    /// <summary>
    /// size	Sets button size xs s m l xl
    /// </summary>
    private ButtonSize? _size;
    [Parameter]
    public ButtonSize? Size
    {
        get { return _size;}
        set
        {
            _size = value;
            setSize(_size);
        }
    }

    private ButtonPin? _pin;
    /// <summary>
    /// pin	Sets button edges style
    /// </summary>
    [Parameter]
    public ButtonPin? Pin {
        get
        {
            return _pin;
        }
        set
        {
            _pin = value;
            setClassViaPin(_pin);
        }
    }
    protected override void OnParametersSet()
    {
        string[] values = ["_self", "_blank", "_parent", "_top"];
        if (!string.IsNullOrEmpty(Target) && !values.Contains(Target))
        {
            throw new ArgumentException("Target must be one of the following values: _self, _blank, _parent, _top");
        }
        if (Appearance == AspNetCore.Components.Appearance.Filled)
        {
            throw new ArgumentException("Appearance.Filled is not supported for FluentButton");
        }
        if (Appearance == AspNetCore.Components.Appearance.Hypertext)
        {
            throw new ArgumentException("Appearance.Hypertext is not supported for FluentButton");
        }
    }

    /// <summary />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && Id is not null && Type != ButtonType.Button)
        {
            _jsModule ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import", JAVASCRIPT_FILE.FormatCollocatedUrl(LibraryConfiguration));
            await _jsModule.InvokeVoidAsync("updateProxy", Id);
        }
    }

    /// <summary />
    protected virtual MarkupString CustomStyle => new InlineStyleBuilder()
        //.AddStyle($"#{Id}::part(g-button)","background","")
        //.AddStyle($"#{Id}::part(g-button)","color","")
        //.AddStyle($"#{Id}::part(g-button:hover)","","")
        //.AddStyle($"#{Id}::part(g-button)", "background", $"padding-box linear-gradient({BackgroundColor}, {BackgroundColor}), border-box {BackgroundColor}", when: !string.IsNullOrEmpty(BackgroundColor))
        //.AddStyle($"#{Id}::part(g-button)", "color", $"{Color}", when: !string.IsNullOrEmpty(Color))
        //.AddStyle($"#{Id}::part(g-button:hover)", "opacity", "0.8", when: !string.IsNullOrEmpty(Color) || !string.IsNullOrEmpty(BackgroundColor))
        .BuildMarkupString();

    /// <summary>
    /// Constructs an instance of <see cref="FluentButton"/>.
    /// </summary>
    public FluentButton()
    {
        _renderButton = RenderButton;
    }

    /// <summary />
    protected override void OnInitialized()
    {
        if (string.IsNullOrEmpty(Id) && (!string.IsNullOrEmpty(BackgroundColor) || !string.IsNullOrEmpty(Color)))
        {
            Id = Identifier.NewId();
        }
    }

    /// <summary />
    protected async Task OnClickHandlerAsync(MouseEventArgs e)
    {
        if (!Disabled && OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(e);
        }

        await Task.CompletedTask;
    }

    public void SetDisabled(bool disabled)
    {
        Disabled = disabled;
        StateHasChanged();
    }

    private string RingStyle(Icon icon)
    {
        var size = icon.Width - 4;
        var inverse = Appearance == AspNetCore.Components.Appearance.Accent ? " filter: invert(1);" : string.Empty;

        return $"width: {size}px; height: {size}px;{inverse}";
    }
    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_jsModule is not null)
            {
                await _jsModule.DisposeAsync();
            }
        }
        catch (Exception ex) when (ex is JSDisconnectedException ||
                                   ex is OperationCanceledException)
        {
            // The JSRuntime side may routinely be gone already if the reason we're disposing is that
            // the client disconnected. This is not an error.
        }
    }

    private Task setSize(ButtonSize? size)
    {
        switch (size)
        {
            case ButtonSize.Xl:
                Class += " g-button_size_xl";
                break;
            case ButtonSize.L:
                Class += " g-button_size_l";
                break;
            case ButtonSize.M:
                Class += " g-button_size_m";
                break;
            case ButtonSize.S:
                Class += " g-button_size_s";
                break;
            case ButtonSize.Xs:
                Class += " g-button_size_xs";
                break;
        }

        return Task.CompletedTask;
    }

    private Task setClassViaType(ButtonType? value)
    {

        switch (value)
        {
            case ButtonType.Normal:
                {
                    Class += $" g-button_view_normal";
                    break;
                }
            case ButtonType.Action:
                {
                    Class += $" g-button_view_action";
                    break;
                }
            case ButtonType.Outlined:
                {
                    Class += $" g-button_view_outlined";
                    break;
                }
            case ButtonType.OutlinedInfo:
                {
                    Class += $" g-button_view_outlined-info";
                    break;
                }
                case ButtonType.OutlinedSuccess:
                {
                    Class += $" g-button_view_outlined-info";
                    break;
                }
                case ButtonType.OutlinedWarning:
                {
                    Class += $" g-button_view_outlined-info";
                    break;
                }
                case ButtonType.OutlinedDanger:
                {
                    Class += $" g-button_view_outlined-danger";
                    break;
                }
                case ButtonType.OutlinedUtility:
                {
                    Class += $" g-button_view_outlined-utility";
                    break;
                }
                case ButtonType.OutlinedAction:
                {
                    Class += $" g-button_view_outlined-action";
                    break;
                }
                case ButtonType.Raised:
                {
                    Class += $" g-button_view_raised";
                    break;
                }
                case ButtonType.Flat:
                {
                    Class += $" g-button_view_flat";
                    break;
                }
                case ButtonType.FlatSecondary:
                {
                    Class += $" g-button_view_flat-secondary";
                    break;
                }
                case ButtonType.FlatInfo:
                {
                    Class += $" g-button_view_flat-info";
                    break;
                }
                case ButtonType.FlatSuccess:
                {
                    Class += $" g-button_view_flat-success";
                    break;
                }
                case ButtonType.FlatWarning:
                {
                    Class += $" g-button_view_outlined-warning";
                    break;
                }
                case ButtonType.FlatDanger:
                {
                    Class += $" g-button_view_flat-danger";
                    break;
                }
                case ButtonType.FlatUtility:
                {
                    Class += $" g-button_view_flat-utility";
                    break;
                }
                case ButtonType.FlatAction:
                {
                    Class += $" g-button_view_flat-action";
                    break;
                }
                case ButtonType.NormalContrast:
                {
                    Class += $" g-button_view_normal-contrast";
                    break;
                }
                case ButtonType.OutlinedContrast:
                {
                    Class += $" g-button_view_outlined-contrast";
                    break;
                }
                case ButtonType.FlatContrast:
                {
                    Class += $" g-button_view_flat-contrast";
                    break;
                }
        }
        return Task.CompletedTask;
    }

    private Task setClassViaPin(ButtonPin? pin)
    {
        switch (pin)
        {
                case ButtonPin.RoundRound:
                    Class += " g-button_pin_round-round";
                    break;
                case ButtonPin.BrickBrick:
                    Class += " g-button_pin_brick-brick";break;
                case ButtonPin.ClearClear:
                    Class += " g-button_pin_clear-clear";break;
                case ButtonPin.CircleCircle:
                    Class += " g-button_pin_cirlce-circle";break;
                case ButtonPin.RoundBrick:
                    Class += " g-button_pin_round-brick";
                    break;
                case ButtonPin.BrickRound:
                    Class += " g-button_pin_brick-round";
                    break;
                case ButtonPin.RoundClear:
                    Class += " g-button_pin_round-clear";break;
                case ButtonPin.ClearRound:
                    Class += " g-button_pin_clear-round";break;
                case ButtonPin.BrickClear: Class += " g-button_pin_brick-clear";break;
                case ButtonPin.ClearBrick: Class += " g-button_pin_clear-brick";break;
                case ButtonPin.CircleBrick: Class += " g-button_pin_circle-brick";break;
                case ButtonPin.BrickCircle: Class += " g-button_pin_brick-circle";break;
                case ButtonPin.CircleClear: Class += " g-button_pin_circle-clear";break;
                case ButtonPin.ClearCircle: Class += " g-button_pin_clear-circle";break;
        }

        return Task.CompletedTask;
    }

    public enum ButtonSize
    {
        Xs,
        S,
        M,
        L,
        Xl
    }
    public enum ButtonPin
    {
        RoundRound,
        BrickBrick,
        ClearClear,
        CircleCircle,
        RoundBrick,
        BrickRound,
        RoundClear,
        ClearRound,
        BrickClear,
        ClearBrick,
        CircleBrick,
        BrickCircle,
        CircleClear,
        ClearCircle
    }
}
