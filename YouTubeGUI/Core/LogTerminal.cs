using System;
using System.Diagnostics;
using Avalonia.Media;
using Avalonia.Threading;
using AvaloniaEdit;
using AvaloniaEdit.Highlighting;

namespace YouTubeGUI.Core
{
    public class LogTerminal
    {
        public TextEditor TextEditor
        {
            get => _textEditor;
            set
            {
                _textEditor = value;
                TextEditor.IsReadOnly = true;
                TextEditor.TextArea.TextView.Options.EnableHyperlinks = true;
                TextEditor.TextArea.TextView.Options.EnableEmailHyperlinks = true;
                TextEditor.TextArea.Caret.CaretBrush = Brushes.Transparent;
                TextEditor.TextArea.TextView.LineTransformers.Add(new RichTextColorizer(_richTextModel));
            }
        }

        private bool IsInitialized => _textEditor != null;
        private TextEditor? _textEditor;
        private readonly RichTextModel _richTextModel = new RichTextModel();
        
        private readonly Color _sqBracketColor = Colors.Gray;
        private readonly Color _mainForeColor = Colors.White;

        public void AppendLog(string? txt, LogType logType = LogType.Info, Exception? ex = null, StackTrace? stackTrace = null)
        {
            if (!IsInitialized) return;
            if (!Dispatcher.UIThread.CheckAccess())
            {
                Dispatcher.UIThread.InvokeAsync(() => AppendLog(txt, logType, ex, stackTrace), DispatcherPriority.Normal);
                return;
            }
            AppendDateTime();
            AppendLogType(logType);
            if (logType == LogType.Trace && stackTrace != null)
            {
#pragma warning disable 8602
                string callerName = stackTrace.GetFrame(3).GetMethod().DeclaringType.Name;
#pragma warning restore 8602
                if (callerName != string.Empty)
                {
                    Append(new RtbProperties() { Text = "[", Foreground = _sqBracketColor });
                    Append(new RtbProperties() { Text = callerName, Foreground = Colors.Chocolate });
                    Append(new RtbProperties() { Text = "]", Foreground = _sqBracketColor });
                }
            }
            Append(new RtbProperties() { Text = "> ", Foreground = _mainForeColor });
            Append(new RtbProperties() { Text = txt, Foreground = _mainForeColor, NewLine = logType != LogType.Exception });
            if (logType == LogType.Exception && ex != null)
            {
                TextEditor.AppendText(Environment.NewLine);
                Append(new RtbProperties() { Text = $"\t{ex.Message}", Foreground = Colors.Red, Background = Colors.Yellow, NewLine = true });
            }
        }

        private void AppendDateTime()
        {
            Append(new RtbProperties() { Text = "[", Foreground = _sqBracketColor });
            Append(new RtbProperties() { Text = DebugManager.GetDateTimeNow, Foreground = _mainForeColor });
            Append(new RtbProperties() { Text = "]", Foreground = _sqBracketColor });
        }
        private void AppendLogType(LogType logType)
        {
            Color colorToUse;
            switch (logType)
            {
                case LogType.Info:
                    colorToUse = Colors.GreenYellow;
                    break;
                case LogType.Trace:
                    colorToUse = Colors.DodgerBlue;
                    break;
                case LogType.Warning:
                    colorToUse = Colors.Yellow;
                    break;
                case LogType.Error:
                    colorToUse = Colors.Orange;
                    break;
                case LogType.Exception:
                    colorToUse = Colors.Red;
                    break;
                default:
                    colorToUse = _mainForeColor;
                    break;
            }
            Append(new RtbProperties() { Text = "[", Foreground = _sqBracketColor });
            Append(new RtbProperties() { Text = logType.ToString(), Foreground = colorToUse });
            Append(new RtbProperties() { Text = "]", Foreground = _sqBracketColor });
        }

        private void Append(RtbProperties rtbProperties)
        {
            if (!IsInitialized) return;
            TextEditor.AppendText(rtbProperties.Text + (rtbProperties.NewLine ? Environment.NewLine : string.Empty));
            if (rtbProperties.Text != null)
            {
                int offset = TextEditor.Text.Length - rtbProperties.Text.Length;
                _richTextModel.SetForeground(offset, rtbProperties.Text.Length, new SimpleHighlightingBrush(rtbProperties.Foreground));
                _richTextModel.SetBackground(offset, rtbProperties.Text.Length, new SimpleHighlightingBrush(rtbProperties.Background));
                _richTextModel.SetFontStyle(offset, rtbProperties.Text.Length, rtbProperties.FontStyle);
                _richTextModel.SetFontWeight(offset, rtbProperties.Text.Length, rtbProperties.FontWeight);
            }
        }

        private class RtbProperties
        {
            public string? Text = string.Empty;
            public Color Foreground;
            public Color Background;
            public FontStyle FontStyle = FontStyle.Normal;
            public FontWeight FontWeight = FontWeight.Normal;
            public bool NewLine = false;
        }
    }
}