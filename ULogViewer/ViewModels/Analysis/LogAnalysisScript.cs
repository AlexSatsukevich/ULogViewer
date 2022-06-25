using CarinaStudio.ULogViewer.Scripting;
using System.Text.Json;

namespace CarinaStudio.ULogViewer.ViewModels.Analysis;

/// <summary>
/// Script for log analysis.
/// </summary>
class LogAnalysisScript : Script<ILogAnalysisScriptContext>
{
    /// <summary>
    /// Initialize new <see cref="LogAnalysisScript"/> instance.
    /// </summary>
    /// <param name="language">Language.</param>
    /// <param name="source">Source.</param>
    public LogAnalysisScript(ScriptLanguage language, string source) : base(language, source)
    { }


    /// <summary>
    /// Load script from JSON format data.
    /// </summary>
    /// <param name="json">JSON data.</param>
    /// <returns>Loaded script.</returns>
    public static LogAnalysisScript Load(JsonElement json) =>
        LogAnalysisScript.Load<LogAnalysisScript>(json);
}


/// <summary>
/// Context of log analysis script.
/// </summary>
public interface ILogAnalysisScriptContext : IScriptContext
{
    /// <summary>
    /// Add analysis result.
    /// </summary>
    /// <param name="type">Type of result.</param>
    /// <param name="message">Message.</param>
    /// <param name="log">Related log.</param>
    void AddAnalysisResult(DisplayableLogAnalysisResultType type, string message, ILogAnalysisScriptLog? log);


    /// <summary>
    /// Add analysis result.
    /// </summary>
    /// <param name="type">Type of result.</param>
    /// <param name="message">Message.</param>
    /// <param name="beginningLog">Beginning log.</param>
    /// <param name="endingLog">Ending log.</param>
    void AddAnalysisResult(DisplayableLogAnalysisResultType type, string message, ILogAnalysisScriptLog beginningLog, ILogAnalysisScriptLog endingLog);


    /// <summary>
    /// Get current log.
    /// </summary>
    ILogAnalysisScriptLog Log { get; }
}


/// <summary>
/// Interface for <see cref="LogAnalysisScript"/> to access log.
/// </summary>
public interface ILogAnalysisScriptLog
{
    /// <summary>
    /// Get property of log.
    /// </summary>
    /// <param name="name">Name of property.</param>
    /// <param name="defaultValue">Default value if there is no value got from property.</param>
    /// <returns>Value of log property.</returns>
    T? GetProperty<T>(string name, T? defaultValue = default);


    /// <summary>
    /// Get text of log.
    /// </summary>
    string Text { get; }
}


/// <summary>
/// Empty implementation of <see cref="ILogAnalysisScriptLog"/>.
/// </summary>
class EmptyLogAnalysisScriptLog : ILogAnalysisScriptLog
{
    /// <summary>
    /// Default instance.
    /// </summary>
    public static readonly ILogAnalysisScriptLog Default = new EmptyLogAnalysisScriptLog();


    // Constructor.
    EmptyLogAnalysisScriptLog()
    { }


    /// <inheritdoc/>
    public T? GetProperty<T>(string name, T? defaultValue = default) =>
        defaultValue;
    

    /// <inheritdoc/>
    public string Text => "";
}