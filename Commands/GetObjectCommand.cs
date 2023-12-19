using System.Diagnostics;
using System.Management.Automation;
using Microsoft.Diagnostics.Runtime;
using RuntimeDiagnostics;

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

    [Parameter]
    public string Type { get; set; }

    [Parameter]
    public SwitchParameter Fields { get; set; }

    protected override void ProcessRecord()
    {
        var objects = Runtime.Heap.EnumerateObjects();

        if (MyInvocation.BoundParameters.ContainsKey("Type"))
        {
            objects = objects.Where(o => o.Type?.Name?.Equals(Type, StringComparison.OrdinalIgnoreCase) == true);
        }

        if (MyInvocation.BoundParameters.ContainsKey("MinSize"))
        {
            objects = objects.Where(o => o.Size >= MinSize);
        }

        if (MyInvocation.BoundParameters.ContainsKey("Largest"))
        {
            objects = objects.OrderByDescending(o => o.Size).Take(Largest);
        }

        if (Fields)
        {
            foreach (var obj in objects)
            {
                var psobject = new ExtendedClrObject(obj);
                WriteObject(psobject);
            }
        }
        else
        {
            WriteObject(objects, true);
        }
    }
}
