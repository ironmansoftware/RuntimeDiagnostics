using System.Management.Automation;
using Microsoft.Diagnostics.Runtime;

namespace ProcessDiagnostics;

[Cmdlet("Get", "ClrVersion")]
public class GetClrVersionCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
    public DataTarget DataTarget { get; set; }

    protected override void ProcessRecord()
    {
        WriteObject(DataTarget.ClrVersions, true);
    }
}
