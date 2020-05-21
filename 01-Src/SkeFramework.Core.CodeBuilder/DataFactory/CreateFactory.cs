using SkeFramework.Core.CodeBuilder.DataServices;
using SkeFramework.Core.CodeBuilder.DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Core.CodeBuilder.DataFactory
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

        /// <summary>
        /// 返回对应生成类
        /// </summary>
        /// <param name="CreateType"></param>
        /// <returns></returns>
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
                case CreateIEntity.Type:
                    return new CreateIEntity();
                case CreateDataHandleManager.Type:
                    return new CreateDataHandleManager();
            }
            return null;
        }
        /// <summary>
        /// 检查模板名称是否可以生成
        /// </summary>
        /// <param name="TemplateName"></param>
        /// <returns></returns>
        public bool CheckCreateTemplate(string TemplateName)
        {
            switch (TemplateName)
            {
                case CreateEntities.Name:
                case CreateHandleCommonInterface.Name:
                case CreateHandleCommon.Name:
                case CreateHandleInterface.Name:
                case CreateHandles.Name:
                case CreateController.Name:
                case CreateViewList.Name:
                case CreateViewUpdate.Name:
                case CreateViewAdd.Name:
                case CreateIEntity.Name:
                case CreateDataHandleManager.Name:
                    return true;
            }
            return false;
        }
    }
}
