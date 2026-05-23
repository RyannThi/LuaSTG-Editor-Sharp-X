using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetvertalign.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetVAlign : TreeNode
{
    [JsonConstructor]
    private RichTextSetVAlign() : base() { }

    public RichTextSetVAlign(DocumentData workspace)
        : this(workspace, "\"top\"") { }

    public RichTextSetVAlign(DocumentData workspace, string align)
        : base(workspace)
    {
        Align = align;
    }

    [JsonIgnore, NodeAttribute]
    public string Align
    {
        get => DoubleCheckAttr(0, "richtextvalign", "Alignment").attrInput;
        set => DoubleCheckAttr(0, "richtextvalign", "Alignment").attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setVAlign({Macrolize(0)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set vertical alignment to {NonMacrolize(0)}";
    }

    public override object Clone()
    {
        var n = new RichTextSetVAlign(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
