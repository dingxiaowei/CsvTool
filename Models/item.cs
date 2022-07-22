/*
 * auto generated by tools(注意:千万不要手动修改本文件)
 * item
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

[Serializable]
public class item : IBinarySerializable
{
	public int Id { get; set; }
	public string Comment { get; set; }
	public string Name { get; set; }
	public string ItemId { get; set; }
	public string ResPath { get; set; }
	public List<float> Offset { get; set; }
	public float Scale { get; set; }
	public string PositionType { get; set; }
	public int Script { get; set; }
	public string ScriptFilePath { get; set; }
	public List<List<float>> Grid { get; set; }

	public void DeSerialize(BinaryReader reader)
	{
		Id = reader.ReadInt32();
		Comment = reader.ReadString();
		Name = reader.ReadString();
		ItemId = reader.ReadString();
		ResPath = reader.ReadString();
		var OffsetCount = reader.ReadInt32();
		if (OffsetCount > 0)
		{
			Offset = new List<float>();
			for (int i = 0; i < OffsetCount; i++)
			{
				Offset.Add(reader.ReadSingle());
			}
		}
		else
		{
			Offset = null;
		}
		Scale = reader.ReadSingle();
		PositionType = reader.ReadString();
		Script = reader.ReadInt32();
		ScriptFilePath = reader.ReadString();
		var GridCount = reader.ReadInt32();
		if (GridCount > 0)
		{
			Grid = new List<List<float>>();
			for (int i = 0; i < GridCount; i++)
			{
				var tempList = new List<float>();
				var tempListCount = reader.ReadInt32();
				for (int j = 0; j < tempListCount; j++)
				{
					tempList.Add(reader.ReadSingle());
				}
				Grid.Add(tempList);
			}
		}
		else
		{
			Grid = null;
		}
	}

	public void Serialize(BinaryWriter writer)
	{
		writer.Write(Id);
		writer.Write(Comment);
		writer.Write(Name);
		writer.Write(ItemId);
		writer.Write(ResPath);
		if (Offset == null || Offset.Count == 0)
		{
			writer.Write(0);
		}
		else
		{
			writer.Write(Offset.Count);
			for (int i = 0; i < Offset.Count; i++)
			{
				writer.Write(Offset[i]);
			}
		}
		writer.Write(Scale);
		writer.Write(PositionType);
		writer.Write(Script);
		writer.Write(ScriptFilePath);
		if (Grid == null || Grid.Count == 0)
		{
			writer.Write(0);
		}
		else
		{
			writer.Write(Grid.Count);
			for (int i = 0; i < Grid.Count; i++)
			{
				writer.Write(Grid[i].Count);
				for (int j = 0; j < Grid[i].Count; j++)
				{
					writer.Write(Grid[i][j]);
				}
			}
		}
	}
}

[Serializable]
public partial class itemConfig : IBinarySerializable
{
	public List<item> itemInfos = new List<item>();
	public void DeSerialize(BinaryReader reader)
	{
		int count = reader.ReadInt32();
		for (int i = 0;i < count; i++)
		{
			item tempData = new item();
			tempData.DeSerialize(reader);
			itemInfos.Add(tempData);
		}
	}

	public void Serialize(BinaryWriter writer)
	{
		writer.Write(itemInfos.Count);
		for (int i = 0; i < itemInfos.Count; i++)
		{
			itemInfos[i].Serialize(writer);
		}
	}

	public IEnumerable<item> QueryById(int id)
	{
		var datas = from d in itemInfos
					where d.Id == id
					select d;
		return datas;
	}
}
