using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBuilder.BLL;
using CodeBuilder.BLL.Interfaces;

namespace CodeBuilder.DAL.DataFactory
{
    /// <summary>
    /// 代码生成工厂类
    /// </summary>
   public class CreateFactory
    {
        private static CreateFactory _simpleInstance = null;

        static CreateFactory()
        {


        }

        public static CreateFactory Instance()
        {
            if (_simpleInstance == null)
            {
                _simpleInstance = new CreateFactory();
            }
            return _simpleInstance;
        }

        public ICreate GetCreateHandle(int CreateType)
        {
            switch (CreateType)
            {
                case CreateEntities.Type:
                    return new CreateEntities();
                case CreateHandleCommonInterface.Type:
                    return new CreateHandleCommonInterface();
                case CreateHandleCommon.Type:
                    return new CreateHandleCommon();
                case CreateHandleInterface.Type:
                    return new CreateHandleInterface();
                case CreateHandles.Type:
                    return new CreateHandles();
                case CreateController.Type:
                    return new CreateController();
                case CreateViewList.Type:
                    return new CreateViewList();
                case CreateViewUpdate.Type:
                    return new CreateViewUpdate();
                case CreateViewAdd.Type:
                    return new CreateViewAdd();
            }
            return null;
        }
    }
}
