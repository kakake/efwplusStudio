using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using ${ObjectModel.AppName}.Entity;
using ${ObjectModel.AppName}.Dao;

namespace ${ObjectModel.AppName}.ObjectModel
{
    public class ${ObjectModel.ClassName} : AbstractBusines
    {
        private ${ObjectModel.DaoName} ${ObjectModel.varDao} = null;

        public ${ObjectModel.ClassName}()
        {
        }
		
		public void method1()
        {
			_hellodao = NewDao<HelloDao>();
        }

    }
}
