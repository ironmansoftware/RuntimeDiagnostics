using System.Management.Automation;
using Microsoft.Diagnostics.Runtime;

namespace ProcessDiagnostics;

[Cmdlet("Get", "ClrObject")]
public class GetObjectCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
    public ClrRuntime Runtime { get; set; }

    [Parameter]
    public ulong MinSize { get; set; }

    [Parameter]
    public int Largest { get; set; }

    protected override void ProcessRecord()
    {
        var objects = Runtime.Heap.EnumerateObjects();

        if (MyInvocation.BoundParameters.ContainsKey("MinSize"))
        {
            objects = objects.Where(o => o.Size >= MinSize);
        }

        if (MyInvocation.BoundParameters.ContainsKey("Largest"))
        {
            objects = objects.OrderByDescending(o => o.Size).Take(Largest);
        }

        WriteObject(objects, true);
    }
}
