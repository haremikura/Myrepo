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
            var fixText
                = new StringBuilder(elementText.Trim(charsToTrim));

            (int replacePointIndex, string markTextHasTag) replaceProperty
                = GetReplaceProperty(elementText, markedText, caretPosition);

            if (replaceProperty.replacePointIndex == -1)
            {
                return null;
            }

            int repacePoint = replaceProperty.replacePointIndex;
            string targetMarkText = replaceProperty.markTextHasTag;

            if (targetMarkText.Contains("</span>"))
            {
                //string fixMarkText = markedText.Replace("</span>", "");
                string markerCodeText = $@"</span><span style=""background:{colorcode}; "">{markedText}</span>";
                return fixText.Replace(targetMarkText, markerCodeText, repacePoint, targetMarkText.Length).ToString();
            }
            else if (targetMarkText.Contains("<span"))
            {
                var tag = Regex.Match(targetMarkText, "<span.*>").Value;
                string fixMarkText = markedText.Replace(tag, "");
                string newMarkText = $@"<span style=""background:{colorcode}; "">{fixMarkText}</span>{tag}";

                return fixText.Replace(targetMarkText, newMarkText, repacePoint, targetMarkText.Length).ToString();
            }
            else
            {
                string makrTextCode
                    = $@"<span style=""background:{colorcode}; "">{markedText}</span>";
                string fixElementText
                    = fixText.Replace(
                            markedText,
                            makrTextCode,
                            replaceProperty.replacePointIndex,
                            markedText.Length)
                        .ToString();
                return fixElementText;
            }

        }




        /// <summary>
        /// マーク個所マッチパタンの文字列を求める。
        /// </summary>
        /// <param name="elementText">マーク個所の文字列を含む文字列</param>
        /// <param name="markedText">マーク個所の文字列</param>
        /// <param name="caretPositionIndex">タグなしでの文字列にたいする、マーク個所の</param>
        /// <returns>正しい置換ポイントとなるインデックスを返す。当てはまる箇所がない場合、-1を返す。</returns>
        private (int, string) GetReplaceProperty(string elementText, string markedText, int caretPositionIndex)
        {
            //elementからmarkedTextにマッチした情報をMatchCollectionを求める。MatchCollectionの取得は、markTextについての判断をする関数から行う。
            //MatchCollectionのインデックスmatchの中から、「elementにあるspanタグをのぞいた文字列からmatchのあるindexの距離部分文字列の長さ」が、caretPositionIndexと同じ長さかを調べる
            (MatchCollection collection, string fixMarkText) markTextMatches = GetMarkTextMatches(elementText, markedText);
            foreach (Match match in markTextMatches.collection)
            {
                int checkIndex
                    = Regex.Replace(
                        elementText.Substring(0, match.Index),
                        "<.*?span.*?>",
                        ""
                        ).Length;

                if (checkIndex == caretPositionIndex)
                {
                    return (match.Index, markTextMatches.fixMarkText);
                }
            }
            return (-1, null);
        }

        /// <summary>
        /// elementTextを繰り返し適応して、markedTextとマッチした一連の対象を求める。
        /// </summary>
        /// <param name="elementText">マークされる文字列を含む文字列</param>
        /// <param name="markedText">マークされる文字列</param>
        /// <returns>一連の対象</returns>
        private (MatchCollection, string) GetMarkTextMatches(string elementText, string markedText)
        {
            //もし、markedTextが、elementTextの中で、タグを挟む文字列でなければ、、
            //markedTextで、MatchCollectionを求める。
            //もし、<span>タグを挟む文字列なら、markedTextの長さ分だけ<span>タグを認識する正規表現を組み込んだ文字列fixMarkedTextで、MatchCollectionを求める。
            if (Regex.IsMatch(elementText, markedText))
            {
                return (Regex.Matches(elementText, markedText), markedText);
            }
            else
            {
                return GetmarkTetHasTagMatches(elementText, markedText);
            }


            (MatchCollection, string) GetmarkTetHasTagMatches(string elementText, string markedText)
            {
                for (int i = 1; i < markedText.Length; i++)
                {
                    string fixMarkedText = markedText.Insert(i, "<.*?span.*?>");
                    var rx = new Regex(fixMarkedText);
                    if (rx.IsMatch(elementText))
                    {
                        string answerMarkedText = rx.Match(elementText).Value;
                        return (Regex.Matches(elementText, fixMarkedText), answerMarkedText);
                    }
                }
                return (null, null);
            }
        }

    }
}