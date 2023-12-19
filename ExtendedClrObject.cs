using System.Management.Automation;
using Microsoft.Diagnostics.Runtime;

namespace RuntimeDiagnostics
{
    public class ExtendedClrObject : PSObject
    {
        public ExtendedClrObject(ClrObject baseObject) : base(baseObject)
        {
            if (baseObject.Type?.Name == "System.String")
            {
                Properties.Add(new PSNoteProperty("Value", baseObject.AsString()));
            }
            else if (baseObject.Type?.IsArray == true)
            {
                var array = baseObject.AsArray();
                Properties.Add(new PSNoteProperty("Value", array));

                for (var i = 0; i < array.Length; i++)
                {
                    var index = i;
                    try
                    {
                        if (array.Type.ComponentType?.ElementType == ClrElementType.Object)
                        {
                            Properties.Add(new PSNoteProperty($"[{index}]", new Lazy<ExtendedClrObject>(() => new ExtendedClrObject(array.GetObjectValue(index)))));
                        }
                        else if (array.Type.ComponentType?.ElementType == ClrElementType.Struct)
                        {
                            Properties.Add(new PSNoteProperty($"[{index}]", new Lazy<ExtendedClrObject>(() => new ExtendedClrObject(array.GetStructValue(index)))));
                        }
                        else
                        {

                        }
                    }
                    catch
                    {

                    }
                }
            }

            baseObject.Type?.Fields.ToList().ForEach(field =>
            {
                if (field.Name == null) return;
                if (Properties.Any(m => m.Name == field.Name)) return;

                if (field.IsValueType)
                {
                    var value = baseObject.ReadValueTypeField(field.Name);
                    Properties.Add(new PSNoteProperty(field.Name, new Lazy<ExtendedClrObject>(() => new ExtendedClrObject(value))));
                }
                else
                {
                    var value = baseObject.ReadObjectField(field.Name);
                    Properties.Add(new PSNoteProperty(field.Name, new Lazy<ExtendedClrObject>(() => new ExtendedClrObject(value))));
                }
            });
        }

        public ExtendedClrObject(ClrValueType baseObject) : base(baseObject)
        {
        }


    }
}