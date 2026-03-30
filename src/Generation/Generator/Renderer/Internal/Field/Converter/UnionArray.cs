namespace Generator.Renderer.Internal.Field;

internal class UnionArray : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.TryPickT0(out var anyType, out _) && anyType.IsArray<GirModel.Union>();
    }

    public RenderableField[] Convert(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;

        return arrayType.FixedSize switch
        {
            null => Default(field),
            { } size => Array(field, size)
        };
    }

    private static RenderableField[] Default(GirModel.Field field)
    {
        return [new RenderableField(Name: Model.Field.GetName(field),
            Attribute: null,
            NullableTypeName: GetNullableTypeName(field)
        )];
    }

    private static RenderableField[] Array(GirModel.Field field, int size)
    {
        var result = new RenderableField[size];

        for (var i = 0; i < size; i++)
        {
            result[i] = new RenderableField(
                Name: $"{Model.Field.GetName(field)}{i}",
                Attribute: null,
                NullableTypeName: GetNullableTypeName(field)
            );
        }
        return result;
    }

    private static string GetNullableTypeName(GirModel.Field field)
    {
        var arrayType = field.AnyTypeOrCallback.AsT0.AsT1;
        var type = (GirModel.Union) arrayType.AnyType.AsT0;
        return Model.Union.GetFullyQualifiedInternalStructName(type);
    }
}
