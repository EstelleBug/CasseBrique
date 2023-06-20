using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ScreenService
    {
        public static int width = 1920;
        public static int height = 1080;

        public static int pixels()
        { return width * height; }
    }
}
