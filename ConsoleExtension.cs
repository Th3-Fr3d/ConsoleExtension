using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE
{
    public struct ConsoleExtension
    {
        public static StringBuilder consoleBuffer = new StringBuilder();
        public static string formatPrompt = string.Empty;
        /// <summary>
        /// Reads the next line of characters from the standard input stream.
        /// </summary>
        /// <param name="prompt">A string prompting the user to enter something.</param>
        /// <param name="advancedConsoleColor">The color of the user input.</param>
        /// <param name="colorParameters">-1 : Draw the whole line in the same color. 0 : Only the user input will be drawn in provided color. 1 : Only the text prompt will be drawn in provided color. DEFAULT: -1</param>
        /// <returns></returns>
        public static string Input(string prompt, ConsoleColorExtension advancedConsoleColor, int colorParameters)
        {
            string userInput = string.Empty;
            switch (colorParameters)
            {
                case -1:
                    {
                        Input(prompt, advancedConsoleColor);
                        break;
                    }
                case 0:
                    {
                        formatPrompt = ConsoleColorExtension.Default.ToString() + prompt + advancedConsoleColor.ToString();
                        Console.Write(prompt);
                        Console.ForegroundColor = advancedConsoleColor.ToConsoleColor();
                        userInput = ReadLineSafe();
                        Console.ForegroundColor = ConsoleColorExtension.Default.ToConsoleColor();
                        break;
                    }
                case 1:
                    {
                        formatPrompt = advancedConsoleColor.ToString() + prompt + ConsoleColorExtension.Default.ToString();
                        Console.ForegroundColor = advancedConsoleColor.ToConsoleColor();
                        Console.Write(prompt);
                        Console.ForegroundColor = ConsoleColorExtension.Default.ToConsoleColor();
                        userInput = ReadLineSafe();
                        break;
                    }
                default:
                    {
                        throw new IndexOutOfRangeException("COLOR_PARAMETER_OUT_OF_RANGE");
                    }
            }
            return userInput;
        }
        /// <summary>
        /// Reads the next line of characters from the standard input stream.
        /// </summary>
        /// <param name="prompt">A string prompting the user to enter something.</param>
        /// <param name="consoleColor">The color of the user input.</param>
        /// <returns></returns>
        public static string Input(string prompt, ConsoleColorExtension consoleColor)
        {
            formatPrompt = consoleColor.ToString() + prompt;
            Console.ForegroundColor = consoleColor.ToConsoleColor();
            Console.Write(prompt);
            string userInput = ReadLineSafe();
            Console.ForegroundColor = ConsoleColorExtension.Default.ToConsoleColor();
            return userInput;
        }
        /// <summary>
        /// Reads the next line of characters from the standard input stream.
        /// </summary>
        /// <param name="prompt">A string prompting the user to enter something.</param>
        /// <returns></returns>
        public static string Input(string prompt)
        {
            Console.Write(prompt);
            formatPrompt = prompt;
            string userInput = ReadLineSafe();
            return userInput;
        }
        /// <summary>
        /// Reads the next line of characters from the standard input stream.
        /// </summary>
        /// <returns></returns>
        public static string Input()
        {
            string userInput = ReadLineSafe();
            return userInput;
        }

        public static string ReadLineSafe()
        {
            consoleBuffer = new StringBuilder();
            for (; ; )
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter && consoleBuffer.Length > 0)
                {
                    Console.Write("\r\n");
                    string userInput = consoleBuffer.ToString();
                    consoleBuffer.Clear();
                    formatPrompt = string.Empty;
                    return userInput;
                }
                else if (key.Key == ConsoleKey.Backspace && consoleBuffer.Length > 0)
                {
                    consoleBuffer.Remove(consoleBuffer.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    consoleBuffer.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }
        }

        public static void PrintF(string text)
        {
            string _override = new string(' ', formatPrompt.Length + consoleBuffer.Length);
            if (_override.Length > 0)
            {
                Console.Write("\r" + _override);
            }
            Console.ForegroundColor = ConsoleColorExtension.Default.ToConsoleColor();
            bool isfirstRealText = true;
            if (text.Contains("\x00"))
            {
                string[] textElements = text.Split('\x00');
                for (int i = 0; i < textElements.Count(); i++)
                {
                    if (textElements[i].Length > 1)
                    {
                        if (textElements[i].Substring(0, 2).Equals("|\x01"))
                        {
                            int index = Convert.ToInt32(textElements[i].Replace("|\x01", ""));
                            if (index > -1 && index < 16)
                            {
                                Console.ForegroundColor = (ConsoleColor)index;
                            }
                            else
                            {
                                throw new IndexOutOfRangeException("INVALID_CONSOLE_COLOR");
                            }
                        }
                        else
                        {
                            if (isfirstRealText)
                            {
                                Console.Write("\r" + textElements[i]);
                                isfirstRealText = false;
                            }
                            else
                            {
                                Console.Write(textElements[i]);
                            }
                        }
                    }
                    else if (isfirstRealText)
                    {
                        Console.Write("\r" + textElements[i]);
                        isfirstRealText = false;
                    }
                    else
                    {
                        Console.Write(textElements[i]);
                    }
                }
                Console.Write("\n");
            }
            else
            {
                if(_override.Length > 0)
                {
                    Console.WriteLine("\r" + text);
                }
                else
                {
                    Console.WriteLine(text);
                }
            }
            if (_override.Length > 0)
            {
                string[] bufferElements = (formatPrompt + consoleBuffer.ToString()).Split('\x00');
                for (int i = 0; i < bufferElements.Count(); i++)
                {
                    if (bufferElements[i].Length > 0)
                    {
                        if (bufferElements[i].Substring(0, 2).Equals("|\x01"))
                        {
                            int index = Convert.ToInt32(bufferElements[i].Replace("|\x01", ""));
                            if (index > -1 && index < 16)
                            {
                                Console.ForegroundColor = (ConsoleColor)index;
                            }
                            else
                            {
                                throw new IndexOutOfRangeException("INVALID_CONSOLE_COLOR");
                            }
                        }
                        else
                        {
                            Console.Write(bufferElements[i]);
                        }
                    }
                }
            }
        }
    }
}
