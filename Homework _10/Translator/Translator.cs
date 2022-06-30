﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TranslatorLib
{
    public class Translator
    {

        public event EventHandler<TranslationAbsenceEventArgs> OnTranslationAbsence;
        public TranslationDictionary Dictionary { get; set; } = new TranslationDictionary();

        public Translator()
        {

        }
        public Translator(TranslationDictionary dictionary)
        {
            Dictionary = dictionary;
        }

        public List<string> Translate(List<string> lines, int inputsNum = 3)
        {
            if(lines == null)
                throw new ArgumentNullException();

            if(Dictionary.IsEmpty())
                throw new DictionaryIsEmptyException();

            // копія тексту, що прийшов
            List<string> resultLines = new List<string>(lines);
            var words = new List<string>();

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            for (int i = 0; i < resultLines.Count && inputsNum != 0; i++)
            {
                words.Clear();
                string[] split = resultLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in split)
                {
                    words.Add(word);
                }

                int countVariedle = 3;
                foreach (string word in words)
                {
                    if (inputsNum == 0)
                        break;

                    bool isUpper = Char.IsUpper(word[0]);

                    int count = 0;
                    if (Char.IsPunctuation(word[word.Length - 1]))
                    {
                        while (!Dictionary.ContainsKey(word[0..^1]) && count < countVariedle)
                        {
                            OnTranslationAbsence(this.Dictionary, new TranslationAbsenceEventArgs(word[0..^1], inputsNum--));
                            count++;
                        }
                        if(isUpper)
                            resultLines[i] = resultLines[i].Replace(word[0..^1], textInfo.ToTitleCase(Dictionary[word[0..^1]]));
                        else 
                            resultLines[i] = resultLines[i].Replace(word[0..^1], Dictionary[word[0..^1]]);
                    }
                    else
                    {
                        while (!Dictionary.ContainsKey(word) && count < countVariedle)
                        {
                            OnTranslationAbsence(this.Dictionary, new TranslationAbsenceEventArgs(word, inputsNum--));
                            count++;
                        }
                        if (isUpper)
                            resultLines[i] = resultLines[i].Replace(word, textInfo.ToTitleCase(Dictionary[word]));
                        else
                            resultLines[i] = resultLines[i].Replace(word, Dictionary[word]);
                    }
                }
            }
            return resultLines;
        }






        public class TranslationAbsenceEventArgs : EventArgs
        {
            public string Word { get; set; }

            // Кількість спроб для корекції словника має бути обмеженою.
            public int InputsLeft { get; set; }
            public TranslationAbsenceEventArgs(string word, int inputsLeft)
            {
                Word = word;
                InputsLeft = inputsLeft;
            }
        }
    }
}
