using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grasstiles : Tile
{
    [SerializeField] private Color _baseColor, _offsetColor;
    //_renderer.color=_baseColor
    public override void Init(int x, int y)
    {
        var isOffset = (x + y) % 2 != 0;
        _renderer.color = isOffset ? _baseColor : _offsetColor;
    }
}
