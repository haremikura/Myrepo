using MVCFramework.Models.Entity;

namespace MVCFramework.Models.DataTransferObject
{
    public class EditPageDto
    {
        public string EditText { get; set; }
        public Marker[] MarkerList { get; set; }
    }
}