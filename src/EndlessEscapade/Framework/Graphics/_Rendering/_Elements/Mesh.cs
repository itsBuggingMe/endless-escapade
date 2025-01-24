namespace EndlessEscapade.Framework.Graphics;

public struct Mesh : IRenderElement
{
    /// <summary>
    ///     Gets or sets the vertices of the mesh.
    /// </summary>
    public VertexPositionColorTexture[] Vertices { readonly get; set; }
    
    /// <summary>
    ///     Gets or sets the indices of the mesh.
    /// </summary>
    public short[] Indices { readonly get; set; }
    
    /// <summary>
    ///     Gets or sets the <see cref="PrimitiveType"/> of the mesh.
    /// </summary>
    public PrimitiveType Type { readonly get; set; }
    
    /// <summary>
    ///     Gets the primitive count of the mesh.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws if the mesh does not have a valid primitive type.</exception>
    public int Count => Type switch
    {
        PrimitiveType.TriangleStrip => Vertices.Length - 2,
        PrimitiveType.TriangleList => Vertices.Length / 3,
        PrimitiveType.LineStrip => Vertices.Length - 1,
        PrimitiveType.LineList => Vertices.Length / 2,
        PrimitiveType.PointListEXT => Vertices.Length,
        _ => throw new InvalidOperationException($"Invalid primitive type: {Type}")  
    };

    public void Draw(in SpriteBatchParameters parameters)
    {
        MeshRendering.Draw(in this, in parameters);
    }

    /// <summary>
    ///     Sets the vertices of the mesh.
    /// </summary>
    /// <param name="vertices">The vertices to set.</param>
    /// <returns>The updated <see cref="Mesh"/>.</returns>
    public Mesh SetVertices(params VertexPositionColorTexture[] vertices)
    {
        Vertices = vertices;
        
        return this;
    }

    /// <summary>
    ///     Sets the indices of the mesh.
    /// </summary>
    /// <param name="indices">The indices to set.</param>
    /// <returns>The updated <see cref="Mesh"/>.</returns>
    public Mesh SetIndices(params short[] indices)
    {
        Indices = indices;
        
        return this;
    }
}