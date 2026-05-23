using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetunitperpixel.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetUnitPerPixel : TreeNode
{
    [JsonConstructor]
    private RichTextSetUnitPerPixel() : base() { }

    public RichTextSetUnitPerPixel(DocumentData workspace)
        : this(workspace, "1") { }

    public RichTextSetUnitPerPixel(DocumentData workspace, string value)
        : base(workspace)
    {
        Value = value;
    }

    [JsonIgnore, NodeAttribute]
    public string Value
    {
        get => DoubleCheckAttr(0).attrInput;
        set => DoubleCheckAttr(0).attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setUnitPerPixel({Macrolize(0)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set units per pixel to {NonMacrolize(0)}";
    }

    public override object Clone()
    {
        var n = new RichTextSetUnitPerPixel(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
