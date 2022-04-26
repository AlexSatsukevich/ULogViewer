using CarinaStudio.Threading;
using System;
using System.ComponentModel;

namespace CarinaStudio.ULogViewer.ViewModels.Analysis;

/// <summary>
/// Analysis result of <see cref="DisplayableLog"/>.
/// </summary>
class DisplayableLogAnalysisResult : BaseApplicationObject<IULogViewerApplication>, INotifyPropertyChanged
{
    // Static fields.
    static readonly long DefaultMemorySize = (4 * IntPtr.Size) // Appliation, message, Analyzer, PropertyChanged
        + 4; // isMessageValid


    // Fields.
    bool isMessageValid;
    string? message;


    /// <summary>
    /// Initialize new <see cref="DisplayableLogAnalysisResult"/> instance.
    /// </summary>
    /// <param name="analyzer"><see cref="IDisplayableLogAnalyzer"/> which generates this result.</param>
    /// <param name="log"><see cref="DisplayableLog"/> which relates to this result.</param>
    public DisplayableLogAnalysisResult(IDisplayableLogAnalyzer<DisplayableLogAnalysisResult> analyzer, DisplayableLog? log) : base(analyzer.Application)
    {
        this.Analyzer = analyzer;
        this.Log = log;
    }


    /// <summary>
    /// Get <see cref="IDisplayableLogAnalyzer"/> which generates this result.
    /// </summary>
    public IDisplayableLogAnalyzer<DisplayableLogAnalysisResult> Analyzer { get; }


    /// <summary>
    /// Invalidate and update message of result.
    /// </summary>
    protected void InvalidateMessage()
    {
        this.VerifyAccess();
        if (this.isMessageValid)
        {
            var message = this.OnUpdateMessage();
            if (this.message != message)
            {
                this.message = message;
                this.OnPropertyChanged(nameof(Message));
            }
        }
    }
    

    /// <summary>
    /// Get <see cref="DisplayableLog"/> which relates to this result.
    /// </summary>
    public DisplayableLog? Log { get; }


    /// <summary>
    /// Get memory size of the result instance in bytes.
    /// </summary>
    public virtual long MemorySize { get => DefaultMemorySize; }


    /// <summary>
    /// Get message of result.
    /// </summary>
    public string? Message
    { 
        get
        {
            if (!this.CheckAccess())
                return this.message;
            if (!this.isMessageValid)
            {
                this.message = this.OnUpdateMessage();
                this.isMessageValid = true;
            }
            return this.message;
        }
    }


    /// <summary>
    /// Raise <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    protected virtual void OnPropertyChanged(string propertyName) => 
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    

    /// <summary>
    /// Called to update message of result.
    /// </summary>
    /// <returns>Message of result.</returns>
    protected virtual string? OnUpdateMessage() => null;


    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;


    /// <inheritdoc/>
    public override string ToString() =>
        $"{this.GetType().Name}: {this.message}";
}