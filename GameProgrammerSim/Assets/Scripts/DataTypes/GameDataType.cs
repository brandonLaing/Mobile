using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class GameDataType
{
  private int _releaseDay;
  private int _releaseMonth;
  private int _releaseYear;

  private DateTime ReleaseTime
  {
    get { return new DateTime(_releaseYear, _releaseMonth, _releaseDay); }
    set
    {
      _releaseDay = value.Day;
      _releaseMonth = value.Month;
      _releaseYear = value.Year;
    }
  }

  private int _codePointsEntered;
  private int _codePointsMax;
  private int _designPointsEntered;
  private int _designPointsMax;
  private int _artPointsEntered;
  private int _artPointsMax;

  private int CodePointsEntered
  {
    get { return _codePointsEntered; } 
    set
    {
      if (value > _codePointsMax)
        _codePointsEntered = _codePointsMax;
      else if (value < 0)
        _codePointsEntered = 0;
      else
        _codePointsEntered = value;
    }
  }

  private int DesignPointsEntered
  {
    get { return _designPointsEntered; }
    set
    {
      if (value > _designPointsMax)
        _designPointsEntered = _designPointsMax;
      else if (value < 0)
        _designPointsEntered = 0;
      else
        _designPointsEntered = value;
    }
  }

  private int ArtPointsEntered
  {
    get { return _artPointsEntered; }
    set
    {
      if (value > _artPointsMax)
        _artPointsEntered = _artPointsMax;
      else if (value < 0)
        _artPointsEntered = 0;
      else
        _artPointsEntered = value;
    }
  }

  private int CodePointsMax
  {
    get { return _codePointsMax; }
    set
    {
      if (value < 0)
        _codePointsMax = 0;
      else
        _codePointsMax = value;
    }
  }

  private int DesignPointsMax
  {
    get { return _designPointsMax; }
    set
    {
      if (value < 0)
        _designPointsMax = 0;
      else
        _designPointsMax = value;
    }
  }

  private int ArtPointsMax
  {
    get { return _artPointsMax; }
    set
    {
      if (value < 0)
        _artPointsMax = 0;
      else
        _artPointsMax = value;
    }
  }



  /// <summary>
  /// Ration of quality between 0 and 1
  /// </summary>
  public float Quality
  {
    get
    {
      return
        ((CodePointsEntered / CodePointsMax) + 
        (DesignPointsEntered / DesignPointsMax) + 
        (ArtPointsEntered / ArtPointsMax) +
        (_randomQualityNumber / 100))
        / 4
        ;
    }
  }

  private float _randomQualityNumber;

  // Popularity is a mix of how good it is and how long since it came out.
  /// <summary>
  /// Popularity starts at 1 and goes down to 0 as time goes by
  /// </summary>
  public float Popularity
  {
    get
    {
      return
        1 /
        (float)(DateTime.Now - ReleaseTime).TotalDays * 0.01F + 1;
    }
  }

  public void ReleaseGame(int codePointsEntered, int designPointsEntered, int artPointsEntered)
  {
    ReleaseTime = DateTime.Now;

    this.CodePointsEntered = codePointsEntered;
    this.DesignPointsEntered = designPointsEntered;
    this.ArtPointsEntered = artPointsEntered;
  }

  public GameDataType(int codePointsMax, int designPointsMax, int artPointsMax, float randomQualityNumber)
  {
    this.CodePointsMax = codePointsMax;
    this.DesignPointsMax = designPointsMax;
    this.ArtPointsMax = artPointsMax;

    _randomQualityNumber = randomQualityNumber;
  }
}