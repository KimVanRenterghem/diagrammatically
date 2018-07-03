using System;
using System.Collections.Generic;
using diagrammatically.Domein.InputProsesers;
using Keystroke.API;
using Keystroke.API.CallbackObjects;

namespace diagrammatically.AvaloniaUi
{
    public class InputGenerator
    {
        private readonly IInputProseser _inputprosecer;
        private string words = string.Empty;

        public InputGenerator(IInputProseser inputprosecer)
        {
            _inputprosecer = inputprosecer;
        }

        public Action<KeyPressed> Genrrate(IEnumerable<string> langs)
        {
            return character =>
            {
                bool IsStringInput(KeyCode code)
                {
                    return
                        code >= KeyCode.D0 && code <= KeyCode.Z ||
                        code >= KeyCode.NumPad0 && code <= KeyCode.Divide ||
                        code >= KeyCode.OemSemicolon && code <= KeyCode.Oem102 ||
                        code == KeyCode.Tab ||
                        code == KeyCode.Enter ||
                        code == KeyCode.Space ||
                        IsBeack(code);
                }

                bool IsBeack(KeyCode code)
                {
                    return code == KeyCode.Back;
                }

                string GetChar(KeyPressed keyPressed)
                {
                    return keyPressed.KeyCode == KeyCode.Enter ? "\n" :
                        keyPressed.KeyCode == KeyCode.Tab ? "\t" :
                        keyPressed.ToString();
                }

                if (IsStringInput(character.KeyCode))
                {
                    words = IsBeack(character.KeyCode)
                        ? words.Length > 0 ?
                            words.Substring(0, words.Length - 1) :
                            ""
                        : words + GetChar(character);
                    
                    _inputprosecer.Loockup(words, character.CurrentWindow, langs);
                }
            };
        }
    }
}