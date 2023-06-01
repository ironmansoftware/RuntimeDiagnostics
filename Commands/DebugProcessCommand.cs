using System.Management.Automation;
using Microsoft.Diagnostics.Runtime;

namespace ProcessDiagnostics;

[Cmdlet("Debug", "ClrProcess")]
public class DebugProcessCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ParameterSetName = "ProcessId")]
    public int ProcessId { get; set; }
    [Parameter]
    public SwitchParameter Suspend { get; set; }
    protected override void ProcessRecord()
    {
        WriteObject(DataTarget.AttachToProcess(ProcessId, suspend: Suspend));
    }
}
