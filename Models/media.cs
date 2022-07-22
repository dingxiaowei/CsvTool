/*
 * auto generated by tools(ע��:ǧ��Ҫ�ֶ��޸ı��ļ�)
 * media
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

[Serializable]
public class media : IBinarySerializable
{
	public int Id { get; set; }
	public string ItemId { get; set; }
	public string Name { get; set; }
	public string LocalPath { get; set; }
	public string Path { get; set; }
	public float TimeLength { get; set; }
	public int ResType { get; set; }
	public int PlayMode { get; set; }

	public void DeSerialize(BinaryReader reader)
	{
		Id = reader.ReadInt32();
		ItemId = reader.ReadString();
		Name = reader.ReadString();
		LocalPath = reader.ReadString();
		Path = reader.ReadString();
		TimeLength = reader.ReadSingle();
		ResType = reader.ReadInt32();
		PlayMode = reader.ReadInt32();
	}

	public void Serialize(BinaryWriter writer)
	{
		writer.Write(Id);
		writer.Write(ItemId);
		writer.Write(Name);
		writer.Write(LocalPath);
		writer.Write(Path);
		writer.Write(TimeLength);
		writer.Write(ResType);
		writer.Write(PlayMode);
	}
}

[Serializable]
public partial class mediaConfig : IBinarySerializable
{
	public List<media> mediaInfos = new List<media>();
	public void DeSerialize(BinaryReader reader)
	{
		int count = reader.ReadInt32();
		for (int i = 0;i < count; i++)
		{
			media tempData = new media();
			tempData.DeSerialize(reader);
			mediaInfos.Add(tempData);
		}
	}

	public void Serialize(BinaryWriter writer)
	{
		writer.Write(mediaInfos.Count);
		for (int i = 0; i < mediaInfos.Count; i++)
		{
			mediaInfos[i].Serialize(writer);
		}
	}

	public IEnumerable<media> QueryById(int id)
	{
		var datas = from d in mediaInfos
					where d.Id == id
					select d;
		return datas;
	}
}