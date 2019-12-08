using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCFramework.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MsTest
{
    [TestClass]
    public class PartailViewTest
    {
        [TestMethod]
        public void CheckSet()
        {

            string testText = new PartailView().GetMarkerText("asdfghj", "sdf", 1, "#aabbcc");

            Debug.WriteLine($@"{testText} 
								a<span style=""background:#aabbcc; "">sdf</span>ghj");


            Assert.AreEqual(testText, $@"a<span style=""background:#aabbcc; "">sdf</span>ghj");
        }

        [TestMethod]
        public void CheckSet2()
        {

            string testText = new PartailView().GetMarkerText("ああいいああ", "ああ", 0, "#aabbcc");

            Debug.WriteLine($"{testText}\r <span style=\"background:#aabbcc; \">ああ</span>いいああ");



            Assert.AreEqual(testText, $@"<span style=""background:#aabbcc; "">ああ</span>いいああ");
        }

        [TestMethod]
        public void CheckSet3()
        {
            /// <summary>
            /// マークテスト：既存マークへの、上書きのマークのテスト
            /// </summary>
            string elementText
                = $"いろはに<span>ほへとちり</span>ぬるを";

            string testText = new PartailView().GetMarkerText(elementText, "ちりぬる", 7, "#ddd");

            string judgeText = $"いろはに<span>ほへと</span><span style=\"background:#ddd; \">ちりぬる</span>を";

            Debug.WriteLine($"{testText}\r{judgeText}");

            Assert.AreEqual(testText, judgeText);


            string testText2 = new PartailView().GetMarkerText(elementText, @"はにほへ", 2, "#ddd");

            string judgeText2 = $"いろ<span style=\"background:#ddd; \">はにほへ</span><span>とちり</span>ぬるを";

            Debug.WriteLine($"{testText2}\r{judgeText2}");

            Assert.AreEqual(testText2, judgeText2);
        }

        [TestMethod]

        public void CheckSet4()
        {
            string elementText
               = $"いいいろろろいいい";

            string testText = new PartailView().GetMarkerText(elementText, "いいい", 0, "#aabbcc");

            string judgeText
                = $"<span style=\"background:#aabbcc; \">いいい</span>ろろろいいい";

            Debug.WriteLine($"{testText}\r{judgeText}");

            Assert.AreEqual(testText, judgeText);



            string elementText2 = $"いいいろろろいいい";
            string testText2 = new PartailView().GetMarkerText(elementText2, "いいい", 0, "#aabbcc");

            string judgeText2
                = $"<span style=\"background:#aabbcc; \">いいい</span>ろろろいいい";

            Debug.WriteLine($"{testText2}\r{judgeText2}");

            Assert.AreEqual(testText2, judgeText2);
        }


        [TestMethod]
        public void IndexSerchCode()
        {


            int answerIndex
                = GetCarePosition("12anc<span>ddggd</span>kd12kkddg12", "12", 12);
            Assert.AreEqual(answerIndex, 25);


            int answerIndex2
                = GetCarePosition("12anc<span>dd</span>g<span>gdk</span>d12kkddg12popo12", "12", 12);
            Assert.AreEqual(answerIndex2, 38);



            int GetCarePosition(string elementText, string markedText, int caretPositionIndex)
            {
                var matches = Regex.Matches(elementText, markedText);
                foreach (Match match in matches)
                {
                    string checkIndex
                        = Regex.Replace(
                            elementText.Substring(0, match.Index), "<.*?span.*?>", ""
                            );

                    if (checkIndex.Length == caretPositionIndex)
                    {
                        return match.Index;
                    }
                }
                return 0;
            }

        }

        /// <summary>
        /// PartailView.GetMatchCollectionMarkedTextWithTagのを検査する
        /// </summary>
        [TestMethod]
        public void GetMatchCollectionMarkedTextWithTagTest()
        {
            testCode($"いろはに<span>ほへとちりぬ</span>るを", "ちりぬる");

            testCode($"いろはに<span>ほへとちり</span>ぬるを", "にほ");

            void testCode(string testText, string markText)
            {
                (MatchCollection collection, string fiMarkedText) testMatch
                    = GetMatchCollectionMarkedTextWithTag(testText, markText);
                Debug.WriteLine($"{testText} : {markText} -> {testMatch.fiMarkedText}");
                foreach (Match match in testMatch.collection)
                {

                    Debug.WriteLine("the match index is " + match.Index);
                }
            }

            (MatchCollection, string) GetMatchCollectionMarkedTextWithTag(string elementText, string markedText)
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
