using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextrender.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextRender : TreeNode
{
    [JsonConstructor]
    private RichTextRender() : base() { }

    public RichTextRender(DocumentData workspace)
        : this(workspace, "0, 0", "1", "1", "0") { }

    public RichTextRender(DocumentData workspace, string position, string scaleX, string scaleY, string rotation)
        : base(workspace)
    {
        Position = position;
        ScaleX = scaleX;
        ScaleY = scaleY;
        Rotation = rotation;
    }

    [JsonIgnore, NodeAttribute]
    public string Position
    {
        get => DoubleCheckAttr(0).attrInput;
        set => DoubleCheckAttr(0).attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string ScaleX
    {
        get => DoubleCheckAttr(1, name: "Scale X").attrInput;
        set => DoubleCheckAttr(1, name: "Scale X").attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string ScaleY
    {
        get => DoubleCheckAttr(2, name: "Scale Y").attrInput;
        set => DoubleCheckAttr(2, name: "Scale Y").attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string Rotation
    {
        get => DoubleCheckAttr(3, name: "Rotation").attrInput;
        set => DoubleCheckAttr(3, name: "Rotation").attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":render({Macrolize(0)}, {Macrolize(1)}, {Macrolize(2)}, {Macrolize(3)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Render RichText at ({NonMacrolize(0)})";
    }

    public override object Clone()
    {
        var n = new RichTextRender(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
