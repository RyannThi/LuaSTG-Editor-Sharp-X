using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsettext.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetText : TreeNode
{
    [JsonConstructor]
    private RichTextSetText() : base() { }

    public RichTextSetText(DocumentData workspace)
        : this(workspace, "") { }

    public RichTextSetText(DocumentData workspace, string text)
        : base(workspace)
    {
        Text = text;
    }

    [JsonIgnore, NodeAttribute]
    public string Text
    {
        get => DoubleCheckAttr(0).attrInput;
        set => DoubleCheckAttr(0).attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setText({Macrolize(0)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set text to '{NonMacrolize(0)}'";
    }

    public override object Clone()
    {
        var n = new RichTextSetText(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
