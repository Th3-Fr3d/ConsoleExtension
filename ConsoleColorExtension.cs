using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE
{
    public sealed class ConsoleColorExtension
    {

        private readonly string name;
        private readonly int value;

        public static ConsoleColorExtension Default = new ConsoleColorExtension((int)ConsoleColor.White, "\x00|\x01" + (int)ConsoleColor.White + "\x00");
        public static readonly ConsoleColorExtension Red = new ConsoleColorExtension((int)ConsoleColor.Red, "\x00|\x01" + (int)ConsoleColor.Red + "\x00");
        public static readonly ConsoleColorExtension Black = new ConsoleColorExtension((int)ConsoleColor.Black, "\x00|\x01" + (int)ConsoleColor.Black + "\x00");
        public static readonly ConsoleColorExtension Blue = new ConsoleColorExtension((int)ConsoleColor.Blue, "\x00|\x01" + (int)ConsoleColor.Blue + "\x00");
        public static readonly ConsoleColorExtension Cyan = new ConsoleColorExtension((int)ConsoleColor.Cyan, "\x00|\x01" + (int)ConsoleColor.Cyan + "\x00");
        public static readonly ConsoleColorExtension DarkBlue = new ConsoleColorExtension((int)ConsoleColor.DarkBlue, "\x00|\x01" + (int)ConsoleColor.DarkBlue + "\x00");
        public static readonly ConsoleColorExtension DarkCyan = new ConsoleColorExtension((int)ConsoleColor.DarkCyan, "\x00|\x01" + (int)ConsoleColor.DarkCyan + "\x00");
        public static readonly ConsoleColorExtension DarkGray = new ConsoleColorExtension((int)ConsoleColor.DarkGray, "\x00|\x01" + (int)ConsoleColor.DarkGray + "\x00");
        public static readonly ConsoleColorExtension DarkGreen = new ConsoleColorExtension((int)ConsoleColor.DarkGreen, "\x00|\x01" + (int)ConsoleColor.DarkGreen + "\x00");
        public static readonly ConsoleColorExtension DarkMagenta = new ConsoleColorExtension((int)ConsoleColor.DarkMagenta, "\x00|\x01" + (int)ConsoleColor.DarkMagenta + "\x00");
        public static readonly ConsoleColorExtension DarkRed = new ConsoleColorExtension((int)ConsoleColor.DarkRed, "\x00|\x01" + (int)ConsoleColor.DarkRed + "\x00");
        public static readonly ConsoleColorExtension DarkYellow = new ConsoleColorExtension((int)ConsoleColor.DarkYellow, "\x00|\x01" + (int)ConsoleColor.DarkYellow + "\x00");
        public static readonly ConsoleColorExtension Gray = new ConsoleColorExtension((int)ConsoleColor.Gray, "\x00|\x01" + (int)ConsoleColor.Gray + "\x00");
        public static readonly ConsoleColorExtension Green = new ConsoleColorExtension((int)ConsoleColor.Green, "\x00|\x01" + (int)ConsoleColor.Green + "\x00");
        public static readonly ConsoleColorExtension Magenta = new ConsoleColorExtension((int)ConsoleColor.Magenta, "\x00|\x01" + (int)ConsoleColor.Magenta + "\x00");
        public static readonly ConsoleColorExtension White = new ConsoleColorExtension((int)ConsoleColor.White, "\x00|\x01" + (int)ConsoleColor.White + "\x00");
        public static readonly ConsoleColorExtension Yellow = new ConsoleColorExtension((int)ConsoleColor.Yellow, "\x00|\x01" + (int)ConsoleColor.Yellow + "\x00");

        private ConsoleColorExtension(int value, string name)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return name;
        }

        public int GetValue()
        {
            return value;
        }

        public void SetDefault()
        {
            Default = new ConsoleColorExtension(value,name);
        }

        public bool IsDefault()
        {
            return value == Default.value ? true : false;
        }

        public ConsoleColor ToConsoleColor()
        {
            try
            {
                return (ConsoleColor)value;
            }
            catch
            {
                throw new IndexOutOfRangeException("COLOR_PARAMETER_OUT_OF_RANGE");
            }
        }

    }
}
