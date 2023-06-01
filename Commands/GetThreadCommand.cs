using System.Management.Automation;
using Microsoft.Diagnostics.Runtime;

namespace ProcessDiagnostics;

[Cmdlet("Get", "ClrThread")]
public class GetThreadCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
    public ClrRuntime Runtime { get; set; }

    protected override void ProcessRecord()
    {
        WriteObject(Runtime.Threads, true);
    }
}
