using LuaSTGEditorSharp.EditorData;
using LuaSTGEditorSharp.EditorData.Document;
using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using LuaSTGEditorSharp.EditorData.Node.Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaSTGEditorSharp.EditorData.Node.Boss;

[Serializable, NodeIcon("bossetfloat.png")]
[RequireAncestor(typeof(BossAlikeTypes))]
[LeafNode]
[RCInvoke(1)]
public class BossSetFloat : TreeNode
{
    [JsonConstructor]
    private BossSetFloat() : base() { }

    public BossSetFloat(DocumentData workSpaceData)
        : this(workSpaceData, "3", "3") { }

    public BossSetFloat(DocumentData workSpaceData, string speed, string amp)
        : base(workSpaceData)
    {
        Speed = speed;
        Amplitude = amp;
    }

    [JsonIgnore, NodeAttribute]
    public string Speed
    {
        get => DoubleCheckAttr(0).attrInput;
        set => DoubleCheckAttr(0).attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string Amplitude
    {
        get => DoubleCheckAttr(1).attrInput;
        set => DoubleCheckAttr(1).attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        string sp1 = Indent(1);
        yield return sp + "self._wisys:SetFloat(function(ani)\n";
        yield return sp + sp1 + $"return 0, {Macrolize(1)} * sin(ani * {Macrolize(0)})\n";
        yield return sp + "end)\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(3, this);
    }

    public override string ToString()
    {
        return $"Set the boss floating animation with an amplitude of {NonMacrolize(1)} and speed of {NonMacrolize(0)}";
    }

    public override object Clone()
    {
        var n = new BossSetFloat(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
