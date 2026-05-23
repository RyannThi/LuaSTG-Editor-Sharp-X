using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetfontsize.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetFontSize : TreeNode
{
    [JsonConstructor]
    private RichTextSetFontSize() : base() { }

    public RichTextSetFontSize(DocumentData workspace)
        : this(workspace, "24") { }

    public RichTextSetFontSize(DocumentData workspace, string size)
        : base(workspace)
    {
        Size = size;
    }

    [JsonIgnore, NodeAttribute]
    public string Size
    {
        get => DoubleCheckAttr(0).attrInput;
        set => DoubleCheckAttr(0).attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setFontSize({Macrolize(0)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set font size to {NonMacrolize(0)}";
    }

    public override object Clone()
    {
        var n = new RichTextSetFontSize(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
