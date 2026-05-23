using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtextmeasure.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextMeasure : TreeNode
{
    [JsonConstructor]
    private RichTextMeasure() : base() { }

    public RichTextMeasure(DocumentData workspace)
        : base(workspace) { }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + ":measure()\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return $"Measure RichText and return it";
    }

    public override object Clone()
    {
        var n = new RichTextMeasure(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
