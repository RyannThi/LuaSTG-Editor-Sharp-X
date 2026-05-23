using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsethodalign.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetHAlign : TreeNode
{
    [JsonConstructor]
    private RichTextSetHAlign() : base() { }

    public RichTextSetHAlign(DocumentData workspace)
        : this(workspace, "\"left\"") { }

    public RichTextSetHAlign(DocumentData workspace, string align)
        : base(workspace)
    {
        Align = align;
    }

    [JsonIgnore, NodeAttribute]
    public string Align
    {
        get => DoubleCheckAttr(0, "richtexthalign", "Alignment").attrInput;
        set => DoubleCheckAttr(0, "richtexthalign", "Alignment").attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setHAlign({Macrolize(0)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set horizontal alignment to {NonMacrolize(0)}";
    }

    public override object Clone()
    {
        var n = new RichTextSetHAlign(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
