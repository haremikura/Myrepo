using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCFramework.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

            string testText = new PartailView().GetMarkerText(elementText, "ちり</span>ぬる", 13, "#ddd");

            string judgeText = $"いろはに<span>ほへと</span><span style=\"background:#ddd; \">ちりぬる</span>を";

            Debug.WriteLine($"{testText}\r{judgeText}");

            Assert.AreEqual(testText, judgeText);


            string testText2 = new PartailView().GetMarkerText(elementText, @"はに<span>ほへ", 2, "#ddd");

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
    }
}
