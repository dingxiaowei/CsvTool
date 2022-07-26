# CsvTool

通用CSV打表工具 - power by aladdin
#### 前言
目前项目中用的csv转json工具，会带来严重的gc性能问题，例如启动加载慢，影响帧率等问题，转成的json数据，我们需要手动写对应的解析模板代码，所以针对以上问题，本通用打表工具应运而生，用二进制数据替代json数据会比较好的解决性能问题，而且能够自动生成对应的模板代码，节省了写重复代码的时间，而且二进制数据比json文件大小更小。

#### 功能
* 生成对应的数据模板代码
* 生成二进制文件
* 自动将文件拷贝到对应的工程目录
* 二进制数据读取
* 字段不配置会使用默认数值

#### 目前支持的字段
* 常规的字段，例如int，float，string，bool，long
* 支持自定义字段 vector，例如[1,2,3]
* 支持自定义字段 list,其实是vector数组，例如[[1,2,3],[2,3,4],[4,5,6]]

#### 效果
![](img/1.gif)

![](img/2.jpg)
相比较json，二进制文件大小只有其1/4

##### 生成的自动化模板代码
```
/*
 * auto generated by tools(注意:千万不要手动修改本文件)
 * avatarguideTest
 */
using System;
using System.IO;
using System.Collections.Generic;

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
}

[Serializable]
public class avatarguideTestList : IBinarySerializable
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
}


```

#### 使用案例
##### 解析二进制文件
```
IBinarySerializable newavList = new avatarguideTestList();
var readOK = FileManager.ReadBinaryDataFromFile(Path.Combine(path, "avatarguideTest.dat"), ref newavList);
if (readOK)
{
    ConsoleHelper.WriteInfoLine(newavList.ToString());
}
else
{
    ConsoleHelper.WriteErrorLine("读取失败");
}
```
![](img/3.jpg)

#### 自动化拷贝批处理
```
@echo off

echo "开始拷贝jsons"
for %%i in (*.dat) do (
    echo begin copy... %%i
    copy /y %%~nxi ..\..\hola_unity\Assets\Resources\Config\%%~nxi
    echo copy complate ... %%i
)
echo "拷贝完成"

pause
```

#### 带扩展的功能
目前还不是最理想的状态，还有很多可以扩展完善的功能，如下：

* 支持生成各种类型的文件，例如json、xml、proto等
* 支持生成各种语法的模板代码
* 支持Excel数据配置规范性检测，例如手误配置不符合规范导致加载异常，例如大小写逗号(肉眼容易忽略)，或者空格等等
* 支持Excel字段客户端服务器可选项
* 支持更多自定义数据类型扩展
