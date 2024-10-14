using Godot;
using System;

[GlobalClass]
public partial class ItemData : Resource
{
    [Export] public string name = "item";
    [Export] public string[] textBoxDescription;
}
