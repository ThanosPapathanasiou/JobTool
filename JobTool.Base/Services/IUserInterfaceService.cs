using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobTool.Base.Interfaces;

namespace JobTool.Base.Interfaces.Services {
    public interface IUserInterfaceService: IService {
        void Output(string message);
        string Input();
    }
}
