using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JobTool.Base.Interfaces;

namespace JobTool.Base.Services {
    public interface ICommandParserService: IService
    {
        Tuple<ICommand, object> Parse(string input);
    }
}
