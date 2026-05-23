using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetoutline.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetOutline : TreeNode
{
    [JsonConstructor]
    private RichTextSetOutline() : base() { }

    public RichTextSetOutline(DocumentData workspace)
        : this(workspace, "255, 255, 255, 255", "2.0") { }

    public RichTextSetOutline(DocumentData workspace, string color, string size)
        : base(workspace)
    {
        ARGB = color;
        Size = size;
    }

    [JsonIgnore, NodeAttribute]
    public string ARGB
    {
        get => DoubleCheckAttr(0, "ARGB").attrInput;
        set => DoubleCheckAttr(0, "ARGB").attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string Size
    {
        get => DoubleCheckAttr(1).attrInput;
        set => DoubleCheckAttr(1).attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        // I like having variable with inverted order. Makes things hard.
        // Thanks Runa from the present.
        // -- Runa from the future.
        yield return sp + sp1 + $":setOutline({Macrolize(1)}, lstg.Color({Macrolize(0)}))\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set text outline color to ({NonMacrolize(0)}) with a size of {NonMacrolize(1)}";
    }

    public override object Clone()
    {
        var n = new RichTextSetOutline(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
