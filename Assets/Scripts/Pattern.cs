using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pattern
{
	public string name;
	public List<Vector2Int> shape;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="name">名前</param>
	/// <param name="shape">形状</param>
	public Pattern(string name, List<Vector2Int> shape)
	{
		this.name = name;
		this.shape = shape;
	}

	/// <summary>
	/// 結果を検証する
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public bool Matches(List<Vector2Int> input)
	{
		// 左下原点
		var normalizedInput = Normalize(input);

		// 左下原点
		var shape = Normalize(this.shape);
		// 配列すべて(形状)が一致するか
		if (normalizedInput.OrderBy(p => p.x * 100 + p.y)
			.SequenceEqual(shape.OrderBy(p => p.x * 100 + p.y)))
		{
			return true;
		}
		// 一致していない
		return false;
	}

	/// <summary>
	/// 回転すべての結果を検証する
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public bool MatchesAllRotate(List<Vector2Int> input)
	{
		// 左下原点
		var normalizedInput = Normalize(input);

		// すべての回転を検証
		for (int rot = 0; rot < 4; rot++)
		{
			// 形状を回転させる
			var rotated = Rotate(Normalize(shape), rot);
			// 配列ですべての要素を検証
			if (normalizedInput.OrderBy(p => p.x * 100 + p.y)
				.SequenceEqual(rotated.OrderBy(p => p.x * 100 + p.y)))
			{
				return true;
			}
		}
		// 一致していない
		return false;
	}

	/// <summary>
	/// 左下原点に合わせる正規化
	/// </summary>
	List<Vector2Int> Normalize(List<Vector2Int> shape)
	{
		int minX = shape.Min(p => p.x);
		int minY = shape.Min(p => p.y);
		return shape.Select(p => new Vector2Int(p.x - minX, p.y - minY)).ToList();
	}

	/// <summary>
	/// 形状を回転させる
	/// </summary>
	List<Vector2Int> Rotate(List<Vector2Int> shape, int rotation)
	{
		// 0, 90, 180, 270度回転
		switch (rotation)
		{
			case 90:
				return shape.Select(p => new Vector2Int(p.y, -p.x)).ToList();
			case 180:
				return shape.Select(p => new Vector2Int(-p.x, -p.y)).ToList();
			case 270:
				return shape.Select(p => new Vector2Int(-p.y, p.x)).ToList();
			default:
				return shape;
		}
	}
}
