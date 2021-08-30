using Garciss.Core.Common.EnumHelper;

namespace Garciss.Core.Common.TestEnumHelper.Fake {
    public enum EnumTestFake {
        unknow = -1,
        [EnumDescription(@"00")]
        testNumCero = 0,
        [EnumDescription(@"10")]
        testNumUno = 1,
        [EnumDescription(@"Dos")]
        testNumDos = 2,
        [EnumDescription(@"hgfjrhguor")]
        testNumDiez = 3,
    }

    public enum EnumTestFakeSinClaves {
        desconocido = -1,
        enumSinClave0 = 0,
        enumSinClave1 = 1,
        enumSinClave2 = 2,
        enumSinClave3 = 3
    }
}
