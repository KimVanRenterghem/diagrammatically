﻿using System;
using System.Collections.Generic;
using diagrammatically.Domein;
using Keystroke.API;
using Keystroke.API.CallbackObjects;

namespace diagrammatically.electron_edge.api
{
    public class InputGenerator : Publisher<string>
    {
        private string words = string.Empty;
        private string _lastCurrentWindow = String.Empty;
        private Subscriber<string> _subscriber;

        private bool IsStringInput(KeyCode code)
            =>
                code >= KeyCode.D0 && code <= KeyCode.Z ||
                code >= KeyCode.NumPad0 && code <= KeyCode.Divide ||
                code >= KeyCode.OemSemicolon && code <= KeyCode.Oem102 ||
                code == KeyCode.Tab ||
                code == KeyCode.Enter ||
                code == KeyCode.Space ||
                IsBeack(code);

        private bool IsBeack(KeyCode code)
            => code == KeyCode.Back;

        private string GetChar(KeyPressed keyPressed)
            => keyPressed.KeyCode == KeyCode.Enter ? "\n" :
                keyPressed.KeyCode == KeyCode.Tab ? "\t" :
                keyPressed.ToString();

        public Action<KeyPressed> Genrrate(IEnumerable<string> langs)
            => character =>
            {
                if (character.CurrentWindow != _lastCurrentWindow)
                {
                    words = string.Empty;
                    _lastCurrentWindow = character.CurrentWindow;
                }

                if (IsStringInput(character.KeyCode))
                {
                    words = IsBeack(character.KeyCode)
                        ? words.Length > 0 ?
                            words.Substring(0, words.Length - 1) :
                            ""
                        : words + GetChar(character);

                    _subscriber.Lisen(words, character.CurrentWindow, langs);
                }
            };

        public void Subscribe(Subscriber<string> subscriber)
        {
            _subscriber = subscriber;
        }
    }
}