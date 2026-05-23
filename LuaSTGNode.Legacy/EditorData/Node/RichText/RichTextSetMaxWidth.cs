using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetmaxwidth.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetMaxWidth : TreeNode
{
    [JsonConstructor]
    private RichTextSetMaxWidth() : base() { }

    public RichTextSetMaxWidth(DocumentData workspace)
        : this(workspace, "0") { }

    public RichTextSetMaxWidth(DocumentData workspace, string width)
        : base(workspace)
    {
        Width = width;
    }

    [JsonIgnore, NodeAttribute]
    public string Width
    {
        get => DoubleCheckAttr(0).attrInput;
        set => DoubleCheckAttr(0).attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setMaxWidth({Macrolize(0)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set max text width to {NonMacrolize(0)}";
    }

    public override object Clone()
    {
        var n = new RichTextSetMaxWidth(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
