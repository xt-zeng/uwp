using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lesson2_ControlCorral
{
    public class GenericCommand
        : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event Action<string> DoSomthing;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var command = parameter as string;
            DoSomthing?.Invoke(command);
        }
    }
}
