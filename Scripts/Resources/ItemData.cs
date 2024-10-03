using Godot;
using System;

[GlobalClass]
public partial class ItemData : Resource
{
    [Export] public string itemName = "item";
    [Export] public string textBoxString = "";
}
