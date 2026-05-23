using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LuaSTGEditorSharp.EditorData.Node.RichText;

[Serializable, NodeIcon("richtexthasanimation.png")]
[LeafNode]
[RequireParent(typeof(ReichTextCreate), typeof(ReichTextRef))]
public class RichTextHasAnimation : TreeNode
{
    [JsonConstructor]
    private RichTextHasAnimation() : base() { }

    public RichTextHasAnimation(DocumentData workspace)
        : base(workspace) { }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + sp1 + ":hasAnimation()\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return "Check if RichText has animation";
    }

    public override object Clone()
    {
        var n = new RichTextHasAnimation(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
