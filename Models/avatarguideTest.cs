/*
 * auto generated by tools(注意:千万不要手动修改本文件)
 * avatarguideTest
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

[Serializable]
public class avatarguideTest : IBinarySerializable
{
	public int Id { get; set; }
	public string gender { get; set; }
	public float age { get; set; }
	public bool bValue { get; set; }
	public List<float> vector { get; set; }
	public List<List<float>> Grid { get; set; }

	public void DeSerialize(BinaryReader reader)
	{
		Id = reader.ReadInt32();
		gender = reader.ReadString();
		age = reader.ReadSingle();
		bValue = reader.ReadBoolean();
		var vectorCount = reader.ReadInt32();
		if (vectorCount > 0)
		{
			vector = new List<float>();
			for (int i = 0; i < vectorCount; i++)
			{
				vector.Add(reader.ReadSingle());
			}
		}
		else
		{
			vector = null;
		}
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
		writer.Write(gender);
		writer.Write(age);
		writer.Write(bValue);
		if (vector == null || vector.Count == 0)
		{
			writer.Write(0);
		}
		else
		{
			writer.Write(vector.Count);
			for (int i = 0; i < vector.Count; i++)
			{
				writer.Write(vector[i]);
			}
		}
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
	public override string ToString()
    {
        var str = $"Id:{Id} Gender:{gender} age:{age} bValue:{bValue} ";
        if (vector != null)
        {
            str += $"vector:[{vector[0]},{vector[1]},{vector[2]}] ";
        }
        if (Grid != null)
        {
            str += "Grid:[";
            for (int i = 0; i < Grid.Count; i++)
            {
                str += $"[{Grid[i][0]},{Grid[i][1]},{Grid[i][2]}]";
                if (i < Grid.Count - 1)
                    str += ",";
            }
            str += "]";
        }
        return str;
    }
}

[Serializable]
public partial class avatarguideTestConfig : IBinarySerializable
{
	public List<avatarguideTest> avatarguideTestInfos = new List<avatarguideTest>();
	public void DeSerialize(BinaryReader reader)
	{
		int count = reader.ReadInt32();
		for (int i = 0;i < count; i++)
		{
			avatarguideTest tempData = new avatarguideTest();
			tempData.DeSerialize(reader);
			avatarguideTestInfos.Add(tempData);
		}
	}

	public void Serialize(BinaryWriter writer)
	{
		writer.Write(avatarguideTestInfos.Count);
		for (int i = 0; i < avatarguideTestInfos.Count; i++)
		{
			avatarguideTestInfos[i].Serialize(writer);
		}
	}

	public IEnumerable<avatarguideTest> QueryById(int id)
	{
		var datas = from d in avatarguideTestInfos
					where d.Id == id
					select d;
		return datas;
	}
	
	public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < avatarguideTestInfos.Count; i++)
        {
            sb.Append($"{avatarguideTestInfos[i].ToString()}\n");
        }
        return sb.ToString();
    }
}
