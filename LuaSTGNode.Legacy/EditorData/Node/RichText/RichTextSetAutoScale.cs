using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextsetautoscale.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextSetAutoScale : TreeNode
{
    [JsonConstructor]
    private RichTextSetAutoScale() : base() { }

    public RichTextSetAutoScale(DocumentData workspace)
        : this(workspace, "true") { }

    public RichTextSetAutoScale(DocumentData workspace, string enable)
        : base(workspace)
    {
        Enable = enable;
    }

    [JsonIgnore, NodeAttribute]
    public string Enable
    {
        get => DoubleCheckAttr(0, "bool", "Enable").attrInput;
        set => DoubleCheckAttr(0, "bool", "Enable").attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + $":setAutoScale({Macrolize(0)})\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Set auto scale: {NonMacrolize(0)}";
    }

    public override object Clone()
    {
        var n = new RichTextSetAutoScale(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
