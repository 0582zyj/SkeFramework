using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeFramework.Winform.SoftAuthorize.DataHandle.FilesHandles
{
    public class RegistryHandles : ISaveHandles
    {
        public string FileSavePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TextCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FinalCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void LoadByFile(Converter<string, string> decrypt)
        {
            throw new NotImplementedException();
        }

        public void LoadByString(string content)
        {
            throw new NotImplementedException();
        }

        public void SaveToFile(Converter<string, string> encrypt)
        {
            throw new NotImplementedException();
        }

        public string ToSaveString()
        {
            throw new NotImplementedException();
        }
    }
}
