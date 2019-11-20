using MVCFramework.Models.Entity;

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
    }
}