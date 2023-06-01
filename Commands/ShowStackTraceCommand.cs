using System.Management.Automation;
using System.Text;
using Microsoft.Diagnostics.Runtime;

namespace ProcessDiagnostics;

[Cmdlet("Show", "ClrStackTrace")]
public class ShowStackTraceCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
    public ClrThread Thread { get; set; }

    protected override void ProcessRecord()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"Managed thread ID: {Thread.ManagedThreadId}");
        stringBuilder.AppendLine();

        try
        {
            foreach (var frame in Thread.EnumerateStackTrace(false))
            {
                if (frame.Method != null)
                    stringBuilder.AppendLine(frame.Method.ToString());
            }
        }
        catch (Exception ex)
        {
            WriteError(new ErrorRecord(ex, "StackTraceFailed", ErrorCategory.NotSpecified, Thread));
        }


        WriteObject(stringBuilder.ToString());
    }
}
