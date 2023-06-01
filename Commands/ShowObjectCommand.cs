using System.Management.Automation;
using System.Text;
using Microsoft.Diagnostics.Runtime;

namespace ProcessDiagnostics;

[Cmdlet("Show", "ClrObject")]
public class ShowObjectCommand : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
    public ClrObject Object { get; set; }

    protected override void ProcessRecord()
    {
        var stringBuilder = new StringBuilder();

        if (Object.Type.Name == "System.String")
        {
            WriteObject(Object.AsString());
        }
        else
        {
            // Object.Type.Fields.ToList().ForEach(field =>
            // {
            //     #stringBuilder.AppendLine($"{field.Name}: {Object.Read}");
            // });
        }




        WriteObject(stringBuilder.ToString());
    }
}
