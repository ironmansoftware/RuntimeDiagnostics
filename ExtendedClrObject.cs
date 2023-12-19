using System.Management.Automation;
using Microsoft.Diagnostics.Runtime;

namespace RuntimeDiagnostics
{
    public class ExtendedClrObject : PSObject
    {
        public Dictionary<string, Lazy<ExtendedClrObject>> Fields { get; private set; } = new Dictionary<string, Lazy<ExtendedClrObject>>();

        public ExtendedClrObject(ClrObject baseObject) : base(baseObject)
        {
            Properties.Add(new PSNoteProperty("Fields", Fields));

            baseObject.Type?.Fields.ToList().ForEach(field =>
            {
                if (field.Name == null) return;

                if (field.IsValueType)
                {
                    var value = baseObject.ReadValueTypeField(field.Name);
                    Fields.Add(field.Name, new Lazy<ExtendedClrObject>(() => new ExtendedClrObject(value)));
                }
                else
                {
                    var value = baseObject.ReadObjectField(field.Name);
                    Fields.Add(field.Name, new Lazy<ExtendedClrObject>(() => new ExtendedClrObject(value)));
                }
            });
        }

        public ExtendedClrObject(ClrValueType baseObject) : base(baseObject)
        {

        }


    }
}