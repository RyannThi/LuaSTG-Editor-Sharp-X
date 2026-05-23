using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetmaxheight.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetMaxHeight : TreeNode
{
    [JsonConstructor]
    private RichTextSetMaxHeight() : base() { }

    public RichTextSetMaxHeight(DocumentData workspace)
        : this(workspace, "0") { }

    public RichTextSetMaxHeight(DocumentData workspace, string height)
        : base(workspace)
    {
        Height = height;
    }

    [JsonIgnore, NodeAttribute]
    public string Height
    {
        get => DoubleCheckAttr(0).attrInput;
        set => DoubleCheckAttr(0).attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setMaxHeight({Macrolize(0)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set max text height to {NonMacrolize(0)}";
    }

    public override object Clone()
    {
        var n = new RichTextSetMaxHeight(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
