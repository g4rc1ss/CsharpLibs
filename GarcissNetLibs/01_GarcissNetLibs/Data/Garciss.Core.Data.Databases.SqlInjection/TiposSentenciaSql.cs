namespace Garciss.Core.Data.Databases.SqlInjection {
    public enum TiposSentenciaSql {
        /// <summary>
        /// 
        /// </summary>
        None = -1,
        /// <summary>
        /// 
        /// </summary>
        Procedure = 0,
        /// <summary>
        /// 
        /// </summary>
        Alter = 1,
        /// <summary>
        /// 
        /// </summary>
        Create = 2,
        /// <summary>
        /// 
        /// </summary>
        Delete = 4,
        /// <summary>
        /// 
        /// </summary>
        Drop = 8,
        /// <summary>
        /// 
        /// </summary>
        Execute = 16,
        /// <summary>
        /// 
        /// </summary>
        Insert = 32,
        /// <summary>
        /// 
        /// </summary>
        Select = 64,
        /// <summary>
        /// 
        /// </summary>
        Update = 128,
        /// <summary>
        /// 
        /// </summary>
        Union = 256,
        /// <summary>
        /// 
        /// </summary>
        Batch = 512,
        /// <summary>
        /// 
        /// </summary>
        Merge = 1024 | Delete | Insert | Select | Update,
        /// <summary>
        /// 
        /// </summary>
        Comment = 2048
    }
}
