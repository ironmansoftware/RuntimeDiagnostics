using System.Management.Automation;
using Microsoft.Diagnostics.Runtime;

namespace ProcessDiagnostics;

[Cmdlet("Mount", "ClrRuntime")]
public class MountRuntimeCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "ClrVersion")]
    public ClrInfo ClrVersion { get; set; }
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "DataTarget")]
    public DataTarget DataTarget { get; set; }
    [Parameter()]
    public string DacLocation { get; set; }

    protected override void ProcessRecord()
    {
        if (ParameterSetName == "DataTarget")
        {
            ClrVersion = DataTarget.ClrVersions[0];
        }

        if (string.IsNullOrEmpty(DacLocation))
        {
            WriteObject(ClrVersion.CreateRuntime());
        }
        else
        {
            WriteObject(ClrVersion.CreateRuntime(DacLocation));
        }
    }
}
