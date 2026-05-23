using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetshadow.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetShadow : TreeNode
{
    [JsonConstructor]
    private RichTextSetShadow() : base() { }

    public RichTextSetShadow(DocumentData workspace)
        : this(workspace, "0, 0", "255, 255, 255, 255", "0") { }

    public RichTextSetShadow(DocumentData workspace, string offset, string color, string blur)
        : base(workspace)
    {
        Offset = offset;
        ARGB = color;
        Blur = blur;
    }

    [JsonIgnore, NodeAttribute]
    public string Offset
    {
        get => DoubleCheckAttr(0).attrInput;
        set => DoubleCheckAttr(0).attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string ARGB
    {
        get => DoubleCheckAttr(1, "ARGB").attrInput;
        set => DoubleCheckAttr(1, "ARGB").attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string Blur
    {
        get => DoubleCheckAttr(2).attrInput;
        set => DoubleCheckAttr(2).attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setShadow({Macrolize(0)}, lstg.Color({Macrolize(1)}), {Macrolize(2)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set text shadow with color ({NonMacrolize(1)}) with offset ({NonMacrolize(0)}) and blur ({NonMacrolize(2)})";
    }

    public override object Clone()
    {
        var n = new RichTextSetShadow(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
