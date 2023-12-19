using System.Diagnostics;
using System.Management.Automation;
using Microsoft.Diagnostics.Runtime;
using RuntimeDiagnostics;

namespace RuntimeDiagnostics;

[Cmdlet("Get", "ClrObject")]
public class GetObjectCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
    public ClrRuntime Runtime { get; set; }

    [Parameter()]
    public ulong MinSize { get; set; }

    [Parameter()]
    public int Largest { get; set; }
    [Parameter()]
    public SwitchParameter Statistics { get; set; }

    [Parameter()]
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
            if (MyInvocation.BoundParameters.ContainsKey("MinSize"))
            {
                objects = objects.Where(o => o.Size >= MinSize);
            }

            if (MyInvocation.BoundParameters.ContainsKey("Largest"))
            {
                objects = objects.OrderByDescending(o => o.Size).Take(Largest);
            }

            if (Statistics)
            {
                objects.Where(m => m.Type != null).GroupBy(m => m.Type.Name).Select(g => new TypeStat
                {
                    Type = g.Key,
                    Count = g.Count(),
                    Size = (ulong)g.Sum(m => (long)m.Size)
                }).OrderByDescending(t => t.Size).ToList().ForEach(t => WriteObject(t));
            }
            else
            {
                WriteObject(objects, true);
            }
        }


    }
}
