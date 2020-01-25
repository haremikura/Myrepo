using MVCFramework.Models.Entity;
using System.Text;
using System.Text.RegularExpressions;

namespace MVCFramework.Models
{
    /// <summary>
    /// ビューの部品を追加するクラスです。
    /// </summary>
    public class PartailView
    {
        /// <summary>
        /// ファイル選択ボタンのrasorを作成する
        /// </summary>
        /// <param name="textFilesList">ボタンとなるTextFilesList</param>
        /// <returns>ファイル選択ボタンのrazor</returns>
        public string GetFileSelectButton(TextFilesList textFilesList)
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

        /// <summary>
        ///　
        /// </summary>
        /// <param name="htmlElement"></param>
        /// <param name="markText"></param>
        /// <param name="colorCode"></param>
        /// <returns></returns>
        internal string GetMarkerColorIndex(string htmlElement, string markText, string colorCode)
        {
            return htmlElement.Replace(
                markText,
                $@"<span style=""background: linear-gradient(transparent 0%, {colorCode} 0%);"">{markText}<span>"
                );
        }

        /// <summary>
        /// マークされたテキストのrazorを作成する。
        /// </summary>
        /// <param name="elementText">マークされるテキストのrazor</param>
        /// <param name="markedText">マークされるテキスの、表示上の文字</param>
        /// <param name="caretPosition">タグなしでの文字列に対する、マーク個所の位置</param>
        /// <param name="colorcode">マークの色のコード</param>
        /// <returns>マークされたテキストのrazor</returns>
        public string GetMarkerText(string elementText, string markedText, int caretPosition, string colorcode)
        {

            //elementTextから、空行と、改行をを除去したfixTextを取得する
            var fixText
                = new StringBuilder(elementText.Trim( ' ', '\n' ));


            (int replacePointIndex, string markTextHasTag)
                = GetReplaceProperty(fixText.ToString(), markedText, caretPosition);

            if (replacePointIndex == -1)
            {
                return null;
            }

            int repacePoint = replacePointIndex;
            string targetMarkText = markTextHasTag;

            //targetMarkTextに含まれるタグの条件で変更して、fixTextのstringを置換する
            if (targetMarkText.Contains("</span>"))
            {
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
                            replacePointIndex,
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
        /// <param name="caretPosition">タグなしでの文字列に対する、マーク個所の位置</param>
        /// <returns>正しい置換ポイントとなるインデックスを返す。当てはまる箇所がない場合、-1を返す。</returns>
        private (int, string) GetReplaceProperty(string elementText, string markedText, int caretPosition)
        {
            //elementからmarkedTextにマッチした情報をMatchCollectionを求める。
            //MatchCollectionの取得は、markTextを引数にとる関数から取得する。

            //MatchCollectionのインデックスmatchの中から、
            //「elementにあるspanタグをのぞいた文字列からmatchのあるindex
            // の距離部分文字列の長さ」が、caretPositionにあたる
            (MatchCollection collection, string fixMarkText) = GetMarkTextMatches(elementText, markedText);
            foreach (Match match in collection)
            {
                int checkIndex
                    = Regex.Replace(
                        elementText.Substring(0, match.Index),
                        "<.*?span.*?>",
                        ""
                        ).Length;

                if (checkIndex == caretPosition)
                {
                    return (match.Index, fixMarkText);
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
            //もし、markedTextが、elementTextの中で、タグを挟む文字列でなければ、
            //markedTextで、MatchCollectionを求める。
            //もし、<span>タグを挟む文字列なら、markedTextの長さ分だけ
            //<span>タグを認識する正規表現を組み込んだ文字列fixMarkedTextで、
            //MatchCollectionを求める。
            if (Regex.IsMatch(elementText, markedText))
            {
                return (Regex.Matches(elementText, markedText), markedText);
            }
            else
            {
                return GetmarkTetHasTagMatches(elementText, markedText);
            }


            (MatchCollection, string) GetmarkTetHasTagMatches(string element, string markd)
            {
                for (int i = 1; i < markd.Length; i++)
                {
                    string fixMarkedText = markd.Insert(i, "<.*?span.*?>");
                    var rx = new Regex(fixMarkedText);
                    if (rx.IsMatch(element))
                    {
                        string answerMarkedText = rx.Match(element).Value;
                        return (Regex.Matches(element, fixMarkedText), answerMarkedText);
                    }
                }
                return (null, null);
            }
        }

    }
}