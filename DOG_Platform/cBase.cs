using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOGPlatform
{
    public enum TypeInputFile
    {
        井轨迹,
        分层数据,
        解释结论,
        射孔数据,
        吸水剖面,
        水平井轨迹,
        
    }
    public enum TypeWell
    {
        Undefined = 0,
        Propose = 1,
        Dry = 2,
        Oil = 3,
        MinorOil = 4,
        Gas = 5,
        MinorGas = 6,
        Platform = 8,
        Injectwater = 15,
        InjectGas = 16,
        DrillingWell = 18
    }

    public enum TypeShowValue
    {
        value,
        rect,
        pie,
           }

    public enum LeftOrRight
    {
        left, right,
    }
    public enum TypeTrack
    {
        深度道,
        地层道,
        曲线道,
        文本道,
        岩性道,
        射孔道,
        解释结论道,
        离散数据道,
    }
    enum JSJLType
    {
        Other = 0,
        Oil = 1,
        Water = 2,
        Gas = 3,
        Dry = 4,
        OilGas = 5,
        OilWater = 6,
        GasWater = 7,
        MinorOil = 8,
        MinorGas = 9,
        Coal = 12,
        UnKnown = 13
    }

    enum OpreateMode
    {
        Initial,
        Select,
        DrawLine,
        DrawPolygon
    }

        

  

    
}
