using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1_BouncerX
{
    class Occasion
    {
        public int AttendanceCount { get; private set; }

        public void Enter()
        {
            AttendanceCount++;
        }

        public void Leave()
        {
            if (AttendanceCount > 0)
            {
                AttendanceCount--;
            }
        }
    }
}
