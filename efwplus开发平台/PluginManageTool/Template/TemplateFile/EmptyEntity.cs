using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Orm;
using EFWCoreLib.CoreFrame.Business;

namespace ${Entity.AppName}.Entity
{
    [Serializable]
    [Table(TableName = "${Entity.ClassName}", EntityType = EntityType.Table, IsGB = false)]
    public abstract class ${Entity.ClassName}:AbstractEntity
    {
#foreach($val in $Entity.Property) 
        private $val.TypeName $val.varName;
        /// <summary>
        /// 
        /// </summary>
        [Column(FieldName = "$val.PropertyName", DataKey = false, Match = "", IsInsert = true)]
        public $val.TypeName $val.PropertyName
        {
            get { return $val.varName; }
            set { $val.varName = value; }
        }
#end
    }
}
