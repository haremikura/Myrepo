using MVCFramework.Models.Entity;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MVCFramework.Models
{
    public class PartailView
    {
        public string GetButton(TextFilesList textFilesList)
        {
            return $@" <div class=""content mt-4"">
                <div class=""card card_button"">
                    <button class=""card-body shadow btn-outline-dark"">
                        <div class=""file_info d-flex"">
                            <div class=""file_icon"">
                                <div class=""ti-money text-success border-success"">

                                </div>
                            </div>
                            <div class=""file_message"">
                                <div class=""file_name"">{textFilesList.FileName}</div>
                                <div class=""file_date"">{textFilesList.Update} </div>
                            </div>
                        </div>
                    </button>
                    <input id=""number"" name=""number"" type=""hidden"" value=""{textFilesList.FileId}"">

                </div>
            </div>";
        }

        internal string GetColor(string htmlElement, string markText, string colorCode)
        {
            return htmlElement.Replace(
                markText,
                $@"<span style=""background: linear-gradient(transparent 0%, {colorCode} 0%);"">{markText}<span>"
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementText"></param>
        /// <param name="markedText"></param>
        /// <param name="caretPosition"></param>
        /// <param name="colorcode"></param>
        /// <returns></returns>
        public string GetMarkerText(string elementText, string markedText, int caretPosition, string colorcode)
        {

            char[] charsToTrim = { ' ', '\n' };
            var updateText = new StringBuilder(elementText.Trim(charsToTrim));
            int replacePointIndex = GetCarePosition(elementText, markedText, caretPosition);

            if (replacePointIndex == -1)
            {
                return null;
            }

            if (markedText.Contains("</span>"))
            {

                string fixMarkText = markedText.Replace("</span>", "");
                string markerCodeText = $@"</span><span style=""background:{colorcode}; "">{fixMarkText}</span>";
                return updateText.Replace(markedText, markerCodeText, replacePointIndex, markedText.Length).ToString();
            }
            else if (markedText.Contains("<span"))
            {
                var before = Regex.Match(markedText, "<span.*>").Value;
                string fixMarkText = markedText.Replace(before, "");
                string markerCodeText = $@"<span style=""background:{colorcode}; "">{fixMarkText}</span>{before}";

                return updateText.Replace(markedText, markerCodeText, replacePointIndex, markedText.Length).ToString();
            }
            else
            {
                string makrTextCode
                    = $@"<span style=""background:{colorcode}; "">{markedText}</span>";
                string fixElementText
                    = updateText.Replace(
                            markedText,
                            makrTextCode,
                            replacePointIndex,
                            markedText.Length)
                        .ToString();
                return fixElementText;
            }

            static int GetCarePosition(string elementText, string markedText, int caretPositionIndex)
            {

                foreach (Match match in GetMatchText(elementText, markedText))
                {
                    string checkIndex
                        = Regex.Replace(
                            elementText.Substring(0, match.Index),
                            "<.*?span.*?>",
                            ""
                            );

                    if (checkIndex.Length == caretPositionIndex)
                    {
                        return match.Index;
                    }
                }
                return -1;

                MatchCollection GetMatchText(string elementText, string markedText)
                {
                    if (Regex.IsMatch(elementText, markedText))
                    {
                        return Regex.Matches(elementText, markedText);
                    }

                    for (int i = 1; i < markedText.Length; i++)
                    {

                    }

                    return null;
                }
            }
        }
    }
}