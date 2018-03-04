using System;
using System.Collections.Generic;
using System.Linq;
using Tesseract;


namespace OCRApp
{
    public class FindService
    { 

        private string phrase;
        private Page page;
        private string tempString;
        private List<Rect> tempCoords;
    
        public FindService(string path, string tessdataPath, string lang)
        {
            var ocrEngine = new TesseractEngine(tessdataPath, lang, EngineMode.Default);
            var imageWithText = Pix.LoadFromFile(path);
            page = ocrEngine.Process(imageWithText);
        }

        private List<Rect> ChekTempString()
        {
            if (tempString == phrase)
            {
                var copy = tempCoords;
                tempString = "";
                tempCoords = new List<Rect>();
                return copy;
            }
            tempString = "";
            tempCoords = new List<Rect>();
            return new List<Rect>();
        }
        private List<Rect> ChekWord(string word, Rect coords)
        {
            word = word.Trim();
            if (phrase.Contains(word))
            {
                tempString = String.Join(" ", tempString, word).Trim();
                tempCoords.Add(coords);
                return new List<Rect>();
            }
            return ChekTempString();
        }
        public List<Dictionary<string, object>> GettCoordinates(string phrase)
        {
            tempString = "";
            tempCoords = new List<Rect>();
            this.phrase = phrase.ToLower();
            var coords = new List<Dictionary<string, object>>();
            using (page)
            {

                using (var iter = page.GetIterator())
                {
                    iter.Begin();
                    do
                    {
                        Rect wordBounds;
                        iter.TryGetBoundingBox(PageIteratorLevel.Word, out wordBounds);
                        var word = iter.GetText(PageIteratorLevel.Word).ToLower();
                        var newCoords = ChekWord(word, wordBounds);
                        foreach (var coord in newCoords)
                        {
                            Dictionary<string, object> dict = new Dictionary<string, object>()
                            {
                                {"x1", coord.X1},
                                {"y1", coord.Y1},
                                {"x2", coord.X2},
                                {"y2", coord.Y2},
                                {"width", coord.Width},
                                {"height", coord.Height},
                            };
                            coords.Add(dict);
                        }
                    } while (iter.Next(PageIteratorLevel.Word));
                }
            }   

            return coords;
        }
    }
}
