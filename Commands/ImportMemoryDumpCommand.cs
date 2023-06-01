using System.Management.Automation;
using Microsoft.Diagnostics.Runtime;

namespace ProcessDiagnostics;

[Cmdlet("Import", "ClrMemoryDump")]
public class ImportMemoryDumpCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0)]
    public string Path { get; set; }

    protected override void ProcessRecord()
    {
        Path = base.GetUnresolvedProviderPathFromPSPath(Path);
        WriteObject(DataTarget.LoadDump(Path));
    }
}
