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

namespace LuaSTGEditorSharp.EditorData.Node.Graphics;

[Serializable, NodeIcon("settexturesamplerstate.png")]
[LeafNode]
[RCInvoke(1)]
public class SetTextureSamplerState : TreeNode
{
    [JsonConstructor]
    private SetTextureSamplerState() : base() { }

    public SetTextureSamplerState(DocumentData workSpaceData)
        : this(workSpaceData, "", "\"point+wrap\"") { }

    public SetTextureSamplerState(DocumentData workSpaceData, string name, string state)
        : base(workSpaceData)
    {
        Name = name;
        State = state;
    }

    [JsonIgnore, NodeAttribute]
    public string Name
    {
        get => DoubleCheckAttr(0, "image").attrInput;
        set => DoubleCheckAttr(0, "image").attrInput = value;
    }

    [JsonIgnore, NodeAttribute]
    public string State
    {
        get => DoubleCheckAttr(1, "samplerstate").attrInput;
        set => DoubleCheckAttr(1, "samplerstate").attrInput = value;
    }

    public override IEnumerable<string> ToLua(int spacing)
    {
        string sp = Indent(spacing);
        yield return sp + "lstg.SetTextureSamplerState(" + Macrolize(0) + ", " + Macrolize(1) + ")\n";
    }

    public override IEnumerable<Tuple<int, TreeNode>> GetLines()
    {
        yield return new Tuple<int, TreeNode>(1, this);
    }

    public override string ToString()
    {
        return "Set sample state of '" + NonMacrolize(0) + "' to (" + NonMacrolize(1) + ")";
    }

    public override object Clone()
    {
        var n = new SetTextureSamplerState(parentWorkSpace);
        n.DeepCopyFrom(this);
        return n;
    }
}
