using Core.Common.EnumHelper;

namespace Core.Common.TestEnumHelper {
    public enum EnumTestFake {
        unknow = -1,
        [EnumDescription(@"00")]
        testNumCero = 0,
        [EnumDescription(@"10")]
        testNumUno = 1,
        [EnumDescription(@"25")]
        testNumDos = 2,
        [EnumDescription(@"tres")]
        testNumTres = 3,
        [EnumDescription(@"cuatro")]
        testNumCuatro = 4,
        [EnumDescription(@"cinco")]
        testNumCinco = 5,
        [EnumDescription(@"seis")]
        testNumSeis = 6,
        [EnumDescription(@"siete07")]
        testNumSiete = 7,
        [EnumDescription(@"080")]
        testNumOcho = 8,
        [EnumDescription(@"1000")]
        testNumNueve = 9,
        [EnumDescription(@"hgfjrhguor")]
        testNumDiez = 10,
    }
}
